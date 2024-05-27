using Demo.BLL.Interfaces;
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
    }
}
