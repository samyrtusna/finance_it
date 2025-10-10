namespace Finance_it.API.Infrastructure.Exceptions
{
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string message) : base(StatusCodes.Status401Unauthorized, message) { }
    }
}
