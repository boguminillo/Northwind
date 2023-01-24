namespace Northwind;

public class Endpoints
{

    public static string GetPostCategories()
    {
        return "https://localhost:7136/northwind/categories";
    }

    public static string UpdateDeleteCategory(int id)
    {
        return $"https://localhost:7136/northwind/categories/{id}";
    }

    public static string GetCustomerCountries()
    {
        return "https://localhost:7136/northwind/customercountries";
    }

    public static string GetCustomersByCountry(string country)
    {
        return $"https://localhost:7136/northwind/customersbycountry/{country}";
    }

    public static string GetCustomersPage(int page)
    {
        return $"https://localhost:7136/northwind/customers/{page}";
    }

    public static string CheckEmployeeName()
    {
        return "https://localhost:7136/northwind/employees/checkname";
    }

    public static string CreateEmployee()
    {
        return "https://localhost:7136/northwind/employees";
    }

    public static string GetUpdateDeleteEmployee(int id)
    {
        return $"https://localhost:7136/northwind/employees/{id}";
    }

    public static string SetEmployeeLastName()
    {
        return "https://localhost:7136/northwind/employees/setlastname";
    }

    public static string GetYoungerEmployees(DateTime date)
    {
        return $"https://localhost:7136/northwind/employees/younger/{date:yyyy-MM-dd}";
    }

}
