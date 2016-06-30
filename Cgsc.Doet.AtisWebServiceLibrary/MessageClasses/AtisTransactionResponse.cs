using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Cgsc.Doet.AtisWebServiceLibrary.MessageClasses
{
    //Represents a response returned from ATIS when attempting to create a transaction.
    [DataContract]
    public class AtisTransaction
    {
        [DataMember(Name="id")]
        public int Id { get; set; }
        [DataMember(Name = "isSuccess")]
        public bool IsSuccess { get; set; }
        [DataMember(Name = "transId")]
        public string TransactionId { get; set; }
        [DataMember(Name = "responseCd")]
        public string ResponseCode { get; set; }
        [DataMember(Name = "processCd")]
        public string ProcessCode { get; set; }
        [DataMember(Name="trackingId")]
        public string TrackingID { get; set;}
        

    }
}
