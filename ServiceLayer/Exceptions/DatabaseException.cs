using System.Net;

namespace ServiceLayer.Exceptions
{
    public class DatabaseException : BaseException
    {
        public DatabaseException()
            : base("Occurred an error in the database",  HttpStatusCode.InternalServerError)
        {
        }
    }
}
