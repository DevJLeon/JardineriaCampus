using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Rol,RolDto>().ReverseMap();
        CreateMap<Usuario,UsuarioDto>().ReverseMap();
        CreateMap<Cliente,ClienteDto>().ReverseMap();
        CreateMap<DetallePedido,DetallePedidoDto>().ReverseMap();
        CreateMap<Empleado,EmpleadoDto>().ReverseMap();
        CreateMap<GamaProducto,GamaProductoDto>().ReverseMap();
        CreateMap<Oficina,OficinaDto>().ReverseMap();
        CreateMap<Pago,PagoDto>().ReverseMap();
        CreateMap<Pedido,PedidoDto>().ReverseMap();
        CreateMap<Producto,ProductoDto>().ReverseMap();
        
    }
}
