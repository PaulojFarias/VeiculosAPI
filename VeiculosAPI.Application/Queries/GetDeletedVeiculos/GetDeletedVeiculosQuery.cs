using MediatR;
using VeiculosAPI.Application.DTOs;

namespace VeiculosAPI.Application.Queries.GetDeletedVeiculos;

public record GetDeletedVeiculosQuery() : IRequest<IEnumerable<VeiculoDto>>;