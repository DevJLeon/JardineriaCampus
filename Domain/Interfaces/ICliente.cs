using Domain.Entities;

namespace Domain.Interfaces;
public interface ICliente : IGenericRepo<Cliente>
{
    Task<IEnumerable<object>> Consulta1();
    Task<(int totalRegistros, object registros)> Consulta1(int pageIndez, int pageSize, string search);
    Task<IEnumerable<object>> Consulta3();
    Task<IEnumerable<object>> Consulta11();
    Task<IEnumerable<object>> Consulta12();
    Task<IEnumerable<object>> Consulta13();
    Task<IEnumerable<object>> Consulta14();
    Task<IEnumerable<object>> Consulta15();
    Task<IEnumerable<object>> Consulta16();
    Task<IEnumerable<object>> Consulta18();
    Task<IEnumerable<object>> Consulta19();
    Task<IEnumerable<object>> Consulta20();
    Task<IEnumerable<object>> Consulta21();
    Task<IEnumerable<object>> Consulta27();
    Task<IEnumerable<object>> Consulta30();
    Task<int> Consulta33(); // fix controller
    Task<IEnumerable<object>> Consulta34();
    Task<int> Consulta36(); // fix controller
    Task<IEnumerable<object>> Consulta37();
    Task<string> Consulta45(); //same
    Task<IEnumerable<object>> Consulta48();
    Task<object> Consulta49(); // bruh
    Task<IEnumerable<object>> Consulta51();
    Task<IEnumerable<object>> Consulta52();
    Task<IEnumerable<object>> Consulta55();
    Task<IEnumerable<object>> Consulta56();
    Task<IEnumerable<object>> Consulta57();
    Task<IEnumerable<object>> Consulta58();
    Task<IEnumerable<object>> Consulta59();
    Task<IEnumerable<object>> Consulta60();
}
