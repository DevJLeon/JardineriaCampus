using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Application.Repository;
public class OficinaRepository: GenericRepo<Oficina>, IOficina
{
        private readonly ApiContext _context;
    
    public OficinaRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Oficina>> GetAllAsync()
    {
        return await _context.Oficinas
            .ToListAsync();
    }

    public async Task<Oficina> GetByIdAsync(string id)
    {
        return await _context.Oficinas
        .FirstOrDefaultAsync(p =>  p.CodigoOficina == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Oficina> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Oficinas as IQueryable<Oficina>;

        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.CodigoOficina.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.CodigoOficina);
        var totalRegistros = await query.CountAsync();
        var registros = await query 
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}