namespace Application.DTOs.Product;

public class ReadProductDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string Image { get; set; }

    public CategoryDTO Category { get; set; }

    public int CategoryId { get; set; }
}