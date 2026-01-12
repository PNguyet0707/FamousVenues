using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Exceptions
{
    public class ConnectionException : BaseException
    {
        public ConnectionException() :base("An error occured when connect with database", HttpStatusCode.NotAcceptable)
        {
        }
    }
}
