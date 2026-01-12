using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Exceptions
{
    public  enum ErrorCode 
    {
       None = 0,
       DataBaseException = 1,
       ConnectionException = 2,
    }
}
