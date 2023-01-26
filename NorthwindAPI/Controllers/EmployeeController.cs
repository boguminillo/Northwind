using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Services.MailService;

namespace NorthwindAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{

    private readonly NorthwindContext _context;
    private readonly IMailService _mailService;

    public EmployeeController(NorthwindContext context, IMailService mailService)
    {
        _context = context;
        _mailService = mailService;
    }

    [HttpPost("employees/checkname")]
    public async Task<ActionResult> CheckName(Employee employee)
    {
        Employee dbEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.FirstName == employee.FirstName && e.LastName == employee.LastName);
        if (dbEmployee == null) return NotFound("Employee not found");
        return Ok(dbEmployee.EmployeeId);
    }

    [HttpGet("employees/{id}")]
    public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
    {
        Employee employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound("Employee not found");
        return Ok(employee);
    }

    [HttpPost("employees")]
    public async Task<ActionResult> CreateEmployee(Employee employee)
    {
        try
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.InnerException.Message);
        }
        return Ok();
    }

    [HttpPut("employees/{id}")]
    public async Task<ActionResult> UpdateEmployee(Employee employee)
    {
        try
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.InnerException.Message);
        }
        return Ok();
    }

    [HttpDelete("employees/{id}")]
    public async Task<ActionResult> DeleteEmployee(int id)
    {
        Employee employee = await _context.Employees.FindAsync(id);

        if (employee == null) return NotFound("Employee not found");

        try
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.InnerException.Message);
        }
        return Ok();
    }

    [HttpGet("employees/younger/{date}")]
    public async Task<ActionResult<dynamic>> GetYoungerEmployees(DateTime date)
    {
        return Ok(await _context.Employees.Where(e => e.BirthDate > date).Select(e => new { e.FirstName, e.LastName, e.BirthDate }).ToListAsync());
    }

    [HttpPost("employees/mail")]
    public ActionResult SendMail(MailToDTO data)
    {
        MailDTO mail = new();
        mail.To = data.mailTo;
        mail.Subject = "This is your Northwind employee data";
        EmployeeDTO employee = data.employee;
        mail.Body = $@"<h1>Employee data</h1>
                        <p>First name: {employee.FirstName}</p>
                        <p>Last name: {employee.LastName}</p>
                        <p>Title: {employee.Title}</p>
                        <p>Title of courtesy: {employee.TitleOfCourtesy}</p>
                        <p>Birth date: {employee.BirthDate}</p>
                        <p>Hire date: {employee.HireDate}</p>
                        <p>Address: {employee.Address}</p>
                        <p>City: {employee.City}</p>
                        <p>Region: {employee.Region}</p>
                        <p>Postal code: {employee.PostalCode}</p>
                        <p>Country: {employee.Country}</p>
                        <p>Home phone: {employee.HomePhone}</p>
                        <p>Extension: {employee.Extension}</p>";

        _mailService.SendMail(mail);

        return Ok();
    }

}