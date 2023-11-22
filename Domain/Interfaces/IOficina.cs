using Domain.Entities;

namespace Domain.Interfaces;
public interface IOficina : IGenericRepo<Oficina>
{
    Task<IEnumerable<object>> Consulta26();
    Task<(int totalRegistros, object registros)> Consulta26(int pageIndez, int pageSize, string search)
}
