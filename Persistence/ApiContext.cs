using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia;
public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions options) : base(options)
    { }
    
    public DbSet<Rol> Roles { get; set; }
    public DbSet<RolUsuario> RolUsuarios { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Cliente> Clientes { get; set; }
    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }
    public virtual DbSet<Empleado> Empleados { get; set; }
    public virtual DbSet<GamaProducto> GamaProductos { get; set; }
    public virtual DbSet<Oficina> Oficinas { get; set; }
    public virtual DbSet<Pago> Pagos { get; set; }
    public virtual DbSet<Pedido> Pedidos { get; set; }
    public virtual DbSet<Producto> Productos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
