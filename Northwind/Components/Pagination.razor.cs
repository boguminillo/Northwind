using Microsoft.AspNetCore.Components;

namespace Northwind.Components;
public partial class Pagination
{
    [Parameter]
    public int LastPage { get; set; }
    [Parameter]
    // This parameter was needed because binding to the CurrentPage property was not working
    public EventCallback<int> OnPageChange { get; set; }
    private int _currentPage;
    [Parameter]
    //This property is used to validate the value and trigger the OnPageChange event when it is set
    public int CurrentPage
    {
        get => _currentPage;
        set
        {
            if (value != _currentPage && value >= 0 && value <= LastPage)
            {
                _currentPage = value;
                OnPageChange.InvokeAsync(value);
            }
        }
    }
}