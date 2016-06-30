using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgsc.Doet.AtisWebServiceLibrary.ResponseClasses
{
    public class Enrollments : ResponseBase
    {
        public Enrollments()
        {
            this.Roster = new List<Enrollment>();
        }
        public Links Links { get; set; }
        public IList<Enrollment> Roster { get; set; }

    }

     public class Enrollment :IAtisItem
    {
        public int Id { get; set; }
        public string Edip { get; set; }
        public string cls { get; set; }
        public Links Link { get; set; }
    }
}
