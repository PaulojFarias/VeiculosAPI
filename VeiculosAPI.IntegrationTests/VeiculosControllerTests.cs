using System.Net;
using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using VeiculosAPI.Application.DTOs;

namespace VeiculosAPI.IntegrationTests;

public class VeiculosControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public VeiculosControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    [Fact]
    public async Task FluxoCompleto_DeveCriarConsultarERemoverVeiculo()
    {
        // 1. ARRANGE
        var novoVeiculo = new
        {
            Descricao = "Toyota Corolla Teste",
            Marca = 1,
            Modelo = "XEI 2.0",
            Valor = 150000.00,
            Opcionais = "Ar condicionado, Direção"
        };

        // 2. ACT (Criar - POST)
        var responsePost = await _client.PostAsJsonAsync("/api/Veiculos", novoVeiculo);

        // ASSERT POST
        responsePost.StatusCode.Should().Be(HttpStatusCode.Created);

        var jsonResponse = await responsePost.Content.ReadAsStringAsync();
        var veiculoCriado = JsonSerializer.Deserialize<VeiculoDto>(jsonResponse, _jsonOptions);

        veiculoCriado.Should().NotBeNull();
        veiculoCriado!.Id.Should().BeGreaterThan(0);

        // 3. ACT (Consultar - GET Por ID)
        var responseGet = await _client.GetAsync($"/api/Veiculos/{veiculoCriado.Id}");

        // ASSERT GET
        responseGet.StatusCode.Should().Be(HttpStatusCode.OK);

        var veiculoConsultado = await responseGet.Content.ReadFromJsonAsync<VeiculoDto>(_jsonOptions);

        veiculoConsultado.Should().NotBeNull();
        veiculoConsultado!.Id.Should().Be(veiculoCriado.Id);
        veiculoConsultado.Descricao.Should().Be(novoVeiculo.Descricao);
        veiculoConsultado.Modelo.Should().Be(novoVeiculo.Modelo);

        // 4. ACT (Remover - DELETE)
        var responseDelete = await _client.DeleteAsync($"/api/Veiculos/{veiculoCriado.Id}");

        // ASSERT DELETE
        responseDelete.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // 5. ACT (Verificar Soft Delete)
        var responseGetDeleted = await _client.GetAsync($"/api/Veiculos/{veiculoCriado.Id}");

        // ASSERT SOFT DELETE
        responseGetDeleted.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Post_DeveRetornarErro_QuandoDadosInvalidos()
    {
        // ARRANGE
        var veiculoInvalido = new
        {
            Descricao = "",
            Marca = 1,
            Modelo = "Modelo Inválido",
            Valor = -10.00
        };

        // ACT
        var response = await _client.PostAsJsonAsync("/api/Veiculos", veiculoInvalido);

        // ASSERT
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var erro = await response.Content.ReadAsStringAsync();
        erro.Should().Contain("Descri");
    }

    [Fact]
    public async Task GetAll_DeveRetornarLista()
    {
        // ACT
        var response = await _client.GetAsync("/api/Veiculos");

        // ASSERT
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var lista = await response.Content.ReadFromJsonAsync<List<VeiculoDto>>(_jsonOptions);
        lista.Should().NotBeNull();
    }
}