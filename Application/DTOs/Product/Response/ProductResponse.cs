using Application.DTOs.Category;

namespace Application.DTOs.Product.Response;

public class ProductResponse
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string Image { get; set; }

    // [IgnoreDataMember] // usado quando for um unico DTO e precisa ignorar a propriedade de navegação
    public ReadCategoryDto Category { get; set; }

    public int CategoryId { get; set; }
}