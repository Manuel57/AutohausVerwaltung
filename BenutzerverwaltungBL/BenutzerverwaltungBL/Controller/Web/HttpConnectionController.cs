// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2017-3-24</date>

using BenutzerverwaltungBL.Model.BusinesObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BenutzerverwaltungBL.Controller.Web
{
    public class HttpConnectionController
    {

        private static HttpConnectionController instance;

        private HttpConnectionController( ) { }

        public static HttpConnectionController Instance
        {
            get
            {
                if ( instance == null )
                {
                    instance = new HttpConnectionController();
                }
                return instance;
            }
        }

        public string CreateUser(string json)
        {
            string user = null;

            user = request(json , "application/json" , "POST");

            return user;
        }

        private string request(string jsonString, string contentType, string method)
        {
            string result = "";

            WebRequest request = WebRequest.Create(
                "https://manuel57.ddns.net/AutohausWebservice/rest/user/createpasswd");

            request.Method = method;
            request.ContentType = contentType;
            request.Headers.Add(HttpRequestHeader.Authorization , "Basic " + Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("SmZ8mz9r54kgb7xc:TPRTdcTkpL9jn67c")));
            StreamWriter sw = new StreamWriter(request.GetRequestStream());
            sw.WriteLine(jsonString);
            sw.Close();
            WebResponse response = request.GetResponse();
            switch ( (response as HttpWebResponse).StatusCode )
            {
                case HttpStatusCode.OK:
                    result = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    break;
                default:
                    break;
            }
            return result;
        }

    }
}
