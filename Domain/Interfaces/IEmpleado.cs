using Domain.Entities;

namespace Domain.Interfaces;
public interface IEmpleado : IGenericRepo<Empleado>
{
Task<IEnumerable<object>> Consulta17();
Task<IEnumerable<object>> Consulta22();
Task<IEnumerable<object>> Consulta23();
Task<IEnumerable<object>> Consulta28();
Task<object> Consulta29();//fix
Task<IEnumerable<object>> Consulta35();
Task<IEnumerable<object>> Consulta54();
Task<IEnumerable<object>> Consulta61();
}
