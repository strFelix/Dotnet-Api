using System.Net;

namespace api.Http.Exceptions
{
    public class ConflictException : BaseException
    {

        public ConflictException(string message, string path) :
        base
            (
                "0-001",
                message,
                HttpStatusCode.Conflict,
                StatusCodes.Status409Conflict,
                path,
                DateTimeOffset.UtcNow,
                null
            )
        { }

    }
}