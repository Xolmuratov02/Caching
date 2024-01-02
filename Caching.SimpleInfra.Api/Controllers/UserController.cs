using Caching.SimpleInfra.Application.Common.Identity.Services;
using Caching.SimpleInfra.Domain.Common.Query;
using Caching.SimpleInfra.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Caching.SimpleInfra.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] FilterPagination paginationOptions, CancellationToken cancellationToken = default)
    {
        var specificationA = new QuerySpecification<User>(paginationOptions.PageSize, paginationOptions.PageToken);

        specificationA.FilteringOptions.Add(user => user.FirstName.Length > 4);
        specificationA.FilteringOptions.Add(user => user.LastName.Length > 5);

        var specificationB = new QuerySpecification<User>(paginationOptions.PageSize, paginationOptions.PageToken);

        specificationB.FilteringOptions.Add(user => user.LastName.Length > 5);
        specificationB.FilteringOptions.Add(user => user.FirstName.Length > 4);

        var resultA = await userService.GetAsync(specificationA, true, cancellationToken);
        var resultB = await userService.GetAsync(specificationB, true, cancellationToken);

        return resultA.Any() ? Ok(resultA) : NotFound();
    }

    [HttpGet("{userId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid userId)
    {
        var result = await userService.GetByIdAsync(userId);
        return result is not null ? Ok(result) : NotFound();
    }
}