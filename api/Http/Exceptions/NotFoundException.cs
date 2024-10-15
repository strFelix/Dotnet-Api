using System.Net;

namespace api.Http.Exceptions
{
    public class NotFoundException : BaseException
    {

        public NotFoundException(string message, string path) :
        base
            (
                "0-002",
                message,
                HttpStatusCode.NoContent,
                StatusCodes.Status404NotFound,
                path,
                DateTimeOffset.UtcNow,
                null
            )
        { }

    }
}