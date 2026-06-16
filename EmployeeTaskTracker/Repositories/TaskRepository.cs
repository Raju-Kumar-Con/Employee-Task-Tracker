using EmployeeTaskTracker.Models;

namespace EmployeeTaskTracker.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private static List<EmployeeTask> tasks = new();

        public List<EmployeeTask> GetAll()
        {
            return tasks;
        }

        public EmployeeTask? GetById(int id)
        {
            return tasks.FirstOrDefault(x => x.Id == id);
        }

        public void Add(EmployeeTask task)
        {
            tasks.Add(task);
        }

        public void Update(EmployeeTask task)
        {
            var existing = GetById(task.Id);

            if (existing != null)
            {
                existing.Title = task.Title;
                existing.Description = task.Description;
                existing.Status = task.Status;
                existing.Deadline = task.Deadline;
            }
        }

        public void Delete(int id)
        {
            var task = GetById(id);

            if (task != null)
                tasks.Remove(task);
        }
    }
}