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


}