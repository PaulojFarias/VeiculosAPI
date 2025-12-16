using MediatR;
using VeiculosAPI.Application.DTOs;

namespace VeiculosAPI.Application.Queries.GetAllVeiculos;

public record GetAllVeiculosQuery() : IRequest<IEnumerable<VeiculoDto>>;