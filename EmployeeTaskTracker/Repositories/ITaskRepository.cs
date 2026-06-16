using EmployeeTaskTracker.Models;

namespace EmployeeTaskTracker.Repositories
{
    public interface ITaskRepository
    {
        List<EmployeeTask> GetAll();
        EmployeeTask? GetById(int id);
        void Add(EmployeeTask task);
        void Update(EmployeeTask task);
        void Delete(int id);
    }
}