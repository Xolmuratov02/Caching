using Bogus;
using Caching.SimpleInfra.Domain.Entities;
using Caching.SimpleInfra.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace Caching.SimpleInfra.Api.Data;

public static class SeedDataExtensions
{
    public static async ValueTask InitializeSeedAsync(this IServiceProvider serviceProvider)
    {
        var identityDbContext = serviceProvider.GetRequiredService<IdentityDbContext>();

        if (!await identityDbContext.Users.AnyAsync())
            await identityDbContext.SeedUsersAsync();
    }

    private static async ValueTask SeedUsersAsync(this IdentityDbContext identityDbContext)
    {
        var userFaker = new Faker<User>().RuleFor(user => user.FirstName, faker => faker.Person.FirstName)
            .RuleFor(user => user.LastName, faker => faker.Person.LastName);

        await identityDbContext.Users.AddRangeAsync(userFaker.Generate(10000));
        await identityDbContext.SaveChangesAsync();
    }
}