using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

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
    public async Task<IEnumerable<object>> Consulta26()
    {
        var dato = await (
            from of in _context.Oficinas
            where !_context.Clientes.Any(c => _context.Empleados
                    .Where(em => em.CodigoEmpleado == c.CodigoEmpleadoRepVentas)
                    .Any(salesRep =>
                        _context.DetallePedidos
                            .Any(dp =>
                                dp.CodigoProductoNavigation.Gama == "Frutales" &&
                                dp.CodigoPedidoNavigation.CodigoCliente == c.CodigoCliente
                            )
                    )
                )
            select new
            {
                NombreOficina = of.CodigoOficina,
                Ciudad = of.Ciudad,
                Pais = of.Pais
            }
        ).ToListAsync();

        return dato;
    }

}