using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using Cgsc.Doet.AtisWebServiceLibrary;
using Cgsc.Doet.AtisWebServiceLibrary.MessageClasses;

namespace AtisWebServiceTest
{

    public partial class Main : Form
    {
        //dev
        private const string URL = @"https://interfacestest.atsc.army.mil/transcript-ws/api/";
        private const string UserName = "cgscstage.leavenworth.army.mil";
        //private const string UserName = "system_sms";
        private const string Password = "Sm3s1$tE*+_2014";
        //production
        //private const string URL = @"https://interfaces.atsc.army.mil/transcript-ws/api/";
        //private const string UserName = "system_sms";
       // private const string Password = "$m$P30d_2o!5aUg";
        public Main()
        {
            InitializeComponent();
        }

        private async void _btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                _btnSend.Enabled = false;
                RestRequester req = RestRequester.GetRestRequester(UserName, Password, URL);

                if (cmbType.SelectedIndex == 0)
                {
                    //string json = await req.SendRawGetRequestAsync(txtCustomUri.Text);
                    string json = await req.SendRequest(txtCustomUri.Text, HttpMethod.Get);
                    txtJson.Text = json;
                    txtResponseHeaders.Text = req.ResponseHeaders.ToString();
                }
                else if (cmbType.SelectedIndex == 1)
                {
                    string retType = "";
                    List<EnrollmentUpdate> enr = new List<EnrollmentUpdate>()
                {
                    new EnrollmentUpdate()
                    {
                       StatusUpdateID = 1234,
                        EDIPI = "1111111111",
                        SchoolYear = "2015",
                        School = "704W",
                        CourseId = "1-250-C62",
                        ClassId = "004",
                        Phase = "2",
                        StatusCode = "C",
                        StatusType = "I",
                        ReasonCode = "",
                        QuotaStatus = "1C",
                        InputDate = "2015-03-09T00:00:00-0500"
                        
                    }
                };

                    retType = AtisJsonSerializer.Serialize<List<EnrollmentUpdate>>(enr);
                    txtJson.Text = retType;
                    retType = await req.SendRequest(txtCustomUri.Text, HttpMethod.Post, retType);
                    txtJson.Text += "\n\n" + retType;
                }
                else if (cmbType.SelectedIndex == 2)
                {
                    string retType = "";
                    List<EnrollmentUpdate> enr = new List<EnrollmentUpdate>()
                {
                   
                    //new EnrollmentUpdate()
                    //{
                    //    EDIPI = "1237665500",
                    //    SchoolYear = "2015",
                    //    School = "704W",
                    //    CourseId = "1-250-C60",
                    //    ClassId = "007",                        
                    //    Phase = "2",
                    //    StatusCode = "D",
                    //    StatusType = "3",
                    //    StatusDate = "2015-05-10",
                    //    ReasonCode = "DW"                        
                    //}

                    new EnrollmentUpdate()
                    {
                        EDIPI = "1279897052",
                        SchoolYear = "2019",
                        School = "701",
                        CourseId = "701-1-250-ILE-CC (DL)",
                        ClassId = "001",                        
                        Phase = "1",
                        StatusCode = "G",
                        StatusType = "3",
                        StatusDate = "2019-11-12",
                        TrackingID = "257634",
                       // ReasonCode = "G"
                        
                    }//,
                    //new EnrollmentUpdate()
                    //{
                    //    StatusUpdateID = 1234,
                    //    EDIPI = "1393072098",
                    //    SchoolYear = "2017",
                    //    School = "704W",
                    //    CourseId = "1-250-C60 (DL) (PI)",
                    //    ClassId = "001",                        
                    //    Phase = "1",
                    //    StatusCode = "Z",
                    //    StatusType = "3",
                    //    StatusDate = "2017-2-7",
                    //    TrackingID = "150526",
                    //    ReasonCode = "HZ"
                        
                    //}
                };

                    retType = AtisJsonSerializer.Serialize<List<EnrollmentUpdate>>(enr);
                    txtJson.Text = retType;
                    List<AtisTransaction> transResponse = await req.UpdateEnrollment(enr);
                    retType = AtisJsonSerializer.Serialize<List<AtisTransaction>>(transResponse);
                    //retType = await req.SendRequest(txtCustomUri.Text, HttpMethod.Put, retType);
                    txtJson.Text += "\n\r\n\r" + retType;
                }
                else if (cmbType.SelectedIndex == 3)
                {
                    TransactionsStatus status = await req.GetTransactionStatus(Convert.ToInt32(txtCustomUri.Text));
                    txtJson.Text = status.ToString();

                }
                else if (cmbType.SelectedIndex == 4)
                {
                    //bool enrollmentStatus = await req.VerifyEnrollment("1-250-C60", "1237665500", "007");
                    bool enrollmentStatus = await req.VerifyEnrollment("1-250-C60 (DL) (PI)", txtCustomUri.Text, "2017");
                    txtJson.Text = enrollmentStatus.ToString();
                }
                //Get class ID for enrollment
                else if (cmbType.SelectedIndex == 5)
                {
                    string classId = await req.GetEnrolledClass("1-250-C61", "1013508638", "2016");
                    txtJson.Text = classId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\r\n\r" + ex.StackTrace);

            }
            finally
            {
                _btnSend.Enabled = true;
            }

        }

        private void btnDeserialize_Click(object sender, EventArgs e)
        {
            Enrollment enr = (Enrollment)AtisJsonSerializer.Deserialize<Enrollment>(txtJson.Text);

            MessageBox.Show(enr.EDIPI);
        }
    }
}
