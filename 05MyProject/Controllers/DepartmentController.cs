using Microsoft.AspNetCore.Mvc;
using MyProject.Models.DTOs.Conversions;
using MyProject.Models.DTOs;
using MyProject.Services.Absructions.Employees;
using MyProject.Services.Absructions.Departments;
using MyProject.Models;
using MyProject.Domain;

namespace MyProject.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }
        public IActionResult Create()
        {
            var employees = _employeeService.GetAll();
            var department = new DepartmentViewModel()
            {
                Managers = (List<Employee>)employees
            };
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentDTO departmentDTO)
        {

            if (ModelState.IsValid)
            {
                try
                {
					var _department = DepartmentConversion.ToEntity(departmentDTO);
					if (_department != null)
						await _departmentService.Add(_department);
					var departments = await _departmentService.GetAllWithAssociateManager();
                    return RedirectToAction("List", new { page = 1 });
                }
                catch (Exception ex)
                {

					TempData["ErrorMessage"] = ex.Message;
					return RedirectToAction("Create");
				}
                
            }
            else
            {
                var managers = _employeeService.GetAll();
                var employeeDto = new DepartmentViewModel()
                {
                    Id = departmentDTO.Id,
                    Budget = departmentDTO.Budget,
                    Name = departmentDTO.Name,
                    Managers = (List<Employee>)managers
                };
                return View(employeeDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _departmentService.GetDepartmentWithAssociateManager(id);
            if (department == null)
                return View("No Item Found!");
            var managers =  _employeeService.GetAll();
            var allManagers = managers;
            ViewBag.SelectedManagerId = department.ManagerId;
            var departmentVM = new DepartmentViewModel()
            {
                Name = department.Name,
                Budget = department.Budget,
                ManagerId = department.ManagerId,
                Managers = (List<Employee>)allManagers
            };
            return View("Edit", departmentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentDTO departmentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _department = DepartmentConversion.ToEntity(departmentDto);
                    await _departmentService.Update(_department);
                }
                if (departmentDto == null)
                    return View("No Item Found!");
                var departments = await _departmentService.GetAllWithAssociateManager();
                return RedirectToAction("List", new { page = 1 });
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Create");
            }
            
        }
        public async Task<IActionResult> List(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var departments = await _departmentService.GetAllWithAssociateManager(pageSize, pageNumber);
            return View("List", departments);
        }
        public async Task<IActionResult> Details(int id)
        {
            var department = await _departmentService.GetDepartmentWithAssociateManager(id);
            if (department == null)
                return View("No Item Found!");

            return View("View", department);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _departmentService.GetDepartmentWithAssociateManager(id);
            if (department == null)
                return View("No Item Found!");
            _departmentService.Remove(department);
            var departments = await _departmentService.GetAllWithAssociateManager();
            return RedirectToAction("List", new { page = 1 });
        }
    }
}
