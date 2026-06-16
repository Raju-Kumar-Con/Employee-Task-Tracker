using Microsoft.AspNetCore.Mvc;
using EmployeeTaskTracker.Services;
using EmployeeTaskTracker.Models;
using ClosedXML.Excel;

namespace EmployeeTaskTracker.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IEmployeeService _employeeService;


    public TaskController(
        ITaskService taskService,
        IEmployeeService employeeService)
        {
            _taskService = taskService;
            _employeeService = employeeService;
        }

        // ==========================================
        // TASK LIST WITH FILTER + PAGINATION
        // ==========================================
        public IActionResult Index(
            string statusFilter,
            string search,
            int page = 1,
            int pageSize = 5)
        {
            var tasks = _taskService.GetAllTasks()
                        ?? new List<EmployeeTask>();

            // Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                tasks = tasks.Where(t =>
                    t.Title.Contains(search,
                    StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Status Filter
            if (!string.IsNullOrWhiteSpace(statusFilter))
            {
                tasks = tasks.Where(t =>
                    t.Status.ToString()
                    .Equals(statusFilter,
                    StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            int totalRecords = tasks.Count;

            var pagedTasks = tasks
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.Search = search;
            ViewBag.StatusFilter = statusFilter;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages =
                (int)Math.Ceiling(
                    totalRecords / (double)pageSize);

            return View(pagedTasks);
        }

        // ==========================================
        // CREATE TASK
        // ==========================================
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeTask task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _taskService.CreateTask(task);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(task);
        }

        // ==========================================
        // EDIT TASK
        // ==========================================
        public IActionResult Edit(int id)
        {
            var task = _taskService.GetTaskById(id);

            if (task == null)
                return NotFound();

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeTask task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _taskService.UpdateTask(task);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(task);
        }

        // ==========================================
        // DELETE TASK
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                _taskService.DeleteTask(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // ==========================================
        // ASSIGN TASK PAGE
        // ==========================================
        public IActionResult Assign()
        {
            ViewBag.Employees =
                _employeeService.GetAll()
                ?? new List<Employee>();

            ViewBag.Tasks =
                _taskService.GetAllTasks()
                ?? new List<EmployeeTask>();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Assign(
            int employeeId,
            int taskId)
        {
            try
            {
                _taskService.AssignTask(
                    employeeId,
                    taskId);

                TempData["Success"] =
                    "Task Assigned Successfully";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // ==========================================
        // AJAX STATUS UPDATE
        // ==========================================
        [HttpPost]
        public JsonResult UpdateStatus(
            int taskId,
            TaskState status)
        {
            try
            {
                _taskService.UpdateTaskState(
                    taskId,
                    status);

                return Json(new
                {
                    success = true,
                    message = "Status Updated"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        // ==========================================
        // SWITCH STATEMENT REQUIREMENT
        // ==========================================
        public IActionResult StatusInfo(
            TaskState status)
        {
            string message;

            switch (status)
            {
                case TaskState.Pending:
                    message = "Task is Pending";
                    break;

                case TaskState.InProgress:
                    message = "Task is In Progress";
                    break;

                case TaskState.Completed:
                    message = "Task is Completed";
                    break;

                default:
                    message = "Unknown Status";
                    break;
            }

            return Content(message);
        }

        // ==========================================
        // ARRAY REQUIREMENT
        // ==========================================
        public IActionResult TaskStatuses()
        {
            string[] statuses =
            {
            "Pending",
            "In Progress",
            "Completed"
        };

            return Json(statuses);
        }

        // ==========================================
        // TYPE CONVERSION REQUIREMENT
        // ==========================================
        public IActionResult TypeConversionDemo()
        {
            string employeeId = "101";

            int convertedId =
                Convert.ToInt32(employeeId);

            return Content(
                $"Converted Id = {convertedId}");
        }

        // ==========================================
        // GET TASKS BY EMPLOYEE
        // ==========================================
        public IActionResult GetTasksByEmployee(
            int employeeId)
        {
            var tasks =
                _taskService.GetTasksByEmployee(
                    employeeId);

            return View(tasks);
        }

        // ==========================================
        // EXPORT TO EXCEL
        // ==========================================
        public IActionResult ExportExcel()
        {
            var tasks =
                _taskService.GetAllTasks()
                ?? new List<EmployeeTask>();

            using (var workbook =
                new XLWorkbook())
            {
                var worksheet =
                    workbook.Worksheets
                    .Add("Tasks");

                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Title";
                worksheet.Cell(1, 3).Value = "Description";
                worksheet.Cell(1, 4).Value = "Status";
                worksheet.Cell(1, 5).Value = "Deadline";
                worksheet.Cell(1, 6).Value = "EmployeeId";

                int row = 2;

                foreach (var task in tasks)
                {
                    worksheet.Cell(row, 1).Value = task.Id;
                    worksheet.Cell(row, 2).Value = task.Title;
                    worksheet.Cell(row, 3).Value = task.Description;
                    worksheet.Cell(row, 4).Value = task.Status.ToString();
                    worksheet.Cell(row, 5).Value = task.Deadline;
                    worksheet.Cell(row, 6).Value = task.EmployeeId;

                    row++;
                }

                using (var stream =
                    new MemoryStream())
                {
                    workbook.SaveAs(stream);

                    return File(
                        stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Tasks.xlsx");
                }
            }
        }
    }

}
