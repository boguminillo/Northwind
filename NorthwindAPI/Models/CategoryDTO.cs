namespace NorthwindAPI.Models;

public class CategoryDTO
{
    public CategoryDTO(int categoryId, string categoryName, string description)
    {
        CategoryId = categoryId;
        CategoryName = categoryName;
        Description = description;
    }

    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
}
