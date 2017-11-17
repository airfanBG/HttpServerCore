using System;
using System.Collections.Generic;
using System.Text;

namespace HTTPServer.Server.Enums
{
    public enum HttpStatusCode
    {
        Ok=100,
        MovedPermanently=301,
        Found=302,
        MovedTemporary=303,
        NotAuthorized=401,
        NotFound=404,
        InternalServerError=500
    }
}
