using System.ComponentModel.DataAnnotations;

namespace QiTask.Models
{
    public class Note
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime LastUpdated { get; set; }
        
        // Foreign key to establish the relationship
        public int UserId { get; set; }

        // Navigation property back to the User
        public User User { get; set; }
    }
}