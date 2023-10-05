using System.ComponentModel.DataAnnotations;

namespace QiTask.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}