using EmployeeTaskTracker.Models;

namespace EmployeeTaskTracker.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static List<Employee> employees = new();

        public List<Employee> GetAll()
        {
            return employees;
        }

        public Employee? GetById(int id)
        {
            return employees.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Employee employee)
        {
            employees.Add(employee);
        }

        public void Update(Employee employee)
        {
            var emp = GetById(employee.Id);

            if(emp != null)
            {
                emp.FirstName = employee.FirstName;
                emp.LastName = employee.LastName;
                emp.Email = employee.Email;
                emp.PhoneNumber = employee.PhoneNumber;
            }
        }

        public void Delete(int id)
        {
            var emp = GetById(id);

            if(emp != null)
                employees.Remove(emp);
        }
    }
}