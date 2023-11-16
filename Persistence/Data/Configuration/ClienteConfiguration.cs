using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(e => e.CodigoCliente).HasName("PRIMARY");

            builder.ToTable("cliente");

            builder.HasIndex(e => e.CodigoEmpleadoRepVentas, "codigo_empleado_rep_ventas");

            builder.Property(e => e.CodigoCliente)
                .ValueGeneratedNever()
                .HasColumnName("codigo_cliente");
            builder.Property(e => e.ApellidoContacto)
                .HasMaxLength(30)
                .HasColumnName("apellido_contacto");
            builder.Property(e => e.Ciudad)
                .HasMaxLength(50)
                .HasColumnName("ciudad");
            builder.Property(e => e.CodigoEmpleadoRepVentas).HasColumnName("codigo_empleado_rep_ventas");
            builder.Property(e => e.CodigoPostal)
                .HasMaxLength(10)
                .HasColumnName("codigo_postal");
            builder.Property(e => e.Fax)
                .HasMaxLength(15)
                .HasColumnName("fax");
            builder.Property(e => e.LimiteCredito)
                .HasPrecision(15, 2)
                .HasColumnName("limite_credito");
            builder.Property(e => e.LineaDireccion1)
                .HasMaxLength(50)
                .HasColumnName("linea_direccion1");
            builder.Property(e => e.LineaDireccion2)
                .HasMaxLength(50)
                .HasColumnName("linea_direccion2");
            builder.Property(e => e.NombreCliente)
                .HasMaxLength(58)
                .HasColumnName("nombre_cliente");
            builder.Property(e => e.NombreContacto)
                .HasMaxLength(30)
                .HasColumnName("nombre_contacto");
            builder.Property(e => e.Pais)
                .HasMaxLength(50)
                .HasColumnName("pais");
            builder.Property(e => e.Region)
                .HasMaxLength(50)
                .HasColumnName("region");
            builder.Property(e => e.Telefono)
                .HasMaxLength(15)
                .HasColumnName("telefono");

            builder.HasOne(d => d.CodigoEmpleadoRepVentasNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.CodigoEmpleadoRepVentas)
                .HasConstraintName("cliente_ibfk_1");
        }
    }