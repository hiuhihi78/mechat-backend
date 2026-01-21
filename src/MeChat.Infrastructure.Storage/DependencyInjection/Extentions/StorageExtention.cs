using MeChat.Domain.Abstractions.Services.External;
using MeChat.Domain.Shared.Configurations;
using MeChat.Infrastructure.Storage.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastructure.Storage.DependencyInjection.Extentions;
public static class StorageExtention
{

    #region Add Storage
    public static void AddInfrastructureStorage(this IServiceCollection services, IConfiguration configuration)
    {
        DistributedStorage distributedStorageConfig = new();
        configuration.GetSection(nameof(DistributedStorage)).Bind(distributedStorageConfig);

        switch(distributedStorageConfig.Mode) 
        {
            case nameof(DistributedStorage.AwsS3):
                services.AddAmazonS3();
                break;
            case nameof(DistributedStorage.AzureBlobStorage):
                break;
            default:
                services.AddAmazonS3();
                break;
        }
    }
    #endregion

    #region Amazon S3
    public static void AddAmazonS3(this IServiceCollection services)
    {
        services.AddTransient<IStorageService, AwsS3Service>();
    }
    #endregion

    #region Azure Blob Storage
    public static void AddAzureBlobStorage(this IServiceCollection services)
    {
    }
    #endregion
}
