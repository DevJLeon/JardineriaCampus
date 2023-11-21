using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class PedidoRepository: GenericRepo<Pedido>, IPedido
{
        private readonly ApiContext _context;
    
    public PedidoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Pedido>> GetAllAsync()
    {
        return await _context.Pedidos
            .ToListAsync();
    }

    public async Task<Pedido> GetByIdAsync(int id)
    {
        return await _context.Pedidos
        .FirstOrDefaultAsync(p =>  p.CodigoPedido == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Pedido> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Pedidos as IQueryable<Pedido>;

        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Estado.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.CodigoPedido);
        var totalRegistros = await query.CountAsync();
        var registros = await query 
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
        public async Task<IEnumerable<object>> Consulta2()
    {
        var dato = await (
            from p in _context.Pedidos
            select p.Estado
            ).Distinct()
            .ToListAsync();

        return dato;
    }
    public async Task<IEnumerable<object>> Consulta4()
    {
        var dato = await (
        from pe in _context.Pedidos
        join cl in _context.Clientes on pe.CodigoCliente equals cl.CodigoCliente
        where pe.Estado == "Pendiente"
        where pe.FechaEntrega > pe.FechaEsperada
        select new
        {
            CodigoPedido = pe.CodigoPedido,
            NombreCliente = cl.NombreCliente,
            FechaEspera = pe.FechaEsperada,
            FechaEntrega = pe.FechaEntrega

        }
        ).ToListAsync();

        return dato;
    }

    

public virtual async Task<(int totalRegistros, object registros)> Consulta4(int pageIndez, int pageSize, string search) // 1.1
    {
        var query = (
        from pe in _context.Pedidos
        join cl in _context.Clientes on pe.CodigoCliente equals cl.CodigoCliente
        where pe.Estado == "Pendiente"
        where pe.FechaEntrega > pe.FechaEsperada
        select new
        {
            CodigoPedido = pe.CodigoPedido,
            NombreCliente = cl.NombreCliente,
            FechaEspera = pe.FechaEsperada,
            FechaEntrega = pe.FechaEntrega

        }
            );

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreCliente.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.NombreCliente);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }


    public async Task<IEnumerable<object>> Consulta5()
    {
        var dato = await (
            from pe in _context.Pedidos
            join cl in _context.Clientes on pe.CodigoCliente equals cl.CodigoCliente
            where pe.FechaEntrega < pe.FechaEsperada.AddDays(-2)
            select new
            {
                Codigo = pe.CodigoPedido,
                CodigoCliente = pe.CodigoCliente,
                FechaEsperada = pe.FechaEsperada,
                FechaEntrega = pe.FechaEntrega
            }
        ).ToListAsync();

        return dato;
    }
    public async Task<IEnumerable<object>> Consulta6()
    {

        var dato = await (
        from pe in _context.Pedidos
        where pe.Estado == "Rechazado" && pe.FechaEntrega.Year == 2009
        select new
        {
            CodigoPedido = pe.CodigoPedido,
            Estado = pe.Estado,
            FechaEntrega = pe.FechaEntrega
        }).ToListAsync();

        return dato;
    }
    //7. Devuelve un listado de todos los pedidos que han sido entregados en el mes de enero de cualquier año.
    public async Task<IEnumerable<object>> Consulta7()
    {

        var dato = await (
        from pe in _context.Pedidos
        where pe.Estado == "Rechazado" && pe.FechaEntrega.Month == 1
        select new
        {
            CodigoPedido = pe.CodigoPedido,
            Estado = pe.Estado,
            FechaEntrega = pe.FechaEntrega
        }).ToListAsync();

        return dato;
    }
public async Task<IEnumerable<object>> Consulta32()
{
    var orderCounts = await (
        from pe in _context.Pedidos
        group pe by pe.Estado into grupoPedidos
        orderby grupoPedidos.Count() descending
        select new
        {
            Estado = grupoPedidos.Key,
            CantidadPedidos = grupoPedidos.Count()
        }
    ).ToListAsync();

    return orderCounts;
}
public async Task<IEnumerable<object>> Consulta38()
{
    var data = await (
        from pedido in _context.Pedidos
        join detallePedido in _context.DetallePedidos on pedido.CodigoPedido equals detallePedido.CodigoPedido
        group detallePedido by pedido.CodigoPedido into grp
        select new
        {
            CodigoPedido = grp.Key,
            NumeroProductosDiferentes = grp.Select(dp => dp.CodigoProducto).Distinct().Count()
        }
    ).ToListAsync();

    return data;
}
public async Task<IEnumerable<object>> Consulta39()
{
    var data = await (
        from pedido in _context.Pedidos
        join detallePedido in _context.DetallePedidos on pedido.CodigoPedido equals detallePedido.CodigoPedido
        group detallePedido by pedido.CodigoPedido into grp
        select new
        {
            CodigoPedido = grp.Key,
            SumaCantidadTotal = grp.Sum(dp => dp.Cantidad)
        }
    ).ToListAsync();

    return data;
}
public async Task<IEnumerable<object>> Consulta53()
{
    var dato = await (
        from p in _context.Productos
        where !_context.DetallePedidos.Any(dp => dp.CodigoProducto == p.CodigoProducto)
        select new
        {
            CodigoProducto = p.CodigoProducto,
            NombreProducto = p.Nombre,
        }
    ).ToListAsync();

    return dato;
}

}