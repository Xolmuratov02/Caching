using Caching.SimpleInfra.Domain.Common.Entities;

namespace Caching.SimpleInfra.Domain.Entities;

public class User : IEntity
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;
}