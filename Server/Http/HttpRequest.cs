using HTTPServer.Server.Common;
using HTTPServer.Server.Enums;
using HTTPServer.Server.Exeptions;
using HTTPServer.Server.Http.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace HTTPServer.Server.Http
{
    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));
            FormData = new Dictionary<string, string>();
            HttpHeaderCollection = new HttpHeaderCollection();
            QueryParameters = new Dictionary<string, string>();
            UrlParameters = new Dictionary<string, string>();
            this.ParseRequest(requestString);
        }


        public Dictionary<string, string> FormData {get;}

        public HttpHeaderCollection HttpHeaderCollection { get; private set; }

        public string Path { get; private set; }

        public Dictionary<string, string> QueryParameters {get;}

        public string Url { get; private set; }

        public Dictionary<string, string> UrlParameters {get;}

        public HttpRequestMethod Method { get; private set; }

        public void AddUrlParameter(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));
            this.UrlParameters[key] = value;
        }

        private void ParseRequest(string requestString)
        {
            string[] requestLines = requestString.Split(new[] { Environment.NewLine }, StringSplitOptions.None );
            
            if (!requestLines.Any())
            {
                throw new BadRequestException("Request is not valid");
            }

            string[] requestLine = requestLines.First()
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);


            if (requestLine.Length!=3||requestLine[2].ToLower()!="http/1.1")
            {
                throw new BadRequestException("Invalid request line");
            }
            this.Url = requestLine[1];
            this.Method = ParseMethod(requestLine.First());
            this.Path = this.ParsePath(this.Url);
            this.ParseHeaders(requestLines);
            this.ParseParameters();
            this.ParseFormData(requestLines.Last());
        }

        private void ParseFormData(string formDataLine)
        {
            if (this.Method==HttpRequestMethod.Get)
            {
                return;
            }
            ParseQuery(formDataLine,this.QueryParameters);
        }

        private void ParseParameters()
        {
            if (!this.Url.Contains('?'))
            {
                return;
            }
            var query = this.Url
              .Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries).Last();

            ParseQuery(query, this.UrlParameters);
        }

        private HttpRequestMethod ParseMethod(string method)
        {
            try
            {
                return Enum.Parse<HttpRequestMethod>(method, true);
            }
            catch (Exception)
            {

                throw new BadRequestException("Invalid method");
            }
        }

        private string ParsePath(string url)
        {
            return url.Split(new[] { '?', '#' }, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        private void ParseHeaders(string[] requestLines)
        {
            var emptyLineAfterHeadersIndex = Array.IndexOf(requestLines, string.Empty);
            for (int i = 1; i < emptyLineAfterHeadersIndex; i++)
            {
                var currentLine = requestLines[1];
                var headerParts = currentLine.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries );

                var headerKey = headerParts[0];
                var headerValue = headerParts[1].Trim() ;
                if (headerParts.Length!=2)
                {
                    throw new BadRequestException("Invalid request");
                }
                var header = new HttpHeader(headerKey, headerValue);
                this.HttpHeaderCollection.Add(header);
                if (!this.HttpHeaderCollection.ContainsKey("Host"))
                {
                    throw new BadRequestException("Invalid host!");
                }
            }
        }
        private void ParseQuery(string query,IDictionary<string,string> dict)
        {
           
            if (!query.Contains('='))
            {
                return;
            }
            var queryPairs = query.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var queryPair in queryPairs)
            {
                var queryKvp = queryPair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (queryKvp.Length != 2)
                {
                    return;
                }


                var queryKey = WebUtility.UrlDecode(queryKvp[0]);
                var queryValue = WebUtility.UrlDecode(queryKvp[1]);
                dict.Add(queryKey, queryValue);
            }
        }
    }
}
