using CovidInfoWebApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CovidInfoWebApp.Utilities
{
    public static class UtilityClass
    {
       static string BaseAddress = "https://localhost:44361/";
        public static TokenClass GetToken(LoginModel model)
        {
            var client = new RestClient(BaseAddress);
            var request = new RestRequest("api/Authenticate/login", Method.POST);
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            request.RequestFormat = DataFormat.Json;
            string body = JsonConvert.SerializeObject(model);
            request.AddJsonBody(body);
            IRestResponse queryResult = client.Execute(request);
            if (queryResult.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<TokenClass>(queryResult.Content);
            }
            return null;
        }

        
        public static Response WebRequestWithToken<T>(string address, Method method, T model,Dictionary<string,object> parameters, string token)
        {
            var client = new RestClient(BaseAddress);
            var request = new RestRequest(address, method);
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            if (token !=null)
            {
                client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token));
            }
           
            if (model !=null)
            {
                request.RequestFormat = DataFormat.Json;
                string body = JsonConvert.SerializeObject(model);
                request.AddJsonBody(body);
            }

            if (parameters!=null && parameters.Count != 0)
            {
                foreach (var item in parameters)
                {
                    request.AddParameter(item.Key,item.Value);
                }
            }

            IRestResponse queryResult = client.Execute(request);
            if (queryResult.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<Response>(queryResult.Content);
            }
            return null;
        }
        
        //public void WebRequest(string address, Method method,string token)
        //{
        //    var client = new RestClient(BaseAddress);
        //    var request = new RestRequest(address, method);
        //    request.RequestFormat = DataFormat.Json;
        //  //  request.AddParameter("token", "saga001", ParameterType.UrlSegment);
        //    request.AddHeader("content-type", "application/json");
        //    if (method == Method.GET)
        //    {
        //       // request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
        //        var queryResult = client.Execute(request);
        //    }

        //    //request.AddBody(new Item
        //    //{
        //    //    ItemName = someName,
        //    //    Price = 19.99
        //    //});
        //    //client.Execute(request);

        //}
    }

    public class TokenClass
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
