using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

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
        .FirstOrDefaultAsync(p =>  p.CodigoProducto == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Producto> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Productos as IQueryable<Producto>;

        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.CodigoProducto);
        var totalRegistros = await query.CountAsync();
        var registros = await query 
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}