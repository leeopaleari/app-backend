using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EntitiesConfiguration;

public class ProductConfiguration : BaseEntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.HasKey(t => t.Id);

        builder.Property(p => p.Name).HasMaxLength(30).IsRequired();
        builder.Property(p => p.Description).HasMaxLength(50);
        builder.Property(p => p.Price).HasPrecision(10, 2).IsRequired();

        builder
            .HasOne(e => e.Category)
            .WithMany(e => e.Products)
            .HasForeignKey(e => e.CategoryId);
    }
}