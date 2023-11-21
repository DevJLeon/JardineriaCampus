using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class ProductoRepository: GenericRepo<Producto>, IProducto
{
        private readonly ApiContext _context;
    
    public ProductoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Producto>> GetAllAsync()
    {
        return await _context.Productos
            .ToListAsync();
    }

    public async Task<Producto> GetByIdAsync(string id)
    {
        return await _context.Productos
        .FirstOrDefaultAsync(pr =>  pr.CodigoProducto == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Producto> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Productos as IQueryable<Producto>;

        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(pr => pr.Nombre.ToLower().Contains(search));
        }

        query = query.OrderBy(pr => pr.CodigoProducto);
        var totalRegistros = await query.CountAsync();
        var registros = await query 
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
    public async Task<IEnumerable<object>> Consulta10()
    {
        var dato = await (
            from pr in _context.Productos
            where pr.Gama == "Ornamentales" && pr.CantidadEnStock > 100
            orderby pr.PrecioVenta descending
            select new 
            {
                Producto = pr.Nombre,
                Codigo = pr.CodigoProducto,
                Gama = pr.Gama,
                Stock = pr.CantidadEnStock,
                PrecioVenta = pr.PrecioVenta
            }).ToListAsync();

        return dato;
    }


public virtual async Task<(int totalRegistros, object registros)> Consulta10(int pageIndez, int pageSize, string search) // 1.1
    {
        var query = (
            from pr in _context.Productos
            where pr.Gama == "Ornamentales" && pr.CantidadEnStock > 100
            orderby pr.PrecioVenta descending
            select new 
            {
                Producto = pr.Nombre,
                Codigo = pr.CodigoProducto,
                Gama = pr.Gama,
                Stock = pr.CantidadEnStock,
                PrecioVenta = pr.PrecioVenta
    });

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Producto.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Producto);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    public async Task<IEnumerable<object>> Consulta24()
    {
        var data = await (
            from p in _context.Productos
            join op in _context.DetallePedidos on p.CodigoProducto equals op.CodigoProducto into oj
            from subop in oj.DefaultIfEmpty()
            where subop == null
            select new
            {
                NombreProducto = p.Nombre,
                Descripcion = p.Descripcion
            }
        ).ToListAsync();

        return data;
    }
    public async Task<IEnumerable<object>> Consulta25()
    {
        var data = await (
            from p in _context.Productos
            join gp in _context.GamaProductos on p.Gama equals gp.Gama
            join op in _context.DetallePedidos on p.CodigoProducto equals op.CodigoProducto into oj
            from subop in oj.DefaultIfEmpty()
            where subop == null
            select new
            {
                NombreProducto = p.Nombre,
                Descripcion = p.Descripcion,
                Imagen = gp.Imagen
            }
        ).ToListAsync();
    
        return data;
    }
public async Task<IEnumerable<object>> Consulta40()
{
    var data = await (
        from detallePedido in _context.DetallePedidos
        group detallePedido by detallePedido.CodigoProducto into grp
        join producto in _context.Productos on grp.Key equals producto.CodigoProducto
        orderby grp.Sum(dp => dp.Cantidad) descending
        select new
        {
            CodigoProducto = grp.Key,
            NombreProducto = producto.Nombre,
            TotalUnidadesVendidas = grp.Sum(dp => dp.Cantidad)
        }
    ).Take(20).ToListAsync();

    return data;
}
public async Task<IEnumerable<object>> Consulta41()
{
    var dato = await (
        from detallePedido in _context.DetallePedidos
        group detallePedido by detallePedido.CodigoProducto into grp
        orderby grp.Sum(x => x.Cantidad) descending
        select new
        {
            CodigoProducto = grp.Key,
            TotalUnidadesVendidas = grp.Sum(x => x.Cantidad)
        }
    ).Take(20).ToListAsync();

    return dato;
}
public async Task<IEnumerable<object>> Consulta42()
{
    var dato = await (
        from detallePedido in _context.DetallePedidos
        where detallePedido.CodigoProducto.StartsWith("OR")
        group detallePedido by detallePedido.CodigoProducto into grp
        orderby grp.Sum(x => x.Cantidad) descending
        select new
        {
            CodigoProducto = grp.Key,
            TotalUnidadesVendidas = grp.Sum(x => x.Cantidad)
        }
    ).Take(20).ToListAsync();

    return dato;
}
public async Task<IEnumerable<object>> Consulta43()
{
    var dato = await (
        from detallePedido in _context.DetallePedidos
        join producto in _context.Productos on detallePedido.CodigoProducto equals producto.CodigoProducto
        let totalFacturado = detallePedido.PrecioUnidad * detallePedido.Cantidad
        let totalConIVA = totalFacturado * 1.21M // 21% VAT
        group new { detallePedido, producto } by new { producto.Nombre } into grp
        let totalVenta = grp.Sum(x => x.detallePedido.Cantidad)
        let totalFacturadoProductos = grp.Sum(x => x.detallePedido.PrecioUnidad * x.detallePedido.Cantidad)
        where totalFacturadoProductos > 3000
        select new
        {
            NombreProducto = grp.Key.Nombre,
            UnidadesVendidas = totalVenta,
            TotalFacturado = totalFacturadoProductos,
            TotalFacturadoConIVA = totalFacturadoProductos * 1.21M
        }
    ).ToListAsync();

    return dato;
}
public async Task<string> Consulta46()
{
    var dato = await (
        from p in _context.Productos
        orderby p.PrecioVenta descending
        select p.Nombre
    ).FirstOrDefaultAsync();

    return dato;
}
public async Task<object> Consulta47()
{
    var dato = await (
        from dp in _context.DetallePedidos
        group dp by dp.CodigoProducto into grp
        orderby grp.Sum(p => p.Cantidad) descending
        join p in _context.Productos on grp.Key equals p.CodigoProducto
        select p.Nombre
    ).FirstOrDefaultAsync();

    return dato;
}
public async Task<object> Consulta50()
{
    var dato = await (
        from p in _context.Productos
        orderby p.PrecioVenta descending
        select new
        {
            NombreProducto = p.Nombre,
            PrecioVenta = p.PrecioVenta
        }
    ).FirstOrDefaultAsync();

    return dato;
}


}