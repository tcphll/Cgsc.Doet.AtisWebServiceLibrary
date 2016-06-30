using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Cgsc.Doet.AtisWebServiceLibrary.MessageClasses
{
    [DataContract]
    public abstract class AtisResponseMessage : IAtisMessage
    {
        /// <summary>
        /// Total number of records returned by the API
        /// </summary>
        [DataMember]
        public int Total { get; set; }        
    }   
}
