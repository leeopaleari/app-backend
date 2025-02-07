using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Category;

public class CreateCategoryDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "The Name is required")]
    [MinLength(3)]
    [MaxLength(100)]
    [DisplayName("Name")]
    public string Name { get; set; }
}