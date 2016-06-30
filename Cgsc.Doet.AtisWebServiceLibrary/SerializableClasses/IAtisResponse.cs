using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgsc.Doet.AtisWebServiceLibrary.ResponseClasses
{
    public interface IAtisResponse
    {
        
         int total { get; set; }
         int limit { get; set; }
         int offest { get; set; }
    }
}
