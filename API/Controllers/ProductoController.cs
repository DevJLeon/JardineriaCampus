using API.Dtos;
using API.Helpers.Errors;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class ProductoController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public ProductoController( IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get()
    {
        var entidad = await unitofwork.Productos.GetAllAsync();
        return mapper.Map<List<ProductoDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductoDto>> Get(int id)
    {
        var entidad = await unitofwork.Productos.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return this.mapper.Map<ProductoDto>(entidad);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Producto>> Post(ProductoDto entidadDto)
    {
        var entidad = this.mapper.Map<Producto>(entidadDto);
        this.unitofwork.Productos.Add(entidad);
        await unitofwork.SaveAsync();
        if(entidad == null)
        {
            return BadRequest();
        }
        entidadDto.CodigoProducto = entidad.CodigoProducto;
        return CreatedAtAction(nameof(Post), new {id = entidadDto.CodigoProducto}, entidadDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<Producto>> Put(int id, [FromBody]Producto entidadDto){
        if(entidadDto == null)
        {
            return NotFound();
        }
        var entidad = this.mapper.Map<Producto>(entidadDto);
        unitofwork.Productos.Update(entidad);
        await unitofwork.SaveAsync();
        return entidadDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var entidad = await unitofwork.Productos.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        unitofwork.Productos.Remove(entidad);
        await unitofwork.SaveAsync();
        return NoContent();
    }
    [HttpGet("consulta10")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> Consulta10()
    {
        var entidad = await unitofwork.Productos.Consulta10();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }

    [HttpGet("Consulta10")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<object>>> Consulta10([FromQuery] Params paisParams)
    {
        var entidad = await unitofwork.Productos.Consulta10(paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
        var listEntidad = mapper.Map<List<object>>(entidad.registros);
        return new Pager<object>(listEntidad, entidad.totalRegistros, paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
    }

    [HttpGet("consulta24")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> Consulta24()
    {
        var entidad = await unitofwork.Productos.Consulta24();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }
    [HttpGet("consulta25")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> Consulta25()
    {
        var entidad = await unitofwork.Productos.Consulta25();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }
    [HttpGet("consulta40")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> Consulta40()
    {
        var entidad = await unitofwork.Productos.Consulta40();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }
    [HttpGet("consulta41")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> Consulta41()
    {
        var entidad = await unitofwork.Productos.Consulta41();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }
    [HttpGet("consulta42")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> Consulta42()
    {
        var entidad = await unitofwork.Productos.Consulta42();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }
    [HttpGet("consulta43")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> Consulta43()
    {
        var entidad = await unitofwork.Productos.Consulta43();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }
    [HttpGet("consulta46")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> Consulta46()
    {
        var entidad = await unitofwork.Productos.Consulta46();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }
    [HttpGet("consulta47")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> Consulta47()
    {
        var entidad = await unitofwork.Productos.Consulta47();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }
    [HttpGet("consulta50")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> Consulta50()
    {
        var entidad = await unitofwork.Productos.Consulta50();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }
}