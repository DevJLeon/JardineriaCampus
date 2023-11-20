using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class RolConfiguration: IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {

        builder.ToTable("rol");
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
        .IsRequired();
        
        builder.Property(p => p.Nombre)
        .HasColumnName("Nombre")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();
    
    }
}

