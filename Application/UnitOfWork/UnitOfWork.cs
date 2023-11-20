using Application.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Persistencia;

namespace Application.UnitOfWork;
public class UnitOfWork  : IUnitOfWork, IDisposable
{
    private readonly ApiContext _context;
    private RolRepository _Rol;
    private UsuarioRepository _Usuario;
    private ClienteRepository _Cliente;
    private DetallePedidoRepository _DetallePedido;
    private EmpleadoRepository _Empleado;
    private GamaProductoRepository _GamaProducto;
    private OficinaRepository _Oficina;
    private PagoRepository _Pago;
    private PedidoRepository _Pedido;
    private ProductoRepository _Producto;

    public UnitOfWork(ApiContext context)
    {
        _context = context;
    }
    
    public IRol Roles
    {
        get{
            if(_Rol== null)
            {
                _Rol= new RolRepository(_context);
            }
            return _Rol;
        }
    }
    
    public IUsuario Usuarios
    {
        get{
            if(_Usuario== null)
            {
                _Usuario= new UsuarioRepository(_context);
            }
            return _Usuario;
        }
    }
    

    public ICliente Clientes
    {
        get{
            if(_Cliente== null)
            {
                _Cliente= new ClienteRepository(_context);
            }
            return _Cliente;
        }
    }

    public IDetallePedido DetallePedidos
    {
        get{
            if(_DetallePedido== null)
            {
                _DetallePedido= new DetallePedidoRepository(_context);
            }
            return _DetallePedido;
        }
    }
    public IEmpleado Empleados
    {
        get{
            if(_Empleado== null)
            {
                _Empleado= new EmpleadoRepository(_context);
            }
            return _Empleado;
        }
    }
    public IGamaProducto GamaProductos
    {
        get{
            if(_GamaProducto== null)
            {
                _GamaProducto= new GamaProductoRepository(_context);
            }
            return _GamaProducto;
        }
    }
    public IOficina Oficinas
    {
        get{
            if(_Oficina== null)
            {
                _Oficina= new OficinaRepository(_context);
            }
            return _Oficina;
        }
    }
    public IPago Pagos
    {
        get{
            if(_Pago== null)
            {
                _Pago= new PagoRepository(_context);
            }
            return _Pago;
        }
    }
    public IPedido Pedidos
    {
        get{
            if(_Pedido== null)
            {
                _Pedido= new PedidoRepository(_context);
            }
            return _Pedido;
        }
    }
    public IProducto Productos
    {
        get{
            if(_Producto== null)
            {
                _Producto= new ProductoRepository(_context);
            }
            return _Producto;
        }
    }
    

    public void Dispose()
    {
        _context.Dispose();
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
