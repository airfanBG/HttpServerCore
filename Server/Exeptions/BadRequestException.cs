using System;
using System.Collections.Generic;
using System.Text;

namespace HTTPServer.Server.Exeptions
{
    public class BadRequestException:Exception
    {
        public BadRequestException(string exception):base(exception)
        {
            
        }
    }
}
