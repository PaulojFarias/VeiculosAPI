using MediatR;
using Microsoft.AspNetCore.Mvc;
using VeiculosAPI.Application.Commands.CreateVeiculo;
using VeiculosAPI.Application.Commands.DeleteVeiculo;
using VeiculosAPI.Application.Commands.RestoreVeiculo;
using VeiculosAPI.Application.Commands.UpdateVeiculo;
using VeiculosAPI.Application.DTOs;
using VeiculosAPI.Application.Queries.GetAllVeiculos;
using VeiculosAPI.Application.Queries.GetDeletedVeiculos;
using VeiculosAPI.Application.Queries.GetVeiculoById;
using VeiculosAPI.Domain.Entities;

namespace VeiculosAPI.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VeiculosController : ControllerBase
{
    private readonly IMediator _mediator;

    public VeiculosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] CreateVeiculoCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(ObterPorId), new { id = id }, new { id });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(VeiculoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var query = new GetVeiculoByIdQuery(id);
        var veiculoDto = await _mediator.Send(query);

        if (veiculoDto is null) return NotFound();

        return Ok(veiculoDto);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VeiculoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar()
    {
        var query = new GetAllVeiculosQuery();
        var veiculos = await _mediator.Send(query);
        return Ok(veiculos);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(int id, [FromBody] UpdateVeiculoCommand command)
    {
        if (id != command.Id) return BadRequest("ID incompatível.");

        var sucesso = await _mediator.Send(command);

        if (!sucesso) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Excluir(int id)
    {
        var command = new DeleteVeiculoCommand(id);
        var sucesso = await _mediator.Send(command);

        if (!sucesso) return NotFound();

        return NoContent();
    }

    [HttpGet("deletados")]
    [ProducesResponseType(typeof(IEnumerable<VeiculoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarDeletados()
    {
        var query = new GetDeletedVeiculosQuery();
        var veiculos = await _mediator.Send(query);
        return Ok(veiculos);
    }

    [HttpPut("{id}/restaurar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Restaurar(int id)
    {
        var command = new RestoreVeiculoCommand(id);
        var sucesso = await _mediator.Send(command);

        if (!sucesso) return NotFound();

        return NoContent();
    }
}