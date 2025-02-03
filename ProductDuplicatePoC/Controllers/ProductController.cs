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
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductGroupsController> _logger;
    private readonly ProductService _productService;

    public ProductController(
        ILogger<ProductGroupsController> logger,
        Application.ProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpPost("updateMongo", Name = "Update Products in mongo")]
    public async Task<ActionResult<List<string>>> PostAsync([FromBody] int totalProducts, CancellationToken cancellationToken)
    {
        try
        {
            var changes = await _productService.UpdateProductsRandomAsync(totalProducts, cancellationToken);
            return Ok(changes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
