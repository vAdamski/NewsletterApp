using System.ComponentModel.DataAnnotations;

namespace NewsletterApp.Shared.Models.DTO;

public class CreateNewsLetterDto
{
    [Required]
    public string Name { get; set; }
}