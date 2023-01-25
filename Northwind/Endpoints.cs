namespace Northwind;

public class Endpoints
{

    public static string GetPostCategories()
    {
        return "https://localhost:7136/api/Category/categories";
    }

    public static string UpdateDeleteCategory(int id)
    {
        return $"https://localhost:7136/api/Category/categories/{id}";
    }

    public static string GetCustomerCountries()
    {
        return "https://localhost:7136/api/Customer/customercountries";
    }

    public static string GetCustomersByCountry(string country)
    {
        return $"https://localhost:7136/api/Customer/customersbycountry/{country}";
    }

    public static string GetCustomersPage(int page)
    {
        return $"https://localhost:7136/api/Customer/customers/{page}";
    }

    public static string CheckEmployeeName()
    {
        return "https://localhost:7136/api/Employee/employees/checkname";
    }

    public static string CreateEmployee()
    {
        return "https://localhost:7136/api/Employee/employees";
    }

    public static string GetUpdateDeleteEmployee(int id)
    {
        return $"https://localhost:7136/api/Employee/employees/{id}";
    }

    public static string SetEmployeeLastName()
    {
        return "https://localhost:7136/api/Employee/employees/setlastname";
    }

    public static string GetYoungerEmployees(DateTime date)
    {
        return $"https://localhost:7136/api/Employee/employees/younger/{date:yyyy-MM-dd}";
    }
    
    public static string SendEmployeeViaMail(){
        return "https://localhost:7136/api/Employee/employees/mail";
    }

}