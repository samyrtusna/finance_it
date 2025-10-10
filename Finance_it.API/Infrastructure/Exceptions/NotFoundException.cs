namespace Finance_it.API.Infrastructure.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string message) : base(StatusCodes.Status404NotFound, message) { }
    }
}
