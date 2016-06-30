using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgsc.Doet.AtisWebServiceLibrary.ResponseClasses
{
    public class Courses : ResponseBase
    {
        public int offset { get; set; }
        public Links links { get; set; }
        public List<Course> course { get; set; }
    }

    public class Course :IAtisItem
    {
        public int Id { get; set; }
        public string crs { get; set; }
        public string fy { get; set; }
        public string sch { get; set; }
        public Links links { get; set; }
        public string phase { get; set; }
    }

}
