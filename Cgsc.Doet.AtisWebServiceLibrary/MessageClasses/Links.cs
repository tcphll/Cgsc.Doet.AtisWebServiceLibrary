using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Cgsc.Doet.AtisWebServiceLibrary.MessageClasses
{
    [DataContract (Name="links")]
    public class Links
    {
     
        public Links()
        {
            this.Link = new List<Link>();
        }
        [DataMember (Name="link")]
        public IList<Link> Link { get; set; }
    }

    [DataContract]
    public class Link
    {
        [DataMember]
        public string rel { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string href { get; set; }
    }
    
}


