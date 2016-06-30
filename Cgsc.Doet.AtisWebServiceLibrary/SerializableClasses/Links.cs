using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgsc.Doet.AtisWebServiceLibrary.ResponseClasses
{
    public class Links
    {
        public Links()
        {
            this.Link = new List<Link>();
        }
        public IList<Link> Link { get; set; }
    }

     public class Link
    {
        public Link() { }
        public string rel { get; set; }
        public string title { get; set; }
        public string href { get; set; }
    }
    
}


