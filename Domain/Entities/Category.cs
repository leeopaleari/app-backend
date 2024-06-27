using Domain.Validation;

namespace Domain.Entities;

public sealed class Category : BaseEntity
{
    public string Name { get; private set; }

    public ICollection<Product> Products { get; set; }

    public Category(string name)
    {
        ValidateDomain(name);
    }

    public Category(int id, string name)
    {
        ValidateDomain(name);
        DomainExceptionValidation.When(id < 0, "Invalid ID value");
        Id = id;
    }

    public void Update(string name)
    {
        ValidateDomain(name);
    }

    private void ValidateDomain(string name)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
        DomainExceptionValidation.When(name.Length < 3, "Invalid name. Too short, minimum 3 characters.");

        Name = name;
    }
}