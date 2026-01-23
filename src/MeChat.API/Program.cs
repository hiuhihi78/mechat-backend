using Asp.Versioning;
using MeChat.API.DependencyInjection;
using MeChat.API.DependencyInjection.Extentions;
using MeChat.API.Middlewares;
using MeChat.Domain.DependencyInjection.Extentions;
using MeChat.Application.DependencyInjection.Extentions;
using MeChat.Infrastructure.Dapper.DependencyInjection.Extentions;
using MeChat.Infrastructure.DistributedCache.DependencyInjection.Extentions;
using MeChat.Infrastructure.MessageBroker.Producer.DependencyInjection.Extentions;
using MeChat.Infrastructure.Persistence.DependencyInjection.Extentions;
using MeChat.Infrastructure.RealTime.DependencyInjection.Extentions;
using MeChat.Infrastructure.Service.DependencyInjection.Extentions;
using MeChat.Infrastructure.Storage.DependencyInjection.Extentions;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using System.Text.Json.Serialization;

namespace MeChat.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddSwaggerGen();

        //Add configuration Swagger with api versioning (API)
        builder.Services
            .AddSwaggerGenNewtonsoftSupport()
            .AddFluentValidationRulesToSwagger()
            .AddEndpointsApiExplorer()
            .AddConfigurationSwagger();

        //Add configuration Api versioning
        builder.Services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Api-Version")
                );
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        // Add configuration Domain services
        builder.Services.AddDomainServices();

        // Add configuration API services
        builder.Services.AddApiServices();

        // Add configuration Application services
        builder.Services.AddApplicationServices();

        // Add configuration Infrastructure.Dapper
        builder.Services.AddInfrastructureDapper();

        // Add configuration Infrastructure.DistributedCache
        builder.Services.AddInfrastructureDistributedCache(builder.Configuration);

        //Add configuration Infrastructure.MessageBroker
        builder.Services.AddInfrastructureMessageBroker(builder.Configuration);

        // Add Message broker producer for email //Infrastructure.MessageBroker.Producer.Email
        builder.Services.AddMessageBrokerProducerEmail();

        //Add configuration Infrastructure.Storage
        builder.Services.AddInfrastructureStorage(builder.Configuration);

        //Add configuration Jwt Authentication
        builder.Services.AddJwtAuthentication(builder.Configuration);

        //Add configuration Jwt Servic
        builder.Services.AddJwtService();

        //Add configuration Persistence
        builder.Services.AddPersistence();

        //Add controller API (Presentation)
        builder.Services
            .AddControllers()
            .AddApplicationPart(MeChat.Presentation.AssemblyReference.Assembly);

        //Add Middlewares
        builder.Services.AddTransient<ExceptionHandlingMiddleware>();

        //Add realtime (Infrastructure.RealTime)
        builder.Services.AddRealTime();

        // Use remove cycle object's data in json respone
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        //Add Cors
        builder.Services.AddCors(options =>
        {
            //var origins = builder.Configuration.GetSection("CorsOrigns").Get<string[]>()!;
            //options.AddDefaultPolicy(policy =>
            //{
            //    policy.WithOrigins(origins).AllowCredentials().AllowAnyHeader().AllowAnyMethod();
            //});

            // Allow all origin
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });

        var app = builder.Build();

        // Develop enviroment
        if (app.Environment.IsDevelopment())
        {
            // Use swagger
            app.UseConfigurationSwagger();
        }

        // Staging enviroment
        if (app.Environment.IsStaging())
        {
            // Use swagger
            app.UseConfigurationSwagger();
        }

        // Production enviroment
        if (app.Environment.IsProduction())
        {
            // Use swagger
            app.UseConfigurationSwagger();
        }

        //Use CORS
        app.UseCors();

        //Use middlewares
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseRouting();

        //Use authentication
        app.UseAuthentication();

        //Use authorization
        app.UseAuthorization();

        app.MapControllers();

        //Mapping hubs
        app.MapRealTimeEndpoints();

        app.Run();
    }
}