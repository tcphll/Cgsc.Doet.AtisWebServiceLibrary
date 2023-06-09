﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Cgsc.Doet.AtisWebServiceLibrary.MessageClasses
{
   
        [DataContract]
        public class AtisClasses :IAtisMessage
        {
            [DataMember(Name="class")]
            public List<AtisClass> AtisClassList { get; set; }
        }

        [DataContract]
        public class AtisClass :IAtisMessage
        {
            //This is a unique ID generated by the TCM-ATIS web service
            [DataMember(Name="id")]
            public int Id { get; set; }
            [DataMember(Name="title")]
            public string Title { get; set; }
            [DataMember(Name="cls")]
            public string ClassId { get; set; }
            [DataMember(Name="crs")]
            public string CourseId { get; set; }
            [DataMember(Name="fy")]
            public string SchoolYear { get; set; }
            [DataMember(Name="phase")]
            public string Phase { get; set; }
            [DataMember(Name="sch")]
            public string School { get; set; }
            [DataMember(Name="links")]
            public Links links { get; set; }
        }

    
}
