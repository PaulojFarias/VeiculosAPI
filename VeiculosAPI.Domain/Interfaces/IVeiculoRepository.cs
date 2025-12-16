using VeiculosAPI.Domain.Entities;

namespace VeiculosAPI.Domain.Interfaces;

public interface IVeiculoRepository
{
    Task AdicionarAsync(Veiculo veiculo);
    Task AtualizarAsync(Veiculo veiculo);
    Task RemoverAsync(Veiculo veiculo);
    Task<Veiculo?> ObterPorIdAsync(int id);
    Task<List<Veiculo>> ListarAsync();
    Task<List<Veiculo>> ListarDeletadosAsync();
}