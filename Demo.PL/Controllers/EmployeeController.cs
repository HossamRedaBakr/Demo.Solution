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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController( IUnitOfWork unitOfWork,  
                                  IDepartmentRepository departmentRepository,
                                  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public IActionResult Index(string SearchValue)
        {
            IEnumerable<Employee> employees;
            if(string.IsNullOrEmpty(SearchValue))
              employees = _unitOfWork.EmployeeRepository.GetAll();
                
            else
                employees = _unitOfWork.EmployeeRepository.GetEmplyeesByEmployeeName(SearchValue);
            
            var MappedEmployees = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employees);
            return View(MappedEmployees);
        }

        public IActionResult Create()
        {

            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            

            if (ModelState.IsValid)
            {
                var MappedEmployee =_mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Add(MappedEmployee);
                int result = _unitOfWork.Complete();
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
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is null)
                return NotFound();
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(ViewName, MappedEmployee);
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();

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
                _unitOfWork.EmployeeRepository.Update(MappedEmployee);
                _unitOfWork.Complete();
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
                _unitOfWork.EmployeeRepository.Delete(MappedEmployee);
                _unitOfWork.Complete();
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
