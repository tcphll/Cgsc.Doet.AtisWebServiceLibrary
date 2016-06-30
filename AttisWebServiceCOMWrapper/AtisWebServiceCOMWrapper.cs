using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgsc.Doet.AtisWebServiceLibrary
{
    public class AtisWebServiceCOMWrapper
    {
        private const string URL = @"https://interfacestest.atsc.army.mil/transcript-ws/api/";
        private const string UserName = "system_sms";
        private const string Password = "Sm3s1$tE*+_2014";

        public async Task<string> TestRequest()
        {
            string retval = "nothing";
            try
            {
                RestRequester req = RestRequester.GetRestRequester(UserName, Password, URL);

                retval = await req.SendRequest("courseaccess", System.Net.Http.HttpMethod.Get);                
            }
            catch(Exception ex)
            {
                retval = ex.Message;
            }
            return retval;
        }

        public string Test()
        {
            string retval = "Nothing was returned";
            retval = TestRequest().Result;
            return retval;
        }

        public bool CheckEnrollmentStatus(string crs, string edipi, string cls = "")
        {
            RestRequester req = RestRequester.GetRestRequester(UserName, Password, URL);
            bool retval = false;

            retval = req.VerifyEnrollment(crs, edipi, cls).Result;

            return retval;
        }

        public string GetEnrolledClassId(string crs, string edipi, string fy)
        {
            RestRequester req = RestRequester.GetRestRequester(UserName, Password, URL);
            string classId = "";
            classId = req.GetEnrolledClass(crs, edipi, fy).Result;
            return classId;
        }
        
    }
}
