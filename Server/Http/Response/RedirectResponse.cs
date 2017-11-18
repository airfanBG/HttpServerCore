using System;
using System.Collections.Generic;
using System.Text;

namespace HTTPServer.Server.Http.Response
{
    public class RedirectResponse:HttpResponse
    {
        public RedirectResponse(string redirectUrl):base(redirectUrl)
        {

        }
    }
}
