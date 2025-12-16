namespace VeiculosAPI.Application.DTOs;

public class VeiculoDto
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public decimal? Valor { get; set; }
    public string? Opcionais { get; set; }
    public DateTime DataCriacao { get; set; }
}