using System.ComponentModel.DataAnnotations;

namespace QiTask.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required] public string FullName { get; set; }

        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }

        // Navigation property for one-to-many relationship
        public ICollection<Note> Notes { get; set; }
    }
}