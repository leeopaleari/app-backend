using Infra.Data.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EntitiesConfiguration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(p => p.FirstName)
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(p => p.LastName)
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(p => p.HomeBase)
            .IsRequired();
    }
}