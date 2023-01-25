namespace NorthwindAPI.Models;

public partial class CustomerDTO
{

    public CustomerDTO(string customerId, string companyName, string contactName, string contactTitle, string address, string city, string region, string postalCode, string country, string phone, string fax, int orders, int customerTypes)
    {
        CustomerId = customerId;
        CompanyName = companyName;
        ContactName = contactName;
        ContactTitle = contactTitle;
        Address = address;
        City = city;
        Region = region;
        PostalCode = postalCode;
        Country = country;
        Phone = phone;
        Fax = fax;
        Orders = orders;
        CustomerTypes = customerTypes;
    }

    public string CustomerId { get; set; }

    public string CompanyName { get; set; }

    public string ContactName { get; set; }

    public string ContactTitle { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string Region { get; set; }

    public string PostalCode { get; set; }

    public string Country { get; set; }

    public string Phone { get; set; }

    public string Fax { get; set; }

    public int Orders { get; set; }

    public int CustomerTypes { get; set; }

}
