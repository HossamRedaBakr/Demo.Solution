using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository) //ASk CLR To Creating Object From Class DepartmentRepo
        {
            _departmentRepository = departmentRepository;
        }

        public IActionResult Index()
        {

            var departments = _departmentRepository.GetAll();
            return View(departments);
        }
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid)
            {
            _departmentRepository.Add(department);
            return RedirectToAction(nameof(Index));
            }
            else 
            {
                return View(department);
            }

        }
    }
}
