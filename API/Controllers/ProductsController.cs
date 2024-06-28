using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
    {
        try
        {
            var products = await _productService.GetAll();

            return Ok(products);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetById(int id)
    {
        try
        {
            var products = await _productService.GetById(id);

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
    public async Task<ActionResult<ProductDTO>> Post([FromBody] ProductDTO productDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var productExists = await _productService.GetById(productDto.Id);

            if (productExists is not null)
            {
                return BadRequest(new { message = "Product already exists" });
            }

            await _productService.Add(productDto);

            return CreatedAtAction(nameof(GetById), new { Id = productDto.Id }, productDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}