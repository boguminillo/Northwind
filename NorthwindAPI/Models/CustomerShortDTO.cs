namespace NorthwindAPI.Models;

public class CustomerShortDTO
{
    public CustomerShortDTO(string customerId, string companyName, string contactName)
    {
        CustomerId = customerId;
        CompanyName = companyName;
        ContactName = contactName;
    }

    public string CustomerId { get; set; }

    public string CompanyName { get; set; }

    public string ContactName { get; set; }
}
