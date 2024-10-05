using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IHostEnvironment _environment; 

        public DepartmentController(
                                    IDepartmentService departmentService,
                                    IMapper mapper,
                                     ILogger<DepartmentController> logger,
                                     IWebHostEnvironment environment
            )
        {
            _departmentService = departmentService;
            _mapper = mapper;
            _logger = logger;
            _environment = environment;
        }
        #endregion

        #region Index 

            [HttpGet] // Get => /Department/Index
            public async Task <IActionResult> Index()
            {
                var departments =await _departmentService.GetAllDepartmentsAsync();
                return View(departments);
            }

        #endregion

        #region Details

        [HttpGet] // Get => /Department/Details
        public async Task <IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();


            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVm)
        {
            var message = "Department Is Not Created";
            if (ModelState.IsValid) // server side validation
            {
                try
                {



                    //var createdDepartment = new CreatedDepartmentDto()
                    //{

                    //    Code = departmentVm.Code,
                    //    Name = departmentVm.Name,
                    //    CreationDate = departmentVm.CreationDate,
                    //    Description = departmentVm.Description,
                    //};
                    var createdDepartment = _mapper.Map<DepartmentViewModel,CreatedDepartmentDto>(departmentVm);

                    var Created =await _departmentService.CreateDepartmentAsync(createdDepartment) > 0;
                    
                    if (Created)
        
                        TempData["Message"] = "Department is Created Successfuly";

                    
                    else
                        TempData["Message"] = "Department is not Created ";


                  return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // 1. log Exception
                    _logger.LogError(ex, ex.Message);

                    // 2. Set Message
                    message = _environment.IsDevelopment() ? ex.Message : "an error occured during create the department";

                }
                ModelState.AddModelError(string.Empty, message);
                return View(departmentVm);
            }


            ModelState.AddModelError(string.Empty, message);
            return View(departmentVm);
        }

        #endregion

        #region Update

        [HttpGet] // Get => /Department/Edit/id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest(); // 400 & i can redirect user to another page

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

            if (department == null)
                return NotFound(); // 404 
            var departmentVm = _mapper.Map<DepartmentDetailsDto , DepartmentViewModel>(department);
            return View(departmentVm);



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentViewModel departmentViewModel)
        {
            if (!ModelState.IsValid) // Server-Side Validation
                return View(departmentViewModel);

            var message = string.Empty;


            try
            {

                #region Manual Mapping 
                /// var updatedDepartment = new UpdatedDepartmentDto()
                ///{
                ///// Id = id,
                ///// Code = departmentViewModel.Code,
                ///// Name = departmentViewModel.Name,
                ///// CreationDate = departmentViewModel.CreationDate,
                ///// Description = departmentViewModel.Description,
                /////};

                #endregion

                var updatedDepartment = _mapper.Map<DepartmentViewModel, UpdatedDepartmentDto>(departmentViewModel);
                var updated = await _departmentService.UpdateDepartmentAsync(updatedDepartment) > 0;

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
        public async  Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department is null)
                return NotFound();

            return View(department);
        }


        [HttpPost] // Post => /Department/Delete/id
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted =  await _departmentService.DeleteDepartmentAsync(id);
                if (deleted)
                {
                    // Deletion successful, redirect to the Index page
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "An error has occurred during deleting the department.";
                }
            }
            catch (Exception ex)
            {
                // 1. Log the Exception
                _logger.LogError(ex, ex.Message);

                // 2. Set Message for Development/Production
                message = _environment.IsDevelopment() ? ex.Message : "An error has occurred while deleting the department.";
            }

            // If we reach this point, there was a failure, so return the view with the department and error message.
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null)
                return NotFound(); // Handle case where department is no longer found

            // Adding the error message to the ModelState
            ModelState.AddModelError(string.Empty, message);
            return View(department); // Return to the delete confirmation view with an error message
        }

        #endregion
    }
}