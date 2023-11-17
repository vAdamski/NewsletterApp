using System.ComponentModel.DataAnnotations;

namespace NewsletterApp.Models.DTO;

public class CreateNewsLetterDto
{
    [Required]
    public string Name { get; set; }
}