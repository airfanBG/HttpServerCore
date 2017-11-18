using HTTPServer.Server.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HTTPServer.Server.Http.Response
{
    public class ViewResponse:HttpResponse
    {
        private readonly IView view;

        public ViewResponse(HttpStatusCode statusCode,IView view):base(statusCode,view)
        {
            this.StatusCode = statusCode;
            this.view = view;
        }
        public HttpStatusCode StatusCode { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()} {this.view.View()}";
        }
    }
}
