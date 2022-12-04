namespace PcManagingApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PcManagingApi.Data;
using PcManagingApi.Data.Dtos;
using PcManagingApi.Models;

[ApiController]
[Route("[controller]")]
public class PcController : ControllerBase
{

    private PcContext _context;
    private IMapper _mapper;

    public PcController(PcContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um Pc ao banco de dados
    /// </summary>
    /// <param name="pcDto">Objeto com os campos necessários para criação de um pc</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaPc([FromBody] CreatePcDto pcDto)
    {
        Pc pc = _mapper.Map<Pc>(pcDto);
        _context.Pcs.Add(pc);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaPcPorId), new { id = pc.Id }, pc);
    }

    [HttpGet]
    public IEnumerable<ReadPcDto> RecuperaPcs([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _mapper.Map<List<ReadPcDto>>(_context.Pcs.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaPcPorId(int id)
    {
        var pc = _context.Pcs.FirstOrDefault(pcs => pcs.Id == id);
        if (pc == null) return NotFound();
        var pcDto = _mapper.Map<ReadPcDto>(pc);
        return Ok(pcDto);
    }
    [HttpPut("{id}")]
    public IActionResult AtualizaPc(int id, [FromBody] UpdatePcDto pcDto)
    {
        var pc = _context.Pcs.FirstOrDefault(pc => pc.Id == id);
        if (pc == null) return NotFound();
        _mapper.Map(pcDto, pc);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizaPcParcial(int id, JsonPatchDocument<UpdatePcDto> patch)
    {
        var pc = _context.Pcs.FirstOrDefault(pc => pc.Id == id);
        if (pc == null) return NotFound();

        var pcParaAtualizar = _mapper.Map<UpdatePcDto>(pc);
        patch.ApplyTo(pcParaAtualizar, ModelState);
        if (!TryValidateModel(pcParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(pcParaAtualizar, pc);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaPc(int id)
    {
        var pc = _context.Pcs.FirstOrDefault(pc => pc.Id == id);
        if (pc == null) return NotFound();
        _context.Remove(pc);
        _context.SaveChanges(true);
        return NoContent();
    }
}
