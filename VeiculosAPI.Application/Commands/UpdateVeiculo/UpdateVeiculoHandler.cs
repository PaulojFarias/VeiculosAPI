using MediatR;
using VeiculosAPI.Domain.Interfaces;

namespace VeiculosAPI.Application.Commands.UpdateVeiculo;

public class UpdateVeiculoHandler : IRequestHandler<UpdateVeiculoCommand, bool>
{
    private readonly IVeiculoRepository _repository;

    public UpdateVeiculoHandler(IVeiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateVeiculoCommand request, CancellationToken cancellationToken)
    {
        var veiculo = await _repository.ObterPorIdAsync(request.Id);

        if (veiculo is null || veiculo.Deletado)
        {
            return false;
        }

        veiculo.Atualizar(
            request.Descricao,
            request.Marca,
            request.Modelo,
            request.Valor,
            request.Opcionais
        );

        await _repository.AtualizarAsync(veiculo);

        return true;
    }
}