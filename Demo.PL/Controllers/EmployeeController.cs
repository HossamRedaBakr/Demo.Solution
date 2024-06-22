using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository  , IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            return View(employees);
        }

        public IActionResult Create()
        {

            ViewBag.Departments = _departmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            
            if (ModelState.IsValid)
            {
                int result = _employeeRepository.Add(employee);
                if (result > 0)
                {
                    TempData["Message"] = "Employee Is Added";
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(employee);
            }

        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee = _employeeRepository.GetById(id);
            if (employee is null)
                return NotFound();

            return View(ViewName, employee);
        }

        public IActionResult Edit(int? id)
        {
         
            return Details(id, "Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee, [FromRoute] int id)
        {
            if (id != employee.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                _employeeRepository.Update(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]

        public IActionResult Delete(Employee employee, [FromRoute] int id)
        {
            if (id != employee.Id)
                return BadRequest();
            try
            {
                _employeeRepository.Delete(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employee);
            }


        }


    }

}
