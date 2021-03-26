using System.Reflection;
using AutoMapper;
using CaWorkshop.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace CaWorkshop.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IRequestPreProcessor<>), typeof(ValidationBehavior<>));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
