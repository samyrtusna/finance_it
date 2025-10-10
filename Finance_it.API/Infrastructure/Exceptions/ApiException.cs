namespace Finance_it.API.Infrastructure.Exceptions
{
    public abstract class ApiException : Exception
    {
        public int StatusCode { get; }

        protected ApiException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
