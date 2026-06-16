using EmployeeTaskTracker.Models;

namespace EmployeeTaskTracker.ViewModels
{
    public class EmployeeVM
    {
        public List<Employee> Employees { get; set; } = new();

    public string SearchTerm { get; set; } = string.Empty;

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}