namespace Finance_it.API.Dtos.ApiResponsesDtos
{
    public class ApiResponseDto<T>
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public  T Data { get; set; } = default!;
        public List<String> Errors { get; set; } = new();

        public ApiResponseDto()
        {
            Success = true;
            Errors = new List<string>();
        }

        public ApiResponseDto(int statusCode, T data)
        {
            StatusCode = statusCode;
            Success = true;
            Data = data;
            Errors = new List<string>();
        }

        public ApiResponseDto(int statusCode, List<string> errors)
        {
            StatusCode = statusCode;
            Success = false;
            Errors = errors;
        }

        public ApiResponseDto(int statusCode, string error)
        {
            StatusCode = statusCode;
            Success = false;
            Errors = new List<string> { error };
        }
    }
}
