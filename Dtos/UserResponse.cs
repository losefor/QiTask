using System.ComponentModel.DataAnnotations;
using QiTask.Models;

namespace QiTask.Dtos
{
    public class UserResponseDto
    {
        [Required] public string token { get; set; }
        
        [Required] public User user { get; set; }

    }
}