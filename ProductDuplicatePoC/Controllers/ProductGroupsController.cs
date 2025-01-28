namespace ProductDuplicatePoC.Controllers;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductDuplicatePoC.Application;

[ApiController]
[Route("[controller]")]
public class ProductGroupsController : ControllerBase
{
    private readonly ILogger<ProductGroupsController> _logger;
    private readonly GroupService _groupService;

    public ProductGroupsController(
        ILogger<ProductGroupsController> logger,
        Application.GroupService groupService)
    {
        _logger = logger;
        _groupService = groupService;
    }

    [HttpPost("fromMongo", Name = "GetProductGroupsMongo")]
    public async Task<ActionResult<List<Dtos.Group>>> GetFromMongo(
        [FromBody] Dtos.GroupFilter filter,
        CancellationToken cancellationToken)
    {
        var results = await _groupService.GetFromMongoAsync(filter, cancellationToken);

        return Ok(results);
    }

    [HttpPost("fromSqlServer", Name = "GetProductGroupsSqlServer")]
    public async Task<ActionResult<List<Dtos.Group>>> GetFromSqlServer(
        [FromBody] Dtos.GroupFilter filter,
        CancellationToken cancellationToken)
    {
        // Change to SQL SERVER
        var results = await _groupService.GetFromSQLAsync(filter, cancellationToken);

        return Ok(results);
    }

    [HttpPost("generate", Name = "GenerateProductGroups")]
    public async Task<ActionResult> PostAsync([FromBody] int totalGroups)
    {
        try
        {
            await _groupService.GenerateGroupsAsync(totalGroups);
            return Accepted();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
