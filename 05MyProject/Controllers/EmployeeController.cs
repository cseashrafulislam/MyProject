using Microsoft.AspNetCore.Mvc;
using MyProject.Domain;
using MyProject.Models;
using MyProject.Models.DTOs;
using MyProject.Models.DTOs.Conversions;
using MyProject.Services.Absructions.Departments;
using MyProject.Services.Absructions.Employees;

namespace MyProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = _departmentService.GetAll();
            var employeeDto = new EmployeeRecordDTO
            (0, string.Empty, string.Empty, string.Empty, 0, departments.ToList(), string.Empty, DateTime.Now.Date, 0, string.Empty, false, true);

            return View(employeeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDTO employee)
        {
            if (ModelState.IsValid)
            {
                var _employee = EmployeeConversion.ToEntity(employee);
                await _employeeService.Add(_employee);
                var employees = await _employeeService.GetAllWithAssociateDepartment();
                return RedirectToAction("List", new { page = 1 });
            }

            var departments = _departmentService.GetAll();
            var employeeDto = new EmployeeRecordDTO
           (employee.Id,
           employee.Name,
           employee.Email,
           employee.PhoneNumber,
           employee.DepartmentId,
           (List<Department>)departments,
           employee.Position,
           employee.JoiningDate,
           employee.Salary,
           employee.Gender,
           false,
           true);
            return View(employeeDto);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetEmployeeWithAssociateDepartment(id);
            if (employee == null)
                return View("No Item Found!");
            var departments = _departmentService.GetAll();
            var AllDepartments = departments;
            ViewBag.SelectedDepartmentId = employee.DepartmentId;
            var employeeVm = new EmployeeViewModel()
            {
                Name = employee.Name,
                Gender = employee.Gender,
                JoiningDate = employee.JoiningDate,
                DepartmentId = employee.DepartmentId,
                Departments = (List<Department>)AllDepartments,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Position = employee.Position,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
            };
            return View("Edit", employeeVm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {
                var _employee = EmployeeConversion.ToEntity(employeeDTO);
                _employeeService.Update(_employee);
            }
            if (employeeDTO == null)
                return View("No Item Found!");
            var employees = await _employeeService.GetAllWithAssociateDepartment();
            return RedirectToAction("List", new { page = 1 });
        }
        public async Task<IActionResult> List(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var employees = await _employeeService.GetAllWithAssociateDepartment(pageSize, pageNumber);
            return View("List", employees);
        }
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeService.GetEmployeeWithAssociateDepartment(id);
            if (employee == null)
                return View("No Item Found!");

            return View("View", employee);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeWithAssociateDepartment(id);
            if (employee == null)
                return View("No Item Found!");
            _employeeService.Remove(employee);
            var employees = await _employeeService.GetAllWithAssociateDepartment();
            return RedirectToAction("List", new { page = 1 });
        }
        public async Task<IActionResult> SearchEmployee(SearchCriteriaDTO searchCriteria)
        {
            var employees = await _employeeService.GetEmployeeBySearchingCriteria(searchCriteria.pageNumber, searchCriteria.pageSize, searchCriteria.searchName, searchCriteria.searchDepartment, searchCriteria.searchPosition, searchCriteria.reviewScore);
            return View("SearchEmployee", employees);
        }
    }
}
