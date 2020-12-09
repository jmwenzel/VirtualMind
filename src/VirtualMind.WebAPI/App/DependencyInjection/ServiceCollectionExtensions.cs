using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace VirtualMind.WebAPI.App.DependencyInjection
{
    using AutoMapper;
    using Infrastructure.Core;
    using Infrastructure.Proxies;
    using Service;
    using VirtualMind.WebAPI.Infrastructure.Data;

    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Dependencies
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddDependendcies(this IServiceCollection services,
            IConfiguration configuration)
        {
            //Settings
            services.Configure<ExchangeProviderSettings>(configuration.GetSection("ExchangeProviders"));
            services.Configure<ThresholdSettings>(configuration.GetSection("Threshold"));
            //
            services.AddTransient<IExchange, DolarExchange>();
            services.AddTransient<IExchange, RealExchange>();
            services.AddTransient<ICurrencyProxy, CurrencyProxy>();
            services.AddTransient<ITransactionProxy, TransactionProxy>();

            #region Repositories DI 
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            #endregion

            // Automapper
            services.AddAutoMapper(typeof(Startup));
        }
        /// <summary>
        /// Register swagger
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterSwagger(this IServiceCollection services)
        {
            // Register api versioning support
            services.AddApiVersioning(options => options.ReportApiVersions = true);
            services.AddVersionedApiExplorer(
                options =>
                {
                    //The format of the version added to the route URL  
                    options.GroupNameFormat = "'v'VVV";
                    //Tells swagger to replace the version in the controller route  
                    options.SubstituteApiVersionInUrl = true;
                });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Virtual Mind Web API",
                    Version = "v1"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
