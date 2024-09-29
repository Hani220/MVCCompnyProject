using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Route.IKEA.BLL.Models.Departments;
using Route.IKEA.BLL.Services.Departments;
using Route.IKEA.PL.ViewModels.Department;

namespace Route.IKEA.PL.Controllers
{
    // Inheritance => DepartmentController is    a Controller    
    // Composition => DepartmentController has   a IDepartmentService
    public class DepartmentController : Controller
    {
        #region Services

        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService,
                                     ILogger<DepartmentController> logger,
                                     IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
        }
        #endregion

        #region Index 

            [HttpGet] // Get => /Department/Index
            public IActionResult Index()
            {
                var departments = _departmentService.GetAllDepartments();
                return View(departments);
            }

        #endregion

        #region Details

        [HttpGet] // Get => /Department/Details
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();


            var department = _departmentService.GetDepartmentById(id.Value);

            if (department == null)
                return NotFound();

            return View(department);

        }
        #endregion

        #region Create

        [HttpGet] // Get => /Department/Create
        public IActionResult Create()
        {
            //var department = _departmentRepository.Add(Department dep);
            return View();
        }


        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto department)
        {
            var message = "Department Is Not Created";
            if (ModelState.IsValid) // server side validation
            {
                try
                {
                    var count = _departmentService.CreateDepartment(department);
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
                    message = _environment.IsDevelopment() ? ex.Message : "an error occured during create the department";

                }
                ModelState.AddModelError(string.Empty, message);
                return View(department);
            }


            ModelState.AddModelError(string.Empty, message);
            return View(department);
        }

        #endregion

        #region Update

        [HttpGet] // Get => /Department/Edit/id
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest(); // 400 & i can redirect user to another page

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department == null)
                return NotFound(); // 404 

            return View(new DepartmentEditViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                CreationDate = department.CreationDate,
                Description = department.Description,
            });

        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, DepartmentEditViewModel departmentViewModel)
        {
            if (!ModelState.IsValid) // Server-Side Validation
                return View(departmentViewModel);

            var message = string.Empty;


            try
            {
                var updatedDepartment = new UpdatedDepartmentDto()
                {
                    Id = id,
                    Code = departmentViewModel.Code,
                    Name = departmentViewModel.Name,
                    CreationDate = departmentViewModel.CreationDate,
                    Description = departmentViewModel.Description,
                };

                var updated = _departmentService.UpdateDepartment(updatedDepartment) > 0;

                if (updated)
                    return RedirectToAction("Index");

                message = "An Error Has Occurred During Update Department";


            }
            catch (Exception ex)
            {

                // 1. log Exception
                _logger.LogError(ex, ex.Message);

                // 2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "An Error Has Occurred During Update Department";

            }

            ModelState.AddModelError(string.Empty, message);
            return View(departmentViewModel);

        }  

        #endregion

        #region Delete

        [HttpGet] // Get => /Department/Delete/id
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
                return NotFound();

            return View(department);
        }


        [HttpPost] // Post => /Department/Delete/id

        public IActionResult Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = _departmentService.DeleteDepartment(id);
                if (deleted)
                    return RedirectToAction(nameof(Index));
                message = "an error has occured during deleting the department";
            }
            catch (Exception ex)
            {
                //1. Log Exception
                _logger.LogError(ex, ex.Message);

                //2.Set Message
                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during deleting the department";
            }
            // ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));

        } 
        #endregion
    }
}