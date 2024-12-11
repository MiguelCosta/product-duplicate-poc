namespace ProductDuplicatePoC.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
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

    [HttpGet(Name = "GetProductGroups")]
    public IEnumerable<Dtos.Group> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Dtos.Group
                { })
            .ToArray();
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
