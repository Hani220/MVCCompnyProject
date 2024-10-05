 using Microsoft.EntityFrameworkCore;
using Route.IKEA.BLL.Common.Services.Attachments;
using Route.IKEA.BLL.Models.Employees;
using Route.IKEA.DAL.Common.Enums;
using Route.IKEA.DAL.Entities.Employees;
using Route.IKEA.DAL.Persistance.Repositories.Employees;
using Route.IKEA.DAL.Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;

        public EmployeeService(
            IUnitOfWork unitOfWork,  // ASK CLR FOR Create an Object From class implements "IUnitOfWork" interface 
            IAttachmentService attachmentService
            )
           
        {
            _unitOfWork = unitOfWork;
            _attachmentService = attachmentService;
        }
        public async Task <IEnumerable<EmployeeDto>> GetEmployeesAsync(string search)
        {
            var employees = await  _unitOfWork.EmployeeRepository
                .GetIQueryable()
                .Where(E => !E.IsDeleted && (string.IsNullOrEmpty(search) || E.Name.ToLower().Contains(search.ToLower())))
                .Include(E => E.Department)
                .Select(employee => new EmployeeDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Salary = employee.Salary,
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    Gender = employee.Gender.ToString(),
                    EmployeeType = employee.EmployeeType.ToString(),
                    DepartmentName = employee.Department.Name
                })
                .ToListAsync();

            return  employees;
        }

        public async Task <EmployeeDetailsDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id);
            if (employee is { })
                return new EmployeeDetailsDto()
                {

                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    Salary = employee.Salary,
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender =employee.Gender,
                    EmployeeType =employee.EmployeeType,
                   Department = employee.Department.Name,
                   Image = employee.Image

                };

            return null;
        }

        public async Task  <int> CreateEmployeeAsync(CreatedEmployeeDto employeeDto)
        {
           

            var employee = new Employee()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                Salary = employeeDto.Salary,
                DepartmentId = employeeDto.DepartmentId,
               

                CreatedOn = DateTime.UtcNow,

                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

            if(employeeDto.Image is not null ) 
                employee.Image = await _attachmentService.UploadFileAsync(employeeDto.Image, "images");


            _unitOfWork.EmployeeRepository.Add(employee);
            return await  _unitOfWork.CompleteAsync();

        }

        public async Task <int> UpdateEmployeeAsync(UpdatedEmployeeDto employeeDto)
        {
            // Retrieve the existing employee from the database
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(employeeDto.Id); 

            if (employee == null)
            {
                // Employee does not exist, handle this case as needed
                return 0;
            }

            // Update the employee's properties
            employee.Name = employeeDto.Name;
            employee.Age = employeeDto.Age;
            employee.Address = employeeDto.Address;
            employee.Salary = employeeDto.Salary;
            employee.IsActive = employeeDto.IsActive;
            employee.Email = employeeDto.Email;
            employee.PhoneNumber = employeeDto.PhoneNumber;
            employee.HiringDate = employeeDto.HiringDate; 
            employee.Gender = employeeDto.Gender;
            employee.DepartmentId = employeeDto.DepartmentId;
            employee.EmployeeType = employeeDto.EmployeeType;
            employee.LastModifiedBy = 1; 
            employee.LastModifiedOn = DateTime.UtcNow;

            // Mark the entity as updated
             _unitOfWork.EmployeeRepository.Update(employee);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task <bool> DeleteEmployeeAsync(int id)
        {
            var employeeRepo =  _unitOfWork.EmployeeRepository;

            var employee = await employeeRepo.GetAsync(id);
            if (employee is { })
                 employeeRepo.Delete(employee);

            return await _unitOfWork.CompleteAsync()>0;
        }

       

      

        
    }
}
