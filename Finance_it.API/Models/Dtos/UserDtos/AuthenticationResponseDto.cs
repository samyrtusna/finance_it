using Finance_it.API.Data.Entities;

namespace Finance_it.API.Models.Dtos.UserDtos
{
    public class AuthenticationResponseDto
    {
        public string? Name { get; set; } = default!;
        public Role? Role { get; set; }
        public string AccessToken { get; set; } = default!;
    }
}
