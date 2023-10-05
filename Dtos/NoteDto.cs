using System.ComponentModel.DataAnnotations;

namespace QiTask.Dtos;

public class NoteDto
{
    [Required]
    public string? Title { get; set; }
}