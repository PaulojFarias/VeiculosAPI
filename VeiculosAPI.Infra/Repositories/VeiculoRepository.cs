using Microsoft.EntityFrameworkCore;
using VeiculosAPI.Domain.Entities;
using VeiculosAPI.Domain.Interfaces;
using VeiculosAPI.Infra.Context;

namespace VeiculosAPI.Infra.Repositories;

public class VeiculoRepository : IVeiculoRepository
{
    private readonly VeiculosDbContext _context;

    public VeiculoRepository(VeiculosDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Veiculo veiculo)
    {
        await _context.Veiculos.AddAsync(veiculo);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Veiculo veiculo)
    {
        _context.Veiculos.Update(veiculo);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Veiculo veiculo)
    {
        _context.Veiculos.Remove(veiculo);
        await _context.SaveChangesAsync();
    }

    public async Task<Veiculo?> ObterPorIdAsync(int id)
    {
        return await _context.Veiculos.FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<List<Veiculo>> ListarAsync()
    {
        return await _context.Veiculos
            .Where(v => !v.Deletado)
            .ToListAsync();
    }

    public async Task<List<Veiculo>> ListarDeletadosAsync()
    {
        return await _context.Veiculos
            .Where(v => v.Deletado)
            .ToListAsync();
    }
}