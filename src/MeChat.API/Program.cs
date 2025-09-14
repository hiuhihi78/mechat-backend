using MeChat.Persistence.DependencyInjection.Extentions;
using MeChat.Infrastucture.Dapper.DependencyInjection.Extentions;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using MeChat.API.DependencyInjection.Extentions;
using MeChat.Application.DependencyInjection.Extentions;
using MeChat.API.Middlewares;
using MeChat.Infrastucture.DistributedCache.DependencyInjection.Extentions;
using System.Text.Json.Serialization;
using MeChat.Infrastucture.Service.DependencyInjection.Extentions;
using MeChat.Infrastucture.Storage.DependencyInjection.Extentions;
using MeChat.Infrastucture.MessageBroker.Producer.Email.DependencyInjection.Extentions;
using MeChat.Infrastructure.RealTime.DependencyInjection.Extentions;

namespace MeChat.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

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
                policy.AllowCredentials().AllowAnyHeader().AllowAnyMethod();
            });
        });

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
            .AddApiVersioning(options => options.ReportApiVersions = true)
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        //Add application utils
        builder.Services.AddApplicationUtils();

        //Add messagebroker (Infrastucture.MessageBroker)
        builder.Services.AddMessageBroker(builder.Configuration);

        //Add configuration MediatR(Application)
        builder.Services.AddMediator();

        //Add configuration AutoMapper(Application)
        builder.Services.AddMapperObjects();

        //Add configuration storage
        builder.Services.AddStorage(builder.Configuration);

        //Add configuration connect SQL Server with Dapper(Infrastucture.Dapper)
        builder.Services.AddSqlServerDapper();

        //Add configuration Jwt Authentication (Infrastucture.Jwt)
        builder.Services.AddJwtAuthentication(builder.Configuration);

        //Add configuration Jwt Service (Infrastucture.Jwt)
        builder.Services.AddJwtService();

        // Add Message broker producer for email //Infrastucture.MessageBroker.Producer.Email
        builder.Services.AddMessageBrokerProducerEmail();

        //Add configuration Redis(Infrastucture.Redis)
        builder.Services.AddDistributedCache(builder.Configuration);
        
        //Add configuration connect SQL Server with EF(Infrastucture.Persistence)
        builder.Services.AddSqlServerEntityFramwork();

        //Add controller API (Infrastucture.Presentation)
        builder.Services
            .AddControllers()
            .AddApplicationPart(Presentation.AssemblyReference.Assembly);

        //Add Middlewares
        builder.Services.AddTransient<ExceptionHandlingMiddleware>();

        //Add realtime (Infrastucture.RealTime)
        builder.Services.AddRealTime();

        // Use remove cycle object's data in json respone
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
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