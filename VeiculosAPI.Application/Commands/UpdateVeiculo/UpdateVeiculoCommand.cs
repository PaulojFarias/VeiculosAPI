using MediatR;
using VeiculosAPI.Domain.Enums;

namespace VeiculosAPI.Application.Commands.UpdateVeiculo;

public record UpdateVeiculoCommand(
    int Id,
    string Descricao,
    Marca Marca,
    string Modelo,
    decimal? Valor,
    string? Opcionais
) : IRequest<bool>;