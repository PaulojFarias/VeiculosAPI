using MediatR;
using VeiculosAPI.Application.Commands.DeleteVeiculo;
using VeiculosAPI.Domain.Interfaces;

public class DeleteVeiculoHandler : IRequestHandler<DeleteVeiculoCommand, bool>
{
    private readonly IVeiculoRepository _repository;

    public DeleteVeiculoHandler(IVeiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteVeiculoCommand request, CancellationToken cancellationToken)
    {
        var veiculo = await _repository.ObterPorIdAsync(request.Id);

        if (veiculo is null || veiculo.Deletado)
        {
            return false;
        }

        veiculo.Deletar();

        await _repository.AtualizarAsync(veiculo);

        return true;
    }
}