using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VeiculosAPI.Application.Behaviors;
using VeiculosAPI.Application.Commands.CreateVeiculo;
using VeiculosAPI.Domain.Interfaces;
using VeiculosAPI.Infra.Context;
using VeiculosAPI.Infra.Repositories;

namespace VeiculosAPI.WebApi.Extensions;

public static class AppServicesExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddDbContext<VeiculosDbContext>(options =>
        {
            //options.UseSqlite("Data Source=app.db");
            options.UseInMemoryDatabase("VeiculosDb");
        });

        services.AddScoped<IVeiculoRepository, VeiculoRepository>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateVeiculoCommand).Assembly));
        services.AddValidatorsFromAssembly(typeof(CreateVeiculoValidator).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}