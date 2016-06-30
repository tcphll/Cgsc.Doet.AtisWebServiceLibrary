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
    public class Courses : AtisResponseMessage
    {
        /// <summary>
        /// List of courses returned.
        /// </summary>
        [DataMember(Name = "course")]
        public List<Course> CourseList { get; set; }
    }

    [DataContract]
    public class Course : IAtisMessage
    {
        /// <summary>
        /// Course number as identified in ATTRS
        /// </summary>
        [DataMember(Name = "crs")]
        public string CourseNumber { get; set; }
        /// <summary>
        /// School year of the course. ATTRS uses the calendar year
        /// </summary>
        [DataMember(Name = "fy")]
        public string SchoolYear { get; set; }
        /// <summary>
        /// Phase for this course.
        /// </summary>
        [DataMember(Name = "phase")]
        public string Phase { get; set; }
        /// <summary>
        /// School this course belongs to.
        /// </summary>
        [DataMember(Name = "sch")]
        public string School { get; set; }
    }
}
