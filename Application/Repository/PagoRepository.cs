using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

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
        .FirstOrDefaultAsync(pa =>  pa.CodigoCliente == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Pago> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Pagos as IQueryable<Pago>;

        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(pa => pa.IdTransaccion.ToLower().Contains(search));
        }

        query = query.OrderBy(pa => pa.CodigoCliente);
        var totalRegistros = await query.CountAsync();
        var registros = await query 
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
    public async Task<IEnumerable<object>> Consulta8()
    {

        var dato = await (
        from pa in _context.Pagos
        where pa.FormaPago == "PayPal"
        orderby pa.Total descending
        select pa).ToListAsync();

        return dato;
    }

    public async Task<IEnumerable<object>> Consulta9()
    {
        var dato = await (
            from pa in _context.Pagos
            select pa.FormaPago
            ).Distinct()
            .ToListAsync();

        return dato;
    }
}