namespace Finance_it.API.Infrastructure.Exceptions
{
    public class BadRequestException : ApiException
    {
        public BadRequestException(string message) : base(StatusCodes.Status400BadRequest, message) { }
    }
}
