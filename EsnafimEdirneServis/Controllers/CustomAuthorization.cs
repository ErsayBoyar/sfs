using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace EsnafimEdirneServis.Controllers
{
    public class CustomAuthorization : AuthorizeAttribute
    {
        
        public class Error
        {
            public string Status { get; set; }
            public string Message { get; set; }
           
        }
        
       
        protected override  void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            
            var myError = new Error
            {
                Status = "403",
                Message = "Geçersiz Authorize Token",
               
            };

            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Forbidden,
                Content = new ObjectContent<Error>(myError, new JsonMediaTypeFormatter())
            };

        }
    }
}