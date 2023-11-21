using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class ClienteRepository: GenericRepo<Cliente>, ICliente
{
        private readonly ApiContext _context;
    
    public ClienteRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _context.Clientes
            .ToListAsync();
    }

    public async Task<Cliente> GetByIdAsync(int id)
    {
        return await _context.Clientes
        .FirstOrDefaultAsync(c =>  c.CodigoCliente == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Cliente> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Clientes as IQueryable<Cliente>;

        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(c => c.NombreCliente.ToLower().Contains(search));
        }

        query = query.OrderBy(c => c.CodigoCliente);
        var totalRegistros = await query.CountAsync();
        var registros = await query 
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    public async Task<IEnumerable<object>> Consulta1()
    {
        var dato = await (
            from c in _context.Clientes
            where c.Pais == "Spain"
            select new
            {
                Nombre = c.NombreCliente,
                Pais = c.Pais,
                Telefono = c.Telefono
            }).ToListAsync();

        return dato;
    }

    public async Task<IEnumerable<object>> Consulta3()
    {
        var dato = await (
        from c in _context.Clientes
        join pa in _context.Pagos on c.CodigoCliente equals pa.CodigoCliente
        where pa.FechaPago.Year == 2008
        select new
        {
            Codigo = c.CodigoCliente,
            Nombre = c.NombreCliente
        }
        ).Distinct()
        .ToListAsync();

        return dato;
    }
    public async Task<IEnumerable<object>> Consulta11()
    {
        var dato = await (
        from c in _context.Clientes
        join em in _context.Empleados on c.CodigoEmpleadoRepVentas equals em.CodigoEmpleado
        where c.Ciudad == "Madrid"
        where em.CodigoEmpleado == 11 || em.CodigoEmpleado == 30
        select new
        {
            Codigo = c.CodigoCliente,
            Nombre = c.NombreCliente,
            Ciudad = c.Ciudad
        }
        ).ToListAsync();

        return dato;
    }
    public async Task<IEnumerable<object>> Consulta12()
    {
        var dato = await (
        from c in _context.Clientes
        join em in _context.Empleados on c.CodigoEmpleadoRepVentas equals em.CodigoEmpleado
        select new
        {
            NombreCliente = c.NombreCliente,
            NombreRepresentante = em.Nombre,
            ApellidoRepresentante = em.Apellidol
        }
        ).ToListAsync();

        return dato;
    }
    public async Task<IEnumerable<object>> Consulta13()
    {
        var dato = await (
        from c in _context.Clientes
        join em in _context.Empleados on c.CodigoEmpleadoRepVentas equals em.CodigoEmpleado
        join pa in _context.Pagos on c.CodigoCliente equals pa.CodigoCliente
        select new
        {
            NombreCliente = c.NombreCliente,
            NombreRepresentante = em.Nombre,
        }
        ).ToListAsync();

        return dato;
    }

    public async Task<IEnumerable<object>> Consulta14()
    {
        var dato = await (
            from c in _context.Clientes
            join em in _context.Empleados on c.CodigoEmpleadoRepVentas equals em.CodigoEmpleado
            where !_context.Pagos.Any(pa => pa.CodigoCliente == c.CodigoCliente)
            select new
            {
                NombreCliente = c.NombreCliente,
                NombreRepresentante = em.Nombre,
            }
        ).ToListAsync();

        return dato;
    }
    public async Task<IEnumerable<object>> Consulta15()
    {
        var dato = await (
            from c in _context.Clientes
            join em in _context.Empleados on c.CodigoEmpleadoRepVentas equals em.CodigoEmpleado
            join of in _context.Oficinas on em.CodigoOficina equals of.CodigoOficina
            join pa in _context.Pagos on c.CodigoCliente equals pa.CodigoCliente
            select new
            {
                NombreCliente = c.NombreCliente,
                NombreRepresentante = em.Nombre,
                CiudadOficina = of.Ciudad
            }
        ).ToListAsync();
    
        return dato;
    }

    public async Task<IEnumerable<object>> Consulta16()
    {
        var dato = await (
            from c in _context.Clientes
            join em in _context.Empleados on c.CodigoEmpleadoRepVentas equals em.CodigoEmpleado
            join of in _context.Oficinas on em.CodigoOficina equals of.CodigoOficina
            where !_context.Pagos.Any(pa => pa.CodigoCliente == c.CodigoCliente)
            select new
            {
                NombreCliente = c.NombreCliente,
                NombreRepresentante = em.Nombre,
                CiudadOficina = of.Ciudad
            }
        ).ToListAsync();
    
        return dato;
    }
    public async Task<IEnumerable<object>> Consulta18()
    {
        var dato = await (
            from c in _context.Clientes
            join pe in _context.Pedidos on c.CodigoCliente equals pe.CodigoCliente
            where pe.FechaEntrega > pe.FechaEsperada && pe.Estado == "Pendiente"
            select new
            {
                NombreCliente = c.NombreCliente
            }
        ).Distinct().ToListAsync();

        return dato;
    }
    public async Task<IEnumerable<object>> Consulta19()
    {
        var dato = await (
            from depe in _context.DetallePedidos
            join pr in _context.Productos on depe.CodigoProducto equals pr.CodigoProducto
            join pe in _context.Pedidos on depe.CodigoPedido equals pe.CodigoPedido
            join c in _context.Clientes on pe.CodigoCliente equals c.CodigoCliente
            group pr.Gama by c.NombreCliente into cgamas
            select new
            {
                Cliente = cgamas.Key,
                ListaGamas = cgamas.Distinct().ToList()
            }
            ).ToListAsync();

        return dato;
    }

    public async Task<IEnumerable<object>> Query20()
    {
        var dato = await (
            from c in _context.Clientes
            join p in _context.Pagos on c.CodigoCliente equals p.CodigoCliente into gj
            from subp in gj.DefaultIfEmpty()
            where subp == null
            select new
            {
                NombreCliente = c.NombreCliente
            }
        ).ToListAsync();

        return dato;
    }
    public async Task<IEnumerable<object>> Query21()
    {
        var dato = await (
            from c in _context.Clientes
            join p in _context.Pagos on c.CodigoCliente equals p.CodigoCliente into pg
            from subp in pg.DefaultIfEmpty()
            join pe in _context.Pedidos on c.CodigoCliente equals pe.CodigoCliente into pd
            from subpe in pd.DefaultIfEmpty()
            where subp == null && subpe == null
            select new
            {
                NombreCliente = c.NombreCliente
            }
        ).ToListAsync();

        return dato;
    }
public async Task<IEnumerable<object>> Query27()
{
    var dato = await (
        from c in _context.Clientes
        where _context.Pedidos.Any(p => p.CodigoCliente == c.CodigoCliente) &&
                !_context.Pagos.Any(pa => pa.CodigoCliente == c.CodigoCliente)
        select new
        {
            NombreCliente = c.NombreCliente,
            CodigoCliente = c.CodigoCliente
        }
    ).ToListAsync();

    return dato;
}
public async Task<IEnumerable<object>> Consulta30()
{
    var dato = await (
        from c in _context.Clientes
        group c by c.Pais into grupoPais
        select new
        {
            Country = grupoPais.Key,
            Clientes_en_Pais = grupoPais.Count()
        }
    ).ToListAsync();

    return dato;
}
public async Task<int> Consulta33()
{
    var dato = await (
        from c in _context.Clientes
        where c.Ciudad == "Madrid"
        select c
    ).CountAsync();

    return dato;
}
public async Task<IEnumerable<object>> Consulta34()
{
    var dato = await (
        from c in _context.Clientes
        where c.Ciudad.StartsWith("M")
        group c by c.Ciudad into cityGroup
        select new 
        {
            Ciudad = cityGroup.Key,
            CantidadClientes = cityGroup.Count()
        }
    ).ToListAsync();

    return dato;
}
public async Task<int> Consulta36()
{
    var data = await (
        from c in _context.Clientes
        where c.CodigoEmpleadoRepVentas == null
        select c
    ).CountAsync();

    return data;
}
public async Task<IEnumerable<object>> Consulta37()
{
    var dato = await (
        from c in _context.Clientes
        join primerPago in _context.Pagos.OrderBy(p => p.FechaPago)
                            on c.CodigoCliente equals primerPago.CodigoCliente into primerPagos
        join ultimoPago in _context.Pagos.OrderByDescending(p => p.FechaPago)
                            on c.CodigoCliente equals ultimoPago.CodigoCliente into ultimoPagos
        select new
        {
            Nombre = c.NombreCliente,
            Apellidos = c.ApellidoContacto,
            PrimerPago = primerPagos.Select(p => p.FechaPago).FirstOrDefault(),
            UltimoPago = ultimoPagos.Select(p => p.FechaPago).FirstOrDefault()
        }
    ).ToListAsync();

    return dato;
}
public async Task<string> Consulta45()
{
    var dato = await (
        from c in _context.Clientes
        orderby c.LimiteCredito descending
        select c.NombreCliente
    ).FirstOrDefaultAsync();

    return dato;
}

public async Task<IEnumerable<object>> Consulta48()
{
    var dato = await (
        from c in _context.Clientes
        where c.LimiteCredito > (
            from pa in _context.Pagos
            where pa.CodigoCliente == c.CodigoCliente
            select pa.Total
        ).Sum()
        select new
        {
            NombreCliente = c.NombreCliente,
            LimiteCredito = c.LimiteCredito
        }
    ).ToListAsync();

    return dato;
}

public async Task<object> Consulta49()
{
    var dato = await (
        from c in _context.Clientes
        orderby c.LimiteCredito descending
        select new
        {
            NombreCliente = c.NombreCliente,
            LimiteCredito = c.LimiteCredito
        }
    ).FirstOrDefaultAsync();

    return dato;
}

public async Task<IEnumerable<object>> Consulta51()
{
    var dato = await (
        from c in _context.Clientes
        where !_context.Pagos.Any(p => p.CodigoCliente == c.CodigoCliente)
        select new
        {
            CodigoCliente = c.CodigoCliente,
            NombreCliente = c.NombreCliente
        }
    ).ToListAsync();

    return dato;
}
public async Task<IEnumerable<object>> Consulta52()
{
    var dato = await (
        from c in _context.Clientes
        join p in _context.Pagos on c.CodigoCliente equals p.CodigoCliente
        select new
        {
            CodigoCliente = c.CodigoCliente,
            NombreCliente = c.NombreCliente,
        }
    ).ToListAsync();

    return dato;
}

public async Task<IEnumerable<object>> Consulta55()
{
    var dato = await (
        from c in _context.Clientes
        where !_context.Pagos.Any(p => p.CodigoCliente == c.CodigoCliente)
        select new
        {
            NombreCliente = c.NombreCliente,
            ApellidoCliente = c.ApellidoContacto
        }
    ).ToListAsync();

    return dato;
}

public async Task<IEnumerable<object>> Consulta56()
{
    var dato = await (
        from c in _context.Clientes
        where _context.Pagos.Any(p => p.CodigoCliente == c.CodigoCliente)
        select new
        {
            NombreCliente = c.NombreCliente,
            ApellidoCliente = c.ApellidoContacto
        }
    ).ToListAsync();

    return dato;
}
public async Task<IEnumerable<object>> Consulta57()
{
    var dato = await (
        from c in _context.Clientes
        select new
        {
            NombreCliente = c.NombreCliente,
            PedidosRealizados = _context.Pedidos.Count(p => p.CodigoCliente == c.CodigoCliente)
        }
    ).ToListAsync();

    return dato;
}

public async Task<IEnumerable<object>> Consulta58()
{
    var dato = await (
        from c in _context.Clientes
        join p in _context.Pedidos on c.CodigoCliente equals p.CodigoCliente
        where p.FechaPedido.Year == 2008
        orderby c.NombreCliente
        select c.NombreCliente
    ).ToListAsync();

    return dato;
}
public async Task<IEnumerable<object>> Consulta59()
{
    var dato = await (
        from c in _context.Clientes
        where !_context.Pagos.Any(pago => pago.CodigoCliente == c.CodigoCliente)
        join em in _context.Empleados on c.CodigoEmpleadoRepVentas equals em.CodigoEmpleado
        join of in _context.Oficinas on em.CodigoOficina equals of.CodigoOficina
        select new
        {
            NombreCliente = c.NombreCliente,
            NombreRepresentante = em.Nombre,
            ApellidoRepresentante = em.Apellidol,
            TelefonoOficina = of.Telefono
        }
    ).ToListAsync();

    return dato;
}

public async Task<IEnumerable<object>> Consulta60()
{
    var dato = await (
        from c in _context.Clientes
        join em in _context.Empleados on c.CodigoEmpleadoRepVentas equals em.CodigoEmpleado
        join of in _context.Oficinas on em.CodigoOficina equals of.CodigoOficina
        select new
        {
            NombreCliente = c.NombreCliente,
            NombreRepresentante = em.Nombre,
            ApellidoRepresentante = em.Apellidol,
            CiudadOficina = of.Ciudad
        }
    ).ToListAsync();

    return dato;
}


}