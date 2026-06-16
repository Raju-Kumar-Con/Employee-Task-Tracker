using EmployeeTaskTracker.Models;

namespace EmployeeTaskTracker.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetAll();

        Employee? GetById(int id);

        void Add(Employee employee);

        void Update(Employee employee);

        void Delete(int id);

        List<Employee> Search(string search);
    }
}