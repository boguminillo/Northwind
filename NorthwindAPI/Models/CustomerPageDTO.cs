namespace NorthwindAPI.Models;

public class CustomerPageDTO
{
    public int LastPage { get; set; }
    public List<CustomerShortDTO> Customers { get; set; }
}