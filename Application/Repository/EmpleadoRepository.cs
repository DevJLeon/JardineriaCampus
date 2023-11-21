using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

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

    public async Task<IEnumerable<object>> Consulta17()
    {
        var dato = await (
            from em in _context.Empleados
            join j1 in _context.Empleados on em.CodigoJefe equals j1.CodigoEmpleado into bossJoin
            from boss in bossJoin.DefaultIfEmpty()
            join j2 in _context.Empleados on boss.CodigoJefe equals j2.CodigoEmpleado into grandBossJoin
            from grandBoss in grandBossJoin.DefaultIfEmpty()
            select new
            {
                NombreEmpleado = em.Nombre,
                NombreJefe = boss != null ? boss.Nombre : null,
                NombreJefeJefe = grandBoss != null ? grandBoss.Nombre : null
            }
        ).ToListAsync();

        return dato;
    }
    public async Task<IEnumerable<object>> Query22()
    {
        var data = await (
            from em in _context.Empleados
            join of in _context.Oficinas on em.CodigoOficina equals of.CodigoOficina
            join c in _context.Clientes on em.CodigoEmpleado equals c.CodigoEmpleadoRepVentas into cj
            from subc in cj.DefaultIfEmpty()
            where subc == null
            select new
            {
                NombreEmpleado = em.Nombre,
                CiudadOficina = of.Ciudad,
                PaisOficina = of.Pais
            }
        ).ToListAsync();

        return data;
    }
    public async Task<IEnumerable<object>> Query23()
    {
        var data = await (
            from em in _context.Empleados
            join of in _context.Oficinas on em.CodigoOficina equals of.CodigoOficina into oj
            from subo in oj.DefaultIfEmpty()
            join c in _context.Clientes on em.CodigoEmpleado equals c.CodigoEmpleadoRepVentas into cj
            from subc in cj.DefaultIfEmpty()
            where subo == null || subc == null
            select new
            {
                NombreEmpleado = em.Nombre,
                TieneOficina = subo != null,
                TieneCliente = subc != null
            }
        ).ToListAsync();

        return data;
    }
public async Task<IEnumerable<object>> Query28()
{
    var data = await (
        from em in _context.Empleados
        join boss in _context.Empleados on em.CodigoJefe equals boss.CodigoEmpleado into bossJoin
        from b in bossJoin.DefaultIfEmpty()
        where !_context.Clientes.Any(c => c.CodigoEmpleadoRepVentas == em.CodigoEmpleado)
        select new
        {
            NombreEmpleado = em.Nombre,
            Jefe = b.Nombre
        }
    ).ToListAsync();

    return data;
}
public async Task<object> Consulta29()
{
    var data = await _context.Empleados.CountAsync();
    return data;
}
public async Task<IEnumerable<object>> Consulta35()
{
    var dato = await (
        from em in _context.Empleados
        join c in _context.Clientes on em.CodigoEmpleado equals c.CodigoEmpleadoRepVentas into clientGroup
        select new 
        {
            NombreRepresentante = em.Nombre,
            CantidadClientes = clientGroup.Count()
        }
    ).ToListAsync();

    return dato;
}





}