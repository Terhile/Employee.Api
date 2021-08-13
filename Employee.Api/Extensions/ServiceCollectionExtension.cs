using Employee.Api.Domain.Configurations;
using Employee.Api.Services.Kafka;
using Employee.Api.Services.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Employee.Api.Persistence.Repositories.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee.Api.Services.Emp;
using FluentValidation;
using Employee.Api.Domain.Request;
using Employee.Api.Validators;
using Employee.Api.Domain.Models;

namespace Employee.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services, IConfiguration config)
        {

            services.Configure<KafkaSettings>(config.GetSection("Kafka:EmployerSettings"));
            services.AddSingleton<ProducerWrapper>();
            services.AddSingleton<IMessageProducer, MessageProducer>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IActivityLogger, ActivityLogger>();
            services.AddSingleton<IValidator<RequestModel>, RequestModelValidator>();
            services.AddSingleton<IValidator<EmployeeModel>, EmployeeModelValidator>();
            services.AddSingleton<IDBContext, DBContext>();
            services.AddHostedService<ConsumerClient>();

            return services;
        }
    }
}
