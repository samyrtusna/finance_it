using System.ComponentModel.DataAnnotations;

namespace Finance_it.API.Models.Dtos.UserDtos
{
    public class PasswordChangeRequestDto
    {
        [Required (ErrorMessage ="Current password is required.")]
        public string CurrentPassword { get; set; }

        [Required (ErrorMessage ="New password is required.")]
        public string NewPassword { get; set; }
    }
}
