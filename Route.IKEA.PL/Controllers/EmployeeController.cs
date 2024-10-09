using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Route.IKEA.BLL.Models.Employees;
using Route.IKEA.BLL.Services.Departments;
using Route.IKEA.BLL.Services.Employees;
using Route.IKEA.PL.ViewModels.Employee;

namespace Route.IKEA.PL.Controllers
{
	[Authorize]
	public class EmployeeController : Controller
    {
        #region Services 
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(
            IEmployeeService employeeService,
            
            ILogger<EmployeeController> logger,
            IWebHostEnvironment environment)
        {
            _employeeService = employeeService;
            _logger = logger;
            _environment = environment;
        }
        #endregion

        #region Index
        [HttpGet] // Get : /Employee/Index
        public async Task <IActionResult> Index(string search)
        {
            var employees = await _employeeService.GetEmployeesAsync(search);
           
            //if(!string.IsNullOrEmpty(search))
            //    return PartialView("Partials/_EmployeeListPartial", employee);
            return View(employees);
        }

        #endregion

        #region Details
        [HttpGet] // Get : /Employee/Details
        public async Task  <IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee =await  _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employee is null)
                return NotFound();
            return View(employee);
        }
        #endregion

        #region Create  
        [HttpGet] // Get => /Employee/Create
        public IActionResult Create([FromServices] IDepartmentService departmentService)
        {
            //var Employee = _EmployeeRepository.Add(Employee dep);
            ViewData["Departments"] = departmentService.GetAllDepartmentsAsync();
            return View();
        }



        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task <IActionResult> Create(CreatedEmployeeDto employee)
        {
            var message = "Employee Is Not Created";
            if (ModelState.IsValid) // server side validation
            {
                try
                {
                    var count = await _employeeService.CreateEmployeeAsync(employee);
                    if (count > 0)
                        TempData["message"] = "Employee Is created";

                    else
                        TempData["message"] = "Employee Is Not created";
                    //message = "Employee is not created.";
                }
                catch (Exception ex)
                {
                    // 1. log Exception
                    _logger.LogError(ex, ex.Message);

                    // 2. Set Message
                    message = _environment.IsDevelopment() ? ex.Message : "Employee Is Not Created";

                }

                ModelState.AddModelError(string.Empty, message);
                return RedirectToAction(nameof(Index));
                //return View(employee);
            }


            ModelState.AddModelError(string.Empty, message);
            return View(employee);
        }


        #endregion

        #region Edit 

        [HttpGet] // Get => /Employee/Edit
        public async Task <IActionResult> Edit(int? id)
        {

            if (id == null)
                return BadRequest(); // 400 & i can redirect user to another page

            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);

      

            if (employee == null)
                return NotFound(); // 404 
           
           
            return View(new EmployeeEditViewModel()
            {
                Name = employee.Name,
                Age = employee.Age,
                Email = employee.Email,
                Address = employee.Address,
                EmployeeType = employee.EmployeeType, 
                Salary = employee.Salary,
                Gender = employee.Gender,
                HiringDate = employee.HiringDate, 
                IsActive = employee.IsActive,
                PhoneNumber = employee.PhoneNumber
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, UpdatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            var message = string.Empty;

            try
            {
                // Set the Id from the route directly to the employee DTO
                employee.Id = id;

                // Call service to update the employee
                var updated = await _employeeService.UpdateEmployeeAsync(employee) > 0;

                if (updated)
                    return RedirectToAction("Index");

                message = "An error has occurred while updating the employee.";
            }
            catch (Exception ex)
            {
                // Log Exception
                _logger.LogError(ex, ex.Message);

                // Set Message
                message = _environment.IsDevelopment() ? ex.Message : "An error has occurred while updating the employee.";
            }

            // Add error message to the ModelState
            ModelState.AddModelError(string.Empty, message);
            return View(employee);
        }


        #endregion

        #region Delete  
        
        [HttpPost] // POST: /Employee/Delete/id
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = await _employeeService.DeleteEmployeeAsync(id);
                if (deleted) // Ensure delete logic returns success
                    return RedirectToAction(nameof(Index)); // Redirect on success
                message = "An error occurred during deleting the Employee";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "An error occurred during deleting the Employee";
            }

            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index)); // Redirect to Index, handle failure there if needed
        }

    }


    #endregion



}

