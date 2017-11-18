using System;
using System.Collections.Generic;
using System.Text;

namespace HTTPServer.Server.Http.Contracts
{
    public interface IHttpContext
    {
        IHttpContext Request { get; }
    }
}
