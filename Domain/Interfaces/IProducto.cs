using Domain.Entities;

namespace Domain.Interfaces;
public interface IProducto : IGenericRepo<Producto>
{
Task<IEnumerable<object>> Consulta10();
Task<(int totalRegistros, object registros)> Consulta10(int pageIndez, int pageSize, string search); // 1.1
Task<IEnumerable<object>> Consulta24();
Task<IEnumerable<object>> Consulta25();
Task<IEnumerable<object>> Consulta40();
Task<IEnumerable<object>> Consulta41();
Task<IEnumerable<object>> Consulta42();
Task<IEnumerable<object>> Consulta43();
Task<string> Consulta46();
Task<object> Consulta47();
Task<object> Consulta50();
}
