using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgsc.Doet.AtisWebServiceLibrary.ResponseClasses 
{
    public class ResponseBase : IAtisResponse
    {
        public int total { get; set; }
        public int limit { get; set; }
        public int offest { get; set; }
        public string Json { get; set; }

        public static IAtisResponse GetResponseObject(string Method)
        {
            dynamic returnType;
            switch (Method)
            {
                case "courses":
                    returnType = new Courses();
                    break;
                case "enrollments":
                    returnType = new Enrollments();
                    break;
                default:
                    returnType = new ResponseBase();
                    break;
            }
            return returnType;
        }
    }
}
