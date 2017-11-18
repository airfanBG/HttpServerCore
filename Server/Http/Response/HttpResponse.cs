using HTTPServer.Server.Common;
using HTTPServer.Server.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HTTPServer.Server.Http.Response
{
    public abstract class HttpResponse
    {
        private readonly IView view;

        public HttpResponse()
        {
            this.HeaderCollection = new HttpHeaderCollection();
        }
        protected HttpResponse(string redirectUrl):this()
        {
            CoreValidator.ThrowIfNullOrEmpty(redirectUrl, nameof(redirectUrl));
            this.HeaderCollection = new HttpHeaderCollection();
            this.StatusCode = HttpStatusCode.Found;
            this.AddHeader("Location", redirectUrl);
        }

       
        protected HttpResponse(HttpStatusCode statusCode,IView view):this()
        {
           
            this.view = view;
            this.HeaderCollection = new HttpHeaderCollection();
            this.StatusCode = statusCode;
        }
        private HttpHeaderCollection HeaderCollection { get; set; }
        private HttpStatusCode StatusCode { get; set; }
        private string StatusMessage => this.StatusCode.ToString();

        public override string ToString()
        {
            var response = new StringBuilder();
            var statusCode = (int)this.StatusCode;

            response.AppendLine($"HTTP/1.1 {this.StatusCode} {this.StatusMessage}");
            response.AppendLine(this.HeaderCollection.ToString());
            response.AppendLine();

            if (statusCode<100||statusCode>399)
            {
                response.AppendLine(this.view.View());
            }
            return response.ToString();
        }
        private void AddHeader(string v, string redirectUrl)
        {
            throw new NotImplementedException();
        }

    }
}
