using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Application.Repository;
public class EmpleadoRepository: GenericRepo<Empleado>, IEmpleado
{
        private readonly ApiContext _context;
    
    public EmpleadoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Empleado>> GetAllAsync()
    {
        return await _context.Empleados
            .ToListAsync();
    }

    public async Task<Empleado> GetByIdAsync(int id)
    {
        return await _context.Empleados
        .FirstOrDefaultAsync(p =>  p.CodigoEmpleado == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Empleado> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Empleados as IQueryable<Empleado>;

        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.CodigoEmpleado);
        var totalRegistros = await query.CountAsync();
        var registros = await query 
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}