using MediatR;
using VeiculosAPI.Application.DTOs;
using VeiculosAPI.Domain.Interfaces;

namespace VeiculosAPI.Application.Queries.GetAllVeiculos;

public class GetAllVeiculosHandler : IRequestHandler<GetAllVeiculosQuery, IEnumerable<VeiculoDto>>
{
    private readonly IVeiculoRepository _repository;

    public GetAllVeiculosHandler(IVeiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<VeiculoDto>> Handle(GetAllVeiculosQuery request, CancellationToken cancellationToken)
    {
        var veiculos = await _repository.ListarAsync();

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