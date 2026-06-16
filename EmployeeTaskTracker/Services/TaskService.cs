using EmployeeTaskTracker.Models;
using EmployeeTaskTracker.Repositories;

namespace EmployeeTaskTracker.Services
{
    public class TaskService : ITaskService
    {
        public void CreateTask(EmployeeTask task)
        {
            try
            {
                task.Id = MemoryRepository.Tasks.Count + 1;
                MemoryRepository.Tasks.Add(task);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating task", ex);
            }
        }

        public List<EmployeeTask> GetAllTasks()
        {
            return MemoryRepository.Tasks;
        }

        public void AssignTask(int employeeId, int taskId)
        {
            try
            {
                var task = MemoryRepository.Tasks
                    .FirstOrDefault(x => x.Id == taskId);

                if (task == null)
                    throw new Exception("Task not found");

                if (!MemoryRepository.EmployeeTaskMap.ContainsKey(employeeId))
                {
                    MemoryRepository.EmployeeTaskMap[employeeId]
                        = new List<EmployeeTask>();
                }

                MemoryRepository.EmployeeTaskMap[employeeId]
                    .Add(task);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while assigning task", ex);
            }
        }

        public List<EmployeeTask> GetTasksByEmployee(int employeeId)
        {
            try
            {
                return MemoryRepository.EmployeeTaskMap.ContainsKey(employeeId)
                    ? MemoryRepository.EmployeeTaskMap[employeeId]
                    : new List<EmployeeTask>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching employee tasks", ex);
            }
        }

        public string UpdateTaskState(int taskId, TaskState status)
        {
            try
            {
                var task = MemoryRepository.Tasks
                    .FirstOrDefault(x => x.Id == taskId);

                if (task == null)
                    throw new Exception("Task not found");

                switch (status)
                {
                    case TaskState.Pending:
                        task.Status = TaskState.Pending;
                        break;

                    case TaskState.InProgress:
                        task.Status = TaskState.InProgress;
                        break;

                    case TaskState.Completed:
                        task.Status = TaskState.Completed;
                        break;

                    default:
                        task.Status = TaskState.Pending;
                        break;
                }

                string[] messages =
                {
                    "Pending",
                    "In Progress",
                    "Completed"
                };

                int index = (int)task.Status;

                string currentStatus = messages[index];

                return currentStatus;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating task state", ex);
            }
        }

        public EmployeeTask? GetTaskById(int id)
        {
            return MemoryRepository.Tasks
                .FirstOrDefault(x => x.Id == id);
        }

        public void UpdateTask(EmployeeTask task)
        {
            try
            {
                var existingTask = MemoryRepository.Tasks
                    .FirstOrDefault(x => x.Id == task.Id);

                if (existingTask == null)
                    throw new Exception("Task not found");

                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.Status = task.Status;
                existingTask.Deadline = task.Deadline;
                existingTask.EmployeeId = task.EmployeeId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating task", ex);
            }
        }

        public void DeleteTask(int id)
        {
            try
            {
                var task = MemoryRepository.Tasks
                    .FirstOrDefault(x => x.Id == id);

                if (task == null)
                    throw new Exception("Task not found");

                MemoryRepository.Tasks.Remove(task);

                foreach (var employeeTasks in MemoryRepository.EmployeeTaskMap.Values)
                {
                    employeeTasks.RemoveAll(t => t.Id == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting task", ex);
            }
        }

        public string GetStatusText(TaskState status)
        {
            switch (status)
            {
                case TaskState.Pending:
                    return "Pending";

                case TaskState.InProgress:
                    return "In Progress";

                case TaskState.Completed:
                    return "Completed";

                default:
                    return "Unknown";
            }
        }

        public string[] GetTaskPriorities()
        {
            string[] priorities = new string[3];

            priorities[0] = "Low";
            priorities[1] = "Medium";
            priorities[2] = "High";

            return priorities;
        }
    }
}