using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Cgsc.Doet.AtisWebServiceLibrary
{
    public class AtisWebServiceException  : Exception 
    {
        public AtisWebServiceException(Exception ex, string json) : base()    
        {
            _baseException = ex;
            _json = json;
            
        }
        public AtisWebServiceException(Exception ex, string json, HttpResponseHeaders responseHeaders)
            : base()
        {
            _baseException = ex;
            _json = json;
            _responseHeaders = responseHeaders;

        }
        private Exception _baseException;
        public Exception BaseException
        {
            get
            {
                return _baseException;
            }
        }
        private string _json;
        public string Json
        {
            get
            {
                return _json;
            }
        }

        private HttpResponseHeaders _responseHeaders;
        public string ResponseHeaders
        {
            get
            {
                return _responseHeaders != null ? _responseHeaders.ToString() : "";
                
            }
        }
    }
}
