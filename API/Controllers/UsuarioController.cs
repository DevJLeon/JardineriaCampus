using API.Dtos;
using API.Helpers.Errors;
using API.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
/*[ApiVersion("1.0")]
[ApiVersion("1.1")]*/
public class UsuarioController : BaseApiController
{
    private readonly IUserService _Usuarioservice;
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public UsuarioController(IUserService Usuarioservice, IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
        _Usuarioservice = Usuarioservice;
    }
    [HttpGet]
    //[Authorize(Roles = "Administrador")]
    //[MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<UsuarioDto>>> Get()
    {
        var entidad = await unitofwork.Usuarios.GetAllAsync();
        return mapper.Map<List<UsuarioDto>>(entidad);
    }
    [HttpGet("{id}")]
    //[Authorize(Roles = "Administrador")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<UsuarioDto>> Get(int id)
    {
        var entidad = await unitofwork.Usuarios.GetByIdAsync(id);
        if (entidad == null)
        {
            return NotFound();
        }
        return this.mapper.Map<UsuarioDto>(entidad);
    }
    /*[HttpGet]
    //[Authorize(Roles = "Administrador")]
    //[MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<UsuarioDto>>> GetPagination([FromQuery] Params paisParams)
    {
        var entidad = await unitofwork.Usuarios.GetAllAsync(paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
        var listEntidad = mapper.Map<List<UsuarioDto>>(entidad.registros);
        return new Pager<UsuarioDto>(listEntidad, entidad.totalRegistros, paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
    }*/
    [HttpPost("register")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> RegisterAsync(RegisterDto model)
    {
        var result = await _Usuarioservice.RegisterAsync(model);
        return Ok(result);
    }

    [HttpPost("token")]
    public async Task<IActionResult> GetTokenAsync(LoginDto model)
    {
        var result = await _Usuarioservice.GetTokenAsync(model);
        SetRefreshTokenInCookie(result.RefreshToken);
        return Ok(result);
    }
    
    [HttpPost("addrole")]
    //[Authorize(Roles = "Administrador")]
    public async Task<IActionResult> AddRoleAsync(AddRoleDto model)
    {
        var result = await _Usuarioservice.AddRoleAsync(model);
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    [Authorize]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var response = await _Usuarioservice.RefreshTokenAsync(refreshToken);
        if (!string.IsNullOrEmpty(response.RefreshToken))
            SetRefreshTokenInCookie(response.RefreshToken);
        return Ok(response);
    }


    [HttpPut("{id}")]
    //[Authorize(Roles = "Administrador")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<UsuarioDto>> Put(int id, [FromBody] UsuarioDto entidadDto)
    {
        if (entidadDto == null)
        {
            return NotFound();
        }
        var entidad = this.mapper.Map<Usuario>(entidadDto);
        unitofwork.Usuarios.Update(entidad);
        await unitofwork.SaveAsync();
        return entidadDto;
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrador")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var entidad = await unitofwork.Usuarios.GetByIdAsync(id);
        if (entidad == null)
        {
            return NotFound();
        }
        unitofwork.Usuarios.Remove(entidad);
        await unitofwork.SaveAsync();
        return NoContent();
    }
    private void SetRefreshTokenInCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(10),
        };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }

}
