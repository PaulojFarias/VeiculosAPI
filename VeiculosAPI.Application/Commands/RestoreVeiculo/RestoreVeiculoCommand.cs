using MediatR;

namespace VeiculosAPI.Application.Commands.RestoreVeiculo;

public record RestoreVeiculoCommand(int Id) : IRequest<bool>;