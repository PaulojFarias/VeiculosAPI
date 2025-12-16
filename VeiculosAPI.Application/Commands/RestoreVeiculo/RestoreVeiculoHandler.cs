using MediatR;
using VeiculosAPI.Domain.Interfaces;

namespace VeiculosAPI.Application.Commands.RestoreVeiculo;

public class RestoreVeiculoHandler : IRequestHandler<RestoreVeiculoCommand, bool>
{
    private readonly IVeiculoRepository _repository;

    public RestoreVeiculoHandler(IVeiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(RestoreVeiculoCommand request, CancellationToken cancellationToken)
    {
        var veiculo = await _repository.ObterPorIdAsync(request.Id);

        if (veiculo is null)
        {
            return false;
        }

        veiculo.Restaurar();

        await _repository.AtualizarAsync(veiculo);

        return true;
    }
}