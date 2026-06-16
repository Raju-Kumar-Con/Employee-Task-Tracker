using EmployeeTaskTracker.Models;
using EmployeeTaskTracker.Repositories;

namespace EmployeeTaskTracker.Services
{
    public class EmployeeService : IEmployeeService
    {
        public void Add(Employee employee)
        {
            try
            {
                employee.Id = MemoryRepository.Employees.Any()
                    ? MemoryRepository.Employees.Max(x => x.Id) + 1
                    : 1;

                MemoryRepository.Employees.Add(employee);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Add Employee Failed: " + ex.Message);
            }
        }

        public void Delete(int id)
        {
            var emp = MemoryRepository
                .Employees
                .FirstOrDefault(x => x.Id == id);

            if (emp != null)
            {
                MemoryRepository.Employees.Remove(emp);
            }
        }

        public List<Employee> GetAll()
        {
            return MemoryRepository.Employees;
        }

        public Employee? GetById(int id)
        {
            return MemoryRepository
                .Employees
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Employee> Search(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return GetAll();

            return MemoryRepository.Employees
                .Where(x =>
                    x.FirstName.Contains(
                        search,
                        StringComparison.OrdinalIgnoreCase)
                    ||
                    x.LastName.Contains(
                        search,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void Update(Employee employee)
        {
            var oldEmployee = GetById(employee.Id);

            if (oldEmployee != null)
            {
                oldEmployee.FirstName = employee.FirstName;
                oldEmployee.LastName = employee.LastName;
                oldEmployee.Email = employee.Email;
                oldEmployee.PhoneNumber = employee.PhoneNumber;
                oldEmployee.Age = employee.Age;
                oldEmployee.Gender = employee.Gender;
                oldEmployee.Hobbies = employee.Hobbies;
                oldEmployee.DOB = employee.DOB;
                oldEmployee.FavoriteColor = employee.FavoriteColor;
                oldEmployee.SkillRange = employee.SkillRange;
                oldEmployee.Password = employee.Password;
                oldEmployee.FileName = employee.FileName;
            }
        }
    }
}