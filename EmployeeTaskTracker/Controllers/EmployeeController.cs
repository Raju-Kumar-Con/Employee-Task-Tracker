using Microsoft.AspNetCore.Mvc;
using EmployeeTaskTracker.Services;
using EmployeeTaskTracker.Models;

namespace EmployeeTaskTracker.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        public IActionResult Index(string search, int page = 1)
        {
            int pageSize = 5;

            var data = string.IsNullOrEmpty(search)
                ? _service.GetAll()
                : _service.Search(search);

            var paged = data
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.TotalPages = (int)Math.Ceiling(data.Count / (double)pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.Search = search;

            return View(paged);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.Add(emp);
                    return RedirectToAction("Index");
                }
                return View(emp);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(emp);
            }
        }

        public IActionResult Edit(int id)
        {
            var emp = _service.GetById(id);
            return View(emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            _service.Update(emp);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}