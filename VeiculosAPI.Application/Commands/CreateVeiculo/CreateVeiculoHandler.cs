using MediatR;
using VeiculosAPI.Application.Commands.CreateVeiculo;
using VeiculosAPI.Domain.Entities;
using VeiculosAPI.Domain.Interfaces;

public class CreateVeiculoHandler : IRequestHandler<CreateVeiculoCommand, int>
{
    private readonly IVeiculoRepository _repository;

    public CreateVeiculoHandler(IVeiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateVeiculoCommand request, CancellationToken cancellationToken)
    {
        var veiculo = new Veiculo(
            request.Descricao,
            request.Marca,
            request.Modelo,
            request.Valor,
            request.Opcionais
        );

        await _repository.AdicionarAsync(veiculo);

        return veiculo.Id;
    }
}