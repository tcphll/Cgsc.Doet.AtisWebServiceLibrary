using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
//using Newtonsoft.Json;
using System.Net.Http;
using System.Diagnostics;
using System.Net;

using Cgsc.Doet.AtisWebServiceLibrary.MessageClasses;

namespace Cgsc.Doet.AtisWebServiceLibrary
{
    public class RestRequester
    {
        //TODO: This all needs to be encrypted and placed within a config file
        //URL       : https://interfacestest.atsc.army.mil/transcript-ws/api/
        //UserName  : system_sms
        //Password  : Sm3s1$tE*+_2014

        #region Constants
        public const string ENROLLMENT_UPDATE_URL = "enrollments";
        #endregion

        /// <summary>
        /// Base URL for the web service
        /// </summary>
        private string URL { get; set; }
        /// <summary>
        /// ATIS username
        /// </summary>
        private string UserName { get; set; }
        /// <summary>
        /// ATIS password
        /// </summary>
        ///
        private string Password { get; set; }
        ///<summary>
        ///Common name for PKI authentication. 
        ///</summary>
        private string CommonName { get; set; }
        /// <summary>
        /// Response headers 
        /// </summary>
        public System.Net.Http.Headers.HttpResponseHeaders ResponseHeaders { get; set; }
        public HttpStatusCode LastRequestStatusCode { get; set; }

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="url"></param>
        private RestRequester(string username, string password, string url)
        {
            this.URL = url;
            this.UserName = username;
            this.Password = password;
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Factory method for creating a RestRequester object
        /// </summary>
        /// <param name="username">ATIS username</param>
        /// <param name="password">ATIS password</param>
        /// <param name="url">Base URL for the web service</param>
        /// <returns>RestRequester</returns>
        public static RestRequester GetRestRequester(string username, string password, string url)
        {
            RestRequester requester = new RestRequester(username, password, url);
            return requester;
        }
        #endregion

        /// <summary>
        /// Sends a request to the specified URI. Returns a string representing a JSON message.
        /// </summary>
        /// <param name="requestUri">URI for the request. This is based on the base URL used during instantiation.</param>
        /// <param name="httpMethod">Method of the request. Can be GET, POST or PUT</param>
        /// <param name="message">Optional paramter used for POST and PUT operations. This is a JSON message as defined by the ATIS API</param>
        /// <returns>String representing a json object</returns>
        public async Task<string> SendRequest(string requestUri, HttpMethod httpMethod, string message = "")
        {
            //dynamic attrsResponse = new AtisResponseMessage();
            string json = "";
            HttpResponseMessage response = null;
            using (var client =  ConfigureHttpClient())
            {
                try
                {
                    //Set up default and authentication headers
                    //client = ConfigureHttpClient();

                    
                    if (httpMethod == HttpMethod.Get)
                    {
                        response = await client.GetAsync(requestUri);
                    }
                    else if (httpMethod == HttpMethod.Post)
                    {
                        StringContent content = new StringContent(message);
                        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        response = await client.PostAsync(requestUri, content);
                    }
                    else if (httpMethod == HttpMethod.Put)
                    {
                        StringContent content = new StringContent(message);
                        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        //content.Headers.Add("X-HTTP-Method-Override", "PUT");
                        response = await client.PostAsync(requestUri, content);
                    }
                    
                    if (response != null )
                    {
                        if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                        {
                            //string json = await response.Content.ReadAsStringAsync();
                            json = response.Content.ReadAsStringAsync().Result;
                            ResponseHeaders = response.Headers;
                        }
                        else
                        {
                            json = response.StatusCode.ToString();
                        }

                    }
                    //Save the status code of this request so we can use it later.
                    LastRequestStatusCode = response.StatusCode;
                    
                }
                catch (TaskCanceledException ex)
                {
                    WriteErrorMessage(ex);
                }
                catch (Exception ex)
                {
                    WriteErrorMessage(ex);
                    //bubble the exception up so calling application can handle it
                    AtisWebServiceException atisEx = new AtisWebServiceException(ex, json, response.Headers);
                    throw atisEx;
                }
                
            }
            
            return json;
        }

        /// <summary>
        /// Verifies a student's enrollment in ATRRS for a specific course/class combination
        /// </summary>
        /// <param name="crs">ATRRS course ID</param>
        /// <param name="cls">ATRRS class ID</param>
        /// <param name="edipi">EDIPI of student being verified</param>
        /// <returns>boolean value indicating enrollment status</returns>
        public async Task<bool> VerifyEnrollment(string crs, string edipi, string fy, string cls = "" )
        {            
            //First, let's get the class we're checking enrollment for
            //string uri = string.Format("classes?crs={0}&cls={1}", crs, cls);
            string uri = "";
            if (cls.Count() > 0)
            {
                uri = string.Format("classes?crs={0}&fy={1}&cls={2}&limit=1000", crs, fy, cls);
            }
            else
            {
                uri = string.Format("classes?crs={0}&fy={1}&limit=1000", crs, fy);
            }
            string json = await this.SendRequest(uri,HttpMethod.Get);
            AtisClasses atisClasses = (AtisClasses)AtisJsonSerializer.Deserialize<AtisClasses>(json);
            int classId = atisClasses.AtisClassList[0].Id;

            //Let's check each class. If student is enrolled in one, return true for verification. We'll set the return value to false by default and change it if we 
            //find an enrollment for this student.
            bool retval = false;
            foreach (AtisClass atisClass in atisClasses.AtisClassList)
            {
                uri = string.Format("classes/{0}/enrollments?edipi={1}&limit=1000", atisClass.Id.ToString(), edipi);
                json = await this.SendRequest(uri, HttpMethod.Get);
                if (this.LastRequestStatusCode == HttpStatusCode.OK)
                {                   
                    AtisRosters atisRosters = (AtisRosters)AtisJsonSerializer.Deserialize<AtisRosters>(json);

                    //if we have an enrollment for this edipi, return true
                    if (atisRosters.AtisRosterList != null && atisRosters.AtisRosterList.Count > 0)
                    {
                        retval = true;
                        break;
                    }
                }
            }
           
            return retval;
        }

        /// <summary>
        /// Verifies a student's enrollment in ATRRS for a specific course/class combination
        /// </summary>
        /// <param name="crs">ATRRS course ID</param>
        /// <param name="cls">ATRRS class ID</param>
        /// <param name="edipi">EDIPI of student being verified</param>
        /// <returns>boolean value indicating enrollment status</returns>
        public async Task<string> GetEnrolledClass(string crs, string edipi, string fy)
        {
            //First, let's get the class we're checking enrollment for
            //string uri = string.Format("classes?crs={0}&cls={1}", crs, cls);            
           
            string uri = string.Format("classes?crs={0}&fy={1}", crs, fy);
            
            string json = await this.SendRequest(uri, HttpMethod.Get);
            AtisClasses atisClasses = (AtisClasses)AtisJsonSerializer.Deserialize<AtisClasses>(json);
            int classId = atisClasses.AtisClassList[0].Id;

            //Let's check each class. If student is enrolled in one, return true for verification. We'll set the return value to false by default and change it if we 
            //find an enrollment for this student.
            string retval = "";
            foreach (AtisClass atisClass in atisClasses.AtisClassList)
            {
                uri = string.Format("classes/{0}/enrollments?edipi={1}", atisClass.Id.ToString(), edipi);
                json = await this.SendRequest(uri, HttpMethod.Get);
                if (this.LastRequestStatusCode == HttpStatusCode.OK)
                {
                    AtisRosters atisRosters = (AtisRosters)AtisJsonSerializer.Deserialize<AtisRosters>(json);

                    //if we have an enrollment for this edipi, return true
                    if (atisRosters.AtisRosterList != null && atisRosters.AtisRosterList.Count > 0)
                    {
                        retval = atisRosters.AtisRosterList[0].ClassId;
                        break;
                    }
                }
            }

            return retval;
        }

        /// <summary>
        /// Gets the status of a transaction already submited to the ATIS Web Service.
        /// </summary>
        /// <param name="transactionID">ID of the transaction</param>
        /// <returns></returns>
        public async Task<TransactionsStatus> GetTransactionStatus(int transactionID)
        {
            TransactionsStatus status;
            string json = "";
            try
            {
                 json = await this.SendRequest(String.Format("transactions/{0}", transactionID.ToString()), HttpMethod.Get);
                AtisTransaction trans = (AtisTransaction)AtisJsonSerializer.Deserialize<AtisTransaction>(json);


                switch (trans.ProcessCode)
                {
                    case "C":
                        status = TransactionsStatus.Complete;
                        break; 
                    case "A":
                        status = TransactionsStatus.Awaiting;
                        break;
                    case "E":
                        status = TransactionsStatus.Error;
                        break;
                    case "S":
                        status = TransactionsStatus.Sent;
                        break;
                    case "Q":
                        status = TransactionsStatus.Queued;
                        break;
                    case "P":
                        status = TransactionsStatus.Processed;
                        break;
                    case "N":
                        status = TransactionsStatus.New;
                        break;
                    case "F":
                        status = TransactionsStatus.Failed;
                        break;
                    default:
                        status = TransactionsStatus.New;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new AtisWebServiceException(ex, json, this.ResponseHeaders);
            }

            return status;
        }

        /// <summary>
        /// Updates the status of an enrollment
        /// </summary>
        /// <param name="enrollments"></param>
        /// <returns></returns>
        public async Task<List<AtisTransaction>> UpdateEnrollment(List<EnrollmentUpdate> enrollments)
        {
            string transactionJson;
            string enrollmentJson = AtisJsonSerializer.Serialize<List<EnrollmentUpdate>>(enrollments);

            transactionJson = await this.SendRequest("enrollments?_method=put",HttpMethod.Put,enrollmentJson);
            List<AtisTransaction> response = (List<AtisTransaction>)AtisJsonSerializer.Deserialize<List<AtisTransaction>>(transactionJson);
            return response;
        }
              
        /// <summary>
        /// Configures HttpClient object 
        /// </summary>
        /// <param name="client"></param>
        private HttpClient ConfigureHttpClient()
        {
            WebRequestHandler handler = new WebRequestHandler();
            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection collection = store.Certificates.Find(X509FindType.FindBySubjectName, UserName, true);
            handler.ClientCertificates.Add(collection[0]);

            HttpClient newClient = new HttpClient(handler);
            //build client object
            newClient.BaseAddress = new Uri(URL);
            newClient.DefaultRequestHeaders.Accept.Clear();
            newClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //Set up client certificate
            
          
          
            //set up the authorization header
           // byte[] authorizationBytes = System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", UserName, Password).ToCharArray());
            //string authorizationHeaderValue = Convert.ToBase64String(authorizationBytes);
            //newClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authorizationHeaderValue);

            return newClient;
        }

        [Conditional("DEBUG")]
        private void WriteErrorMessage(Exception ex)
        {
            System.IO.File.WriteAllText("error.txt", string.Format("{0} \n\n {1}", ex.Message, ex.StackTrace));
        }       
    }
    public enum TransactionsStatus
    {
        /// <summary>
        /// Request received by ATIS and awaiting to be picked up by ATRRS
        /// </summary>
        New = 'N',
        /// <summary>
        /// Request received by ATIS and awaiting to be picked up by ATRRS
        /// </summary>
        Awaiting = 'A',
        /// <summary>
        /// Request successfully sent to ATRRS
        /// </summary>
        Sent = 'S',
        /// <summary>
        /// Request is queued for processing in ATRRS
        /// </summary>
        Queued = 'Q',
        /// <summary>
        /// Request processed by ATRRS. Outcome is unknown at this time.
        /// </summary>
        Complete = 'C',
        /// <summary>
        /// Request was succesfully processed in ATRRS and the ATIS data has been updated. This is the desired end state.
        /// </summary>
        Processed = 'P',
        /// <summary>
        /// Some error ocurred during processing.
        /// </summary>
        Error = 'E',        
        /// <summary>
        /// Some error ocurred during processing.
        /// </summary>
        Failed = 'F'        
    }
}