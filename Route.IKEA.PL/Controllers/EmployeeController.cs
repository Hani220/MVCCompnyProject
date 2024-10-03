using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Route.IKEA.BLL.Models.Departments;
using Route.IKEA.BLL.Models.Employees;
using Route.IKEA.BLL.Services.Departments;
using Route.IKEA.BLL.Services.Employees;
using Route.IKEA.PL.ViewModels.Department;
using Route.IKEA.PL.ViewModels.Employee;

namespace Route.IKEA.PL.Controllers
{
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
        public IActionResult Index()
        {
            var employee = _employeeService.GetAllEmployees();

            return View(employee);
        }
        #endregion

        #region Details
        [HttpGet] // Get : /Employee/Details
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null)
                return NotFound();
            return View(employee);
        }
        #endregion

        #region Create  
        [HttpGet] // Get => /Employee/Create
        public IActionResult Create()
        {
           
         
            //var Employee = _EmployeeRepository.Add(Employee dep);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatedEmployeeDto employee)
        {
            var message = "Employee Is Not Created";
            if (ModelState.IsValid) // server side validation
            {
                try
                {
                    var count = _employeeService.CreateEmployee(employee);
                    if (count > 0)
                        return RedirectToAction("Index");
                    else
                        ModelState.AddModelError(string.Empty, message);
                }
                catch (Exception ex)
                {
                    // 1. log Exception
                    _logger.LogError(ex, ex.Message);

                    // 2. Set Message
                    message = _environment.IsDevelopment() ? ex.Message : "Employee Is Not Created";

                }
                ModelState.AddModelError(string.Empty, message);
                return View(employee);
            }


            ModelState.AddModelError(string.Empty, message);
            return View(employee);
        }



        #endregion

        #region Edit 

        [HttpGet] // Get => /Employee/Edit
        public IActionResult Edit(int? id)
        {

            if (id == null)
                return BadRequest(); // 400 & i can redirect user to another page

            var employee = _employeeService.GetEmployeeById(id.Value);

      

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
        public IActionResult Edit([FromRoute] int id, UpdatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            var message = string.Empty;

            try
            {
                // Set the Id from the route directly to the employee DTO
                employee.Id = id;

                // Call service to update the employee
                var updated = _employeeService.UpdateEmployee(employee) > 0;

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
        public IActionResult Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = _employeeService.DeleteEmployee(id);
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

