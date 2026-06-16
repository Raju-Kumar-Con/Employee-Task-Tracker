using EmployeeTaskTracker.Models;

namespace EmployeeTaskTracker.Services
{
    public interface ITaskService
    {
        List<EmployeeTask> GetAllTasks();

        void CreateTask(EmployeeTask task);

        void AssignTask(
            int employeeId,
            int taskId);

        string UpdateTaskState(
            int taskId,
            TaskState status);

        List<EmployeeTask> GetTasksByEmployee(
            int employeeId);

        // NEW METHODS

        EmployeeTask? GetTaskById(int id);

        void UpdateTask(EmployeeTask task);

        void DeleteTask(int id);
    }
}