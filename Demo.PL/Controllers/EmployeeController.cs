using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository,  
                                  IDepartmentRepository departmentRepository,
                                  IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            var MappedEmployees = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employees);

            return View(MappedEmployees);
        }

        public IActionResult Create()
        {

            ViewBag.Departments = _departmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            

            if (ModelState.IsValid)
            {
                var MappedEmployee =_mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                int result = _employeeRepository.Add(MappedEmployee);
                if (result > 0)
                {
                    TempData["Message"] = "Employee Is Added";
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(employeeVM);
            }

        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee = _employeeRepository.GetById(id);
            if (employee is null)
                return NotFound();
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(ViewName, MappedEmployee);
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Departments = _departmentRepository.GetAll();

            return Details(id, "Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var MappedEmployee =_mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                _employeeRepository.Update(MappedEmployee);
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]

        public IActionResult Delete(EmployeeViewModel employeeVm, [FromRoute] int id)
        {
            if (id != employeeVm.Id)
                return BadRequest();
            try
            {
                var MappedEmployee = _mapper.Map<EmployeeViewModel,Employee> (employeeVm);
                _employeeRepository.Delete(MappedEmployee);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVm);
            }


        }


    }

}
