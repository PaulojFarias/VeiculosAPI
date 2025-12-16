using VeiculosAPI.Domain.Enums;

namespace VeiculosAPI.Domain.Entities;

public class Veiculo
{
    protected Veiculo() { }

    public Veiculo(string descricao, Marca marca, string modelo, decimal? valor, string? opcionais)
    {
        Descricao = descricao;
        Marca = marca;
        Modelo = modelo;
        Valor = valor;
        Opcionais = opcionais;
        DataCriacao = DateTime.Now;
        Deletado = false;
    }

    public int Id { get; private set; }
    public string Descricao { get; private set; } = string.Empty;
    public Marca Marca { get; private set; }
    public string Modelo { get; private set; } = string.Empty;
    public decimal? Valor { get; private set; }
    public string? Opcionais { get; private set; }
    public bool Deletado { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime? DataAlteracao { get; private set; }

    public void Atualizar(string descricao, Marca marca, string modelo, decimal? valor, string? opcionais)
    {
        Descricao = descricao;
        Marca = marca;
        Modelo = modelo;
        Valor = valor;
        Opcionais = opcionais;
        DataAlteracao = DateTime.Now;
    }

    public void Deletar()
    {
        Deletado = true;
        DataAlteracao = DateTime.Now;
    }

    public void Restaurar()
    {
        Deletado = false;
        DataAlteracao = DateTime.Now;
    }
}