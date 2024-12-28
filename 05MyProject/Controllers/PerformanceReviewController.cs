using Microsoft.AspNetCore.Mvc;
using MyProject.Domain;
using MyProject.Models.DTOs.Conversions;
using MyProject.Models.DTOs;
using MyProject.Models;
using MyProject.Services.Absructions.Employees;
using MyProject.Services.Absructions.PerformanceReviews;

namespace MyProject.Controllers
{
	public class PerformanceReviewController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPerformanceService _performanceService;
        public PerformanceReviewController(IEmployeeService employeeService, IPerformanceService performanceService)
        {
            _employeeService = employeeService;
            _performanceService = performanceService;
        }
        public IActionResult Create()
        {
            var employees = _employeeService.GetAll();
            var performance = new PerformanceReviewViewModel()
			{
				Id = 0,
				EmployeeId = 0,
				ReviewNotes = string.Empty,
				ReviewScore = 0,
				ReviewDate = DateOnly.FromDateTime(DateTime.Now),
				Employees = (List<Employee>)employees
			};
			return View(performance);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PerformanceReviewDTO performanceReviewDTO)
        {

            if (ModelState.IsValid)
            {
                var performanceReview = PerformanceReviewConversion.ToEntity(performanceReviewDTO);
                if (performanceReview != null)
                    await _performanceService.Add(performanceReview);
				var performanceReviews = await _performanceService.GetAllWithAssociateEmployee();
                return RedirectToAction("List", new { page = 1 });
            }
            else
            {
				var employees = _employeeService.GetAll();
				var performance = new PerformanceReviewViewModel()
				{
					Id = performanceReviewDTO.Id,
					EmployeeId = performanceReviewDTO.EmployeeId,
					ReviewDate = performanceReviewDTO.ReviewDate,
					ReviewScore = performanceReviewDTO.ReviewScore,
					ReviewNotes = performanceReviewDTO.ReviewNotes,
					Employees = (List<Employee>)employees
				};
				return View(performance);
			}
            
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var performance = await _performanceService.GetPerformanceReviewWithAssociateEmployee(id);
            if (performance == null)
                return View("No Item Found!");
            var employees = _employeeService.GetAll();
            var allManagers = employees;
            ViewBag.SelectedEmployeeId = performance.EmployeeId;
            var performanceVM = new PerformanceReviewViewModel()
            {
                Id = performance.Id,
                EmployeeId = performance.EmployeeId,
                ReviewDate = performance.ReviewDate,
                ReviewScore = performance.ReviewScore,
                ReviewNotes = performance.ReviewNotes,
                Employees = (List<Employee>) employees
			};

			return View("Edit", performanceVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PerformanceReviewDTO performance)
        {
            if (ModelState.IsValid)
            {
                var _performanceReview = PerformanceReviewConversion.ToEntity(performance);
                _performanceService.Update(_performanceReview);
            }
            if (performance == null)
                return View("No Item Found!");
            var performances= await _performanceService.GetAllWithAssociateEmployee();
            return RedirectToAction("List", new { page = 1 });
        }
        public async Task<IActionResult> List(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var performances = await _performanceService.GetAllWithAssociateEmployee(pageSize, pageNumber);
            return View("List", performances);
        }
        public async Task<IActionResult> Details(int id)
        {
            var performanceReview = await _performanceService.GetPerformanceReviewWithAssociateEmployee(id);
            if (performanceReview == null)
                return View("No Item Found!");

            return View("View", performanceReview);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var performance = await _performanceService.GetPerformanceReviewWithAssociateEmployee(id);
            if (performance == null)
                return View("No Item Found!");
            _performanceService.Remove(performance);
            var performances = await _performanceService.GetAllWithAssociateEmployee();
            return RedirectToAction("List", new { page = 1 });
        }
    }
}
