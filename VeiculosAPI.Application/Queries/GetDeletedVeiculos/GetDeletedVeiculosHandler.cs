using MediatR;
using VeiculosAPI.Application.DTOs;
using VeiculosAPI.Domain.Interfaces;

namespace VeiculosAPI.Application.Queries.GetDeletedVeiculos;

public class GetDeletedVeiculosHandler : IRequestHandler<GetDeletedVeiculosQuery, IEnumerable<VeiculoDto>>
{
    private readonly IVeiculoRepository _repository;

    public GetDeletedVeiculosHandler(IVeiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<VeiculoDto>> Handle(GetDeletedVeiculosQuery request, CancellationToken cancellationToken)
    {
        var veiculos = await _repository.ListarDeletadosAsync();

        return veiculos.Select(v => new VeiculoDto
        {
            Id = v.Id,
            Descricao = v.Descricao,
            Marca = v.Marca.ToString(),
            Modelo = v.Modelo,
            Valor = v.Valor,
            Opcionais = v.Opcionais,
            DataCriacao = v.DataCriacao
        });
    }
}