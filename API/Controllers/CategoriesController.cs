using Application.DTOs.Category;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReadCategoryDto>>> Get()
    {
        try
        {
            var categories = await categoryService.GetAll();

            if (categories == null)
            {
                return NotFound("Categories not found");
            }

            return Ok(categories);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<ReadCategoryDto>> GetById(int id)
    {
        try
        {
            var category = await categoryService.GetById(id);

            if (category is null)
            {
                return NotFound("Category not found");
            }

            return Ok(category);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateCategoryDto categoryDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdCategory = await categoryService.Add(categoryDto);

            return new CreatedAtRouteResult("GetCategory", new { Id = createdCategory.Id }, createdCategory);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] CreateCategoryDto categoryDto)
    {
        if (!ModelState.IsValid || id != categoryDto.Id)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // TODO: ajustar o dateCreated e dateUpdated que não estão sendo atualizados corretamente
            await categoryService.Update(categoryDto);

            return Ok(categoryDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ReadCategoryDto>> Delete(int id)
    {
        try
        {
            var category = await categoryService.GetById(id);

            if (category is null)
            {
                return NotFound("Category not found.");
            }

            await categoryService.Remove(id);

            return Ok(category);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}