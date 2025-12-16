using MediatR;
using VeiculosAPI.Application.DTOs;
using VeiculosAPI.Domain.Interfaces;

namespace VeiculosAPI.Application.Queries.GetVeiculoById;

public class GetVeiculoByIdHandler : IRequestHandler<GetVeiculoByIdQuery, VeiculoDto?>
{
    private readonly IVeiculoRepository _repository;

    public GetVeiculoByIdHandler(IVeiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task<VeiculoDto?> Handle(GetVeiculoByIdQuery request, CancellationToken cancellationToken)
    {
        var veiculo = await _repository.ObterPorIdAsync(request.Id);

        if (veiculo is null || veiculo.Deletado) return null;

        return new VeiculoDto
        {
            Id = veiculo.Id,
            Descricao = veiculo.Descricao,
            Modelo = veiculo.Modelo,
            Marca = veiculo.Marca.ToString(),
            Valor = veiculo.Valor,
            Opcionais = veiculo.Opcionais,
            DataCriacao = veiculo.DataCriacao
        };
    }
}