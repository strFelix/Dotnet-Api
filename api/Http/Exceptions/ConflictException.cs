using System.Net;

namespace api.Http.Exceptions
{
    public class ConflictException : BaseException
    {

        public ConflictException(string message) :
        base
            (
                "0-001",
                message,
                HttpStatusCode.Conflict,
                StatusCodes.Status409Conflict,
                null,
                DateTimeOffset.UtcNow,
                null
            )
        { }

    }
}