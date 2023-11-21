using Domain.Entities;

namespace Domain.Interfaces;
public interface IPago : IGenericRepo<Pago>
{
Task<IEnumerable<object>> Consulta8();
Task<IEnumerable<object>> Consulta9();
Task<string> Consulta31();
}
