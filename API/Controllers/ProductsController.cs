using Application.DTOs.Product.Request;
using Application.DTOs.Product.Response;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAll()
    {
        try
        {
            var products = await productService.GetAll();

            return Ok(products);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetById(int id)
    {
        try
        {
            var products = await productService.GetById(id);

            if (products == null)
            {
                return NotFound("Product not found");
            }

            return Ok(products);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    public async Task<ActionResult<CreateProductRequest>> Post([FromBody] CreateProductRequest productRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var productExists = await productService.GetById(productRequest.Id);

            if (productExists is not null)
            {
                return BadRequest(new { message = "Product already exists" });
            }

            await productService.Add(productRequest);

            return CreatedAtAction(nameof(GetById), new { Id = productRequest.Id }, productRequest);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] CreateProductRequest productRequest)
    {
        if (!ModelState.IsValid || id != productRequest.Id)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // TODO: ajustar o dateCreated e dateUpdated que não estão sendo atualizados corretamente
            await productService.Update(productRequest);

            return Ok(productRequest);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProductResponse>> Delete(int id)
    {
        try
        {
            var product = await productService.GetById(id);

            if (product is null)
            {
                return NotFound("Category not found.");
            }

            await productService.Remove(id);

            return Ok(product);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}