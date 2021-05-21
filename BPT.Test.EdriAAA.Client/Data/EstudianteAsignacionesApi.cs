using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace BPT.Test.EdriAAA.Client.Data
{
    class EstudianteAsignacionesApi
    {
        public dynamic Post(string url, string json, string auth = null)
        {
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                if (auth != null)
                {
                    request.AddHeader("Authorization", auth);
                }

                IRestResponse response = client.Execute(request);

                dynamic datos = JsonConvert.DeserializeObject(response.Content);

                return datos;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public dynamic Put(string url, string json, string auth = null)
        {
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.PUT);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                if (auth != null)
                {
                    request.AddHeader("Authorization", auth);
                }

                IRestResponse response = client.Execute(request);

                dynamic datos = JsonConvert.DeserializeObject(response.Content);

                return datos;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public dynamic Get(string url)
        {
            HttpWebRequest myrequestweb = (HttpWebRequest)WebRequest.Create(url);
            myrequestweb.UserAgent = ".NET Framework Test Client";
            myrequestweb.Credentials = CredentialCache.DefaultCredentials;
            myrequestweb.Proxy = null;
            HttpWebResponse myhttpwebresponse = (HttpWebResponse)myrequestweb.GetResponse();
            Stream mystream = myhttpwebresponse.GetResponseStream();
            StreamReader mystreamReader = new StreamReader(mystream);

            string datos = HttpUtility.HtmlDecode(mystreamReader.ReadToEnd());


            dynamic data = JsonConvert.DeserializeObject(datos);

            return data;
        }

        public dynamic Delete(string url, string json, string auth = null)
        {
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                if (auth != null)
                {
                    request.AddHeader("Authorization", auth);
                }

                IRestResponse response = client.Execute(request);

                dynamic datos = JsonConvert.DeserializeObject(response.Content);

                return datos;
            }
            catch (Exception ex)
            {
                return null;
            }


        }
    }
}
