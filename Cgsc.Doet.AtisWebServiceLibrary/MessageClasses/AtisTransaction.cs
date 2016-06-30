using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Cgsc.Doet.AtisWebServiceLibrary.MessageClasses
{
    /// <summary>
    /// Represents the results of a transaction as a result of a POST or PUT operation to the ATIS API
    /// </summary>
    [DataContract]    
    class AtisTransaction_ :IAtisMessage
    {
        /// <summary>
        /// Indicates whether the request was successfully processed
        /// </summary>
        [DataMember (Name="isSucccess")]    
        public bool IsSuccess { get; set; }       
        /// <summary>
        /// Transaction ID GUID
        /// </summary>
        [DataMember(Name = "transId")]
        public string TransactionId { get; set; }
        /// <summary>
        /// Response code returned by the API
        /// </summary>
        [DataMember(Name = "responseCd")]
        public int ResponseCode { get; set; }
        /// <summary>
        /// Process Code returned by the API
        /// </summary>
        [DataMember(Name = "processCd")]
        public string ProcessCode { get; set; }
        /// <summary>
        /// Database ID of the transaction.
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }
    }
}
