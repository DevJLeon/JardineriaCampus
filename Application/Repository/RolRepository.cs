using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Application.Repository;
public class RolRepository : GenericRepo<Rol>, IRol
{
    private readonly ApiContext _context;
    
    public RolRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Rol>> GetAllAsync()
    {
        return await _context.Roles
            .ToListAsync();
    }

    public async Task<Rol> GetByIdAsync(int id)
    {
        return await _context.Roles
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Rol> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Roles as IQueryable<Rol>;

        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query 
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}