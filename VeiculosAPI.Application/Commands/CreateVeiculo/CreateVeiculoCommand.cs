using MediatR;
using VeiculosAPI.Domain.Enums;

namespace VeiculosAPI.Application.Commands.CreateVeiculo;

public record CreateVeiculoCommand(
    string Descricao,
    Marca Marca,
    string Modelo,
    decimal? Valor,
    string? Opcionais
) : IRequest<int>;