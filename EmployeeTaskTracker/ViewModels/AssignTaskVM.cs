using EmployeeTaskTracker.Models;

namespace EmployeeTaskTracker.ViewModels
{
    public class AssignTaskVM
    {
        public int EmployeeId { get; set; }

        public int TaskId { get; set; }

      public List<Employee> Employees { get; set; } = new();

public List<EmployeeTask> Tasks { get; set; } = new();
    }
}