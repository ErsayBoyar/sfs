using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    
        
    public class CreateTokenController : ApiController
    {
        public class ErrorResult : IHttpActionResult
        {
            Error _error;
            HttpRequestMessage _request;

            public ErrorResult(Error error, HttpRequestMessage request)
            {
                _error = error;
                _request = request;
            }
            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage()
                {
                    Content = new ObjectContent<Error>(_error, new JsonMediaTypeFormatter()),
                    RequestMessage = _request
                };
                return Task.FromResult(response);
            }
        }

        public class Error
        {
            public string Status { get; set; }
            public string Message { get; set; }
            public List<Data> Data { get; set; }
        }
        Error myError;
        public class Data
        {
            public string access_token { get; set; }
            public string expires_in { get; set; }
            public DateTime expires { get; set; }

        }
        public async Task<IHttpActionResult> Get(string user,string pass)
        {
           
            string URL = "http://localhost:81/token";
            System.Net.WebRequest webRequest = System.Net.WebRequest.Create(URL);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            Stream reqStream = webRequest.GetRequestStream();
            string postData = "grant_type=password&username="+user+"&password="+pass+"";
            byte[] postArray = Encoding.ASCII.GetBytes(postData);
            reqStream.Write(postArray, 0, postArray.Length);
            reqStream.Close();
            StreamReader sr = new StreamReader(webRequest.GetResponse().GetResponseStream());
            string Result = sr.ReadToEnd();
            dynamic obj = JsonConvert.DeserializeObject<dynamic>(Result);
            DateTime tarih = DateTime.Now.AddDays(1);
            var myError = new Error
            {
                Status = "200",
                Message = "İşlem Başarılı",
                Data = new List<Data>
                {
                    new Data { access_token=obj["access_token"], expires_in=obj["expires_in"],expires=tarih },
                    
                }
            };

            return new ErrorResult(myError, Request);
        }
    }
}
