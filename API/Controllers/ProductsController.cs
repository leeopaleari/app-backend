using Application.DTOs.Product;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReadProductDto>>> GetAll()
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
    public async Task<ActionResult<IEnumerable<ReadProductDto>>> GetById(int id)
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
    public async Task<ActionResult<CreateProductDto>> Post([FromBody] CreateProductDto productDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var productExists = await productService.GetById(productDto.Id);

            if (productExists is not null)
            {
                return BadRequest(new { message = "Product already exists" });
            }

            await productService.Add(productDto);

            return CreatedAtAction(nameof(GetById), new { Id = productDto.Id }, productDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] CreateProductDto productDto)
    {
        if (!ModelState.IsValid || id != productDto.Id)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // TODO: ajustar o dateCreated e dateUpdated que não estão sendo atualizados corretamente
            await productService.Update(productDto);

            return Ok(productDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ReadProductDto>> Delete(int id)
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