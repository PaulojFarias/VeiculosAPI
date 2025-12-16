using MediatR;
using VeiculosAPI.Domain.Entities;
using VeiculosAPI.Application.DTOs;

namespace VeiculosAPI.Application.Queries.GetVeiculoById;

public record GetVeiculoByIdQuery(int Id) : IRequest<VeiculoDto?>;