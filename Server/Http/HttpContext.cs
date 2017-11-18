using HTTPServer.Server.Common;
using HTTPServer.Server.Http.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTTPServer.Server.Http
{
    public class HttpContext : IHttpContext
    {
        private readonly IHttpContext request;

        public HttpContext(string requestStr)
        {
            CoreValidator.ThrowIfNull(requestStr, nameof(requestStr));
            this.request = new HttpContext(requestStr);
        }
        public IHttpContext Request => this.request;
    }
}
