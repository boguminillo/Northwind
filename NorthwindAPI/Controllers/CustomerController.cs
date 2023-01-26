using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NorthwindAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{

    private readonly NorthwindContext _context;

    public CustomerController(NorthwindContext context)
    {
        _context = context;
    }

    [HttpGet("customercountries")]
    public async Task<ActionResult<List<string>>> GetCustomerCountries()
    {
        return Ok(await _context.Customers.Select(c => c.Country).Distinct().OrderBy(c => c).ToListAsync());
    }

    [HttpGet("customersbycountry/{country}")]
    public async Task<ActionResult<List<CustomerDTO>>> GetCustomersByCountry(string country)
    {
        return Ok(await _context.Customers.Where(c => c.Country == country)
            .Select(c => new CustomerDTO(c.CustomerId, c.CompanyName, c.ContactName, c.ContactTitle, c.Address, c.City, c.Region, c.PostalCode, c.Country, c.Phone, c.Fax, c.Orders.Count(), c.CustomerTypes.Count()))
            .ToListAsync());
    }

    [HttpGet("customers/{page}")]
    public async Task<ActionResult<CustomerPageDTO>> GetCustomers(int page)
    {
        int lastPage = (_context.Customers.Count() - 1) / 5 + 1;
        List<CustomerShortDTO> customers = await _context.Customers.Skip((page - 1) * 5).Take(5).Select(c => new CustomerShortDTO(c.CustomerId, c.CompanyName, c.ContactName)).ToListAsync();
        return Ok(new CustomerPageDTO { LastPage = lastPage, Customers = customers });
    }
}
