namespace Finance_it.API.Models.Dtos.UserDtos
{
    public class AuthenticationResponseDto
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;

    }
}
