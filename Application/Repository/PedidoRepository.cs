using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Application.Repository;
public class PedidoRepository: GenericRepo<Pedido>, IPedido
{
        private readonly ApiContext _context;
    
    public PedidoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Pedido>> GetAllAsync()
    {
        return await _context.Pedidos
            .ToListAsync();
    }

    public async Task<Pedido> GetByIdAsync(int id)
    {
        return await _context.Pedidos
        .FirstOrDefaultAsync(p =>  p.CodigoPedido == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Pedido> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Pedidos as IQueryable<Pedido>;

        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Estado.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.CodigoPedido);
        var totalRegistros = await query.CountAsync();
        var registros = await query 
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}