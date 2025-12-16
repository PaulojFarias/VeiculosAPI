using MediatR;

namespace VeiculosAPI.Application.Commands.DeleteVeiculo;

public record DeleteVeiculoCommand(int Id) : IRequest<bool>;