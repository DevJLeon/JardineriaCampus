using Domain.Entities;

namespace Domain.Interfaces;
public interface IPedido : IGenericRepo<Pedido>
{
Task<IEnumerable<object>> Consulta2();
Task<IEnumerable<object>> Consulta4();
Task<IEnumerable<object>> Consulta5();
Task<IEnumerable<object>> Consulta6();
Task<IEnumerable<object>> Consulta7();
Task<IEnumerable<object>> Consulta32();
Task<IEnumerable<object>> Consulta38();
Task<IEnumerable<object>> Consulta39();
Task<IEnumerable<object>> Consulta53();
}
