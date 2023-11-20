using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Application.Repository;
public class GamaProductoRepository: GenericRepo<GamaProducto>, IGamaProducto
{
        private readonly ApiContext _context;
    
    public GamaProductoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<GamaProducto>> GetAllAsync()
    {
        return await _context.GamaProductos
            .ToListAsync();
    }

    public async Task<GamaProducto> GetByIdAsync(string id)
    {
        return await _context.GamaProductos
        .FirstOrDefaultAsync(p =>  p.Gama == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<GamaProducto> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.GamaProductos as IQueryable<GamaProducto>;

        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Gama.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Gama);
        var totalRegistros = await query.CountAsync();
        var registros = await query 
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}