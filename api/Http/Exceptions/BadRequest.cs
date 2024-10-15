using System.Net;

namespace api.Http.Exceptions
{
    public class BadRequest : BaseException
    {

        public BadRequest(string message) :
        base
            (
                "0-003",
                message,
                HttpStatusCode.BadRequest,
                StatusCodes.Status400BadRequest,
                null,
                DateTimeOffset.UtcNow,
                null
            )
        { }

    }
}