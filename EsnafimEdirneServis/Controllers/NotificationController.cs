using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class NotificationController : ApiController
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
        public async Task<IHttpActionResult> Post(string uid, string mesaj)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add("Authorization:key=AAAAOFW548c:APA91bHqbTPfPyfSFG3dNxEubrT4wg4Xs-yjva24NUVeCCVYL0DCElz4cxED2eD5COQRWoYX1oDeH5soqjwHGk6jYA6kea2l0fnEWA4oGuIffhZ-qpNjfwbc6m3qrbXd_HmSls4EHwi2");
            httpWebRequest.Method = "POST";
            using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                
                
                    string json = string.Concat(new object[] { "{\"notification\": {\"title\":\"Esnafım Edirne\",\"body\": \"", mesaj, "\",\"badge\": 0,\"sound\": \"default\",\"click_action\": \"FCM_PLUGIN_ACTIVITY\"},\"to\" : \"", uid, "\",\"content_available\": true}" });
               
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            using (StreamReader streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream()))
            {
                streamReader.ReadToEnd();
            }
            var myError = new Error
            {
                Status = "200",
                Message = "İşlem Başarılı",
                Data = null
            };

            return new ErrorResult(myError, Request);

        }
    }
}
