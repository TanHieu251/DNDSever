using System.ComponentModel.DataAnnotations;

namespace DNDServer.Authen.DTO
{
    public class DTOForgotPassword
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
