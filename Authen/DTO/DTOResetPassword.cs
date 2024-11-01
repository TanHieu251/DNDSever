using System.ComponentModel.DataAnnotations;

namespace DNDServer.Authen.DTO
{
    public class DTOResetPassword
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Token { get; set; }
        [Required]
        [MinLength(5)]
        public string? NewPassword { get; set; }
    }
}
