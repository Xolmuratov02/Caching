namespace Caching.SimpleInfra.Api.Configurations;

public static partial class HostConfiguration
{
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder.AddCaching().AddIdentityInfrastructure().AddExposers();

        return new ValueTask<WebApplicationBuilder>(builder);
    }

    public static async ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        await app.SeedDataAsync();

        app.UseExposers();

        return app;
    }
}