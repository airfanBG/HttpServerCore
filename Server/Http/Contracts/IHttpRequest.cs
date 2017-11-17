using System;
using System.Collections.Generic;
using System.Text;

namespace HTTPServer.Server.Http.Contracts
{
    public interface IHttpRequest
    {
        Dictionary<string, string> FormData { get; }
        HttpHeaderCollection HttpHeaderCollection { get; }
        string Path { get; }
        Dictionary<string, string> QueryParameters { get; }
        string Url { get; }
        Dictionary<string, string> UrlParameters { get; }
        void AddUrlParameter(string key, string value);
    }
}
