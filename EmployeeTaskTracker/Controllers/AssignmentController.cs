using Microsoft.AspNetCore.Mvc;
using EmployeeTaskTracker.Services;

namespace EmployeeTaskTracker.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IEmployeeService _employeeService;

        public AssignmentController(
            ITaskService taskService,
            IEmployeeService employeeService)
        {
            _taskService = taskService;
            _employeeService = employeeService;
        }

        public IActionResult Assign()
        {
            ViewBag.Employees = _employeeService.GetAll();
            ViewBag.Tasks = _taskService.GetAllTasks();
            return View();
        }

        [HttpPost]
        public IActionResult Assign(int employeeId, int taskId)
        {
            try
            {
                _taskService.AssignTask(employeeId, taskId);
                return RedirectToAction("Assign");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public IActionResult EmployeeTasks(int id)
        {
            var tasks = _taskService.GetTasksByEmployee(id);
            return View(tasks);
        }
    }
}