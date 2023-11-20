using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Application.Repository;
public class PagoRepository: GenericRepo<Pago>, IPago
{
        private readonly ApiContext _context;
    
    public PagoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Pago>> GetAllAsync()
    {
        return await _context.Pagos
            .ToListAsync();
    }

    public async Task<Pago> GetByIdAsync(int id)
    {
        return await _context.Pagos
        .FirstOrDefaultAsync(p =>  p.CodigoCliente == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Pago> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Pagos as IQueryable<Pago>;

        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.IdTransaccion.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.CodigoCliente);
        var totalRegistros = await query.CountAsync();
        var registros = await query 
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}