// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2017-3-24</date>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace BenutzerverwaltungBL.Controller
{
    public class JsonConverter
    {
        public static string serializeData<T>( T o )
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
           
            ser.WriteObject(stream1 , o);

            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            return sr.ReadToEnd();
        }

        public static T deserializeData<T>( string json )
        {

            T o = Activator.CreateInstance<T>();

            using ( MemoryStream stream1 = new MemoryStream(Encoding.Unicode.GetBytes(json)) )
            {

                DataContractJsonSerializer ser = new DataContractJsonSerializer(o.GetType());

                o = ( T ) ser.ReadObject(stream1);
            }


            return o;


        }
    }
}
