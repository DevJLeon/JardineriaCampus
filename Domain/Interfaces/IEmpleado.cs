using Domain.Entities;

namespace Domain.Interfaces;
public interface IEmpleado : IGenericRepo<Empleado>
{
Task<IEnumerable<object>> Consulta17();
Task<(int totalRegistros, object registros)> Consulta17(int pageIndez, int pageSize, string search); // 1.1
Task<IEnumerable<object>> Consulta22();
Task<IEnumerable<object>> Consulta23();
Task<IEnumerable<object>> Consulta28();
Task<object> Consulta29();//fix
Task<IEnumerable<object>> Consulta35();
Task<IEnumerable<object>> Consulta54();
Task<IEnumerable<object>> Consulta61();
}
