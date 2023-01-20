using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NorthwindAPI.Entities;

namespace NorthwindAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class NorthwindController : ControllerBase
{


    private readonly ILogger<NorthwindController> _logger;
    private readonly NorthwindContext _context;

    public NorthwindController(ILogger<NorthwindController> logger, NorthwindContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("categories")]
    public async Task<ActionResult<dynamic>> GetCategories()
    {
        return Ok(await _context.Categories.Select(c => new { c.CategoryId, c.CategoryName, c.Description }).ToListAsync());
    }

    [HttpGet("categories/{id}")]
    public async Task<ActionResult<dynamic>> GetCategory(int id)
    {
        Category category = await _context.Categories.FindAsync(id);
        return Ok(new {category.CategoryId, category.CategoryName, category.Description});
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

        if (Category == null)
        {
            return NotFound("Category not found");
        }

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

    [HttpGet("customercountries")]
    public async Task<ActionResult<List<string>>> GetCustomerCountries()
    {
        return Ok(await _context.Customers.Select(c => c.Country).Distinct().OrderBy(c => c).ToListAsync());
    }

    [HttpGet("customersbycountry/{country}")]
    public async Task<ActionResult<List<dynamic>>> GetCustomersByCountry(string country)
    {
        return Ok(await _context.Customers.Where(c => c.Country == country)
            .Select(c => new { c.CustomerId, c.CompanyName, c.ContactName, c.ContactTitle, c.Address, c.City, c.Region, c.PostalCode, c.Country, c.Phone, c.Fax, Orders = c.Orders.Count(), CustomerTypes = c.CustomerTypes.Count() })
            .ToListAsync());
    }

    [HttpPost("employees/checkname")]
    public async Task<ActionResult> CheckName(string firstName, string lastName)
    {
        Employee employee = await _context.Employees.FirstOrDefaultAsync(e => e.FirstName == firstName);
        if (employee == null) return NotFound("Employee not found");
        if (employee.LastName.IsNullOrEmpty()) return BadRequest("Employee last name not found");
        if (employee.LastName != lastName) return Unauthorized("Employee first name and last name do not match");
        return Ok();
    }

    [HttpPost("employees/setlastname")]
    public async Task<ActionResult> SetPassword(string firstName, string lastName)
    {
        Employee employee = await _context.Employees.FirstOrDefaultAsync(e => e.FirstName == firstName);
        if (employee == null) return NotFound("Employee not found");
        employee.LastName = lastName;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("customers/{page}")]
    public async Task<ActionResult<dynamic>> GetCustomers(int page)
    {
        int lastPage = (_context.Customers.Count() - 1) / 5 + 1;
        var customers = await _context.Customers.Skip((page - 1) * 5).Take(5).Select(c => new { c.CustomerId, c.CompanyName, c.ContactName }).ToListAsync();
        return Ok(new { LastPage = lastPage, Customers = customers });
    }

    [HttpGet("employees/younger/{date}")]
    public async Task<ActionResult<dynamic>> GetYoungerEmployees(DateTime date)
    {
        return Ok(await _context.Employees.Where(e => e.BirthDate > date).Select(e => new { e.FirstName, e.LastName, e.BirthDate }).ToListAsync());
    }

}