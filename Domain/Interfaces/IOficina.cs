using Domain.Entities;

namespace Domain.Interfaces;
public interface IOficina : IGenericRepo<Oficina>
{
    Task<IEnumerable<object>> Consulta26();
}
