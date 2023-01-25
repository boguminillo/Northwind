using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NorthwindAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{

    private readonly NorthwindContext _context;

    public CategoryController(NorthwindContext context)
    {
        _context = context;
    }

    [HttpGet("categories")]
    public async Task<ActionResult<CategoryDTO>> GetCategories()
    {
        return Ok(await _context.Categories.Select(c => new CategoryDTO(c.CategoryId, c.CategoryName, c.Description)).ToListAsync());
    }

    [HttpGet("categories/{id}")]
    public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
    {
        Category category = await _context.Categories.FindAsync(id);
        return Ok(new CategoryDTO(category.CategoryId, category.CategoryName, category.Description));
    }

    [HttpPost("categories")]
    public async Task<ActionResult> CreateCategory(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("categories/{id}")]
    public async Task<ActionResult> UpdateCategory(Category category)
    {
        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("categories/{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        Category Category = await _context.Categories.FindAsync(id);

        if (Category == null) return NotFound("Category not found");

        try
        {
            _context.Categories.Remove(Category);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.InnerException.Message);
        }
        return Ok();
    }
}
