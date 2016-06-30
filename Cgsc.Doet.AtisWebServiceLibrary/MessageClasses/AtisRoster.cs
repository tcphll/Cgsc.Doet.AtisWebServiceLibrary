using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Cgsc.Doet.AtisWebServiceLibrary.MessageClasses
{
    [DataContract]
    public class AtisRosters :IAtisMessage
    {
        [DataMember(Name="roster")]
        public List<AtisRoster> AtisRosterList { get; set; }
    }
    [DataContract]
    public class AtisRoster :IAtisMessage
    {
        [DataMember(Name="id")]
        public int Id { get; set; }
        [DataMember(Name="edipi")]
        public string Edipi { get; set; }
        [DataMember(Name="cls")]
        public string ClassId { get; set; }
        public Links links { get; set; }
    }
}
