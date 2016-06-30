using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using Cgsc.Doet.AtisWebServiceLibrary.MessageClasses;

namespace Cgsc.Doet.AtisWebServiceLibrary
{
    public static class AtisJsonSerializer
    {
        public static string Serialize<T>(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, (T)obj);
            StreamReader sr = new StreamReader(ms);
            ms.Position = 0;
            string json = sr.ReadToEnd();

            return json;
        }

        public static object Deserialize<T>(string json)
        {
            MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(json));
            ms.Position = 0;
            DataContractJsonSerializerSettings s = new DataContractJsonSerializerSettings();
            
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T), s);            
            
            object ret = serializer.ReadObject(ms);

            return ret;
        }
    }    
}