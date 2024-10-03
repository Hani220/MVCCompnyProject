using Microsoft.EntityFrameworkCore;
using Route.IKEA.BLL.Models.Employees;
using Route.IKEA.DAL.Common.Enums;
using Route.IKEA.DAL.Entities.Employees;
using Route.IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)// ASK CLR FOR Create an Object From class implements IEmployeeRepository interface 
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {

            var employees =_employeeRepository.GetIQueryable()
                .Where(E=> !E.IsDeleted)
                .Include(E => E.Department)
                .Select(employee => new EmployeeDto()
                {
                        Id = employee.Id,
                        Name = employee.Name,
                        Age = employee.Age,
                        Salary = employee.Salary,
                        IsActive = employee.IsActive,
                        Email = employee.Email,
                        Gender = employee.Geneder.ToString(),
                        EmployeeType = employee.EmployeeType.ToString(),
                        DepartmentName = employee.Department.Name


                }).ToList();

            return employees;
           
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.Get(id);
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
                    Gender =employee.Geneder,
                    EmployeeType =employee.EmployeeType,
                   Department = employee.Department.Name

                };

            return null;
        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                Salary = employeeDto.Salary,
                IsActive = employeeDto.IsActive,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Geneder = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy =1,
                LastModifiedBy =1,
                LastModifiedOn =DateTime.UtcNow
            };

            return _employeeRepository.Add(employee);
            
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            // Retrieve the existing employee from the database
            var employee = _employeeRepository.Get(employeeDto.Id); 

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
            employee.Geneder = employeeDto.Gender;
            employee.DepartmentId = employeeDto.DepartmentId;
            employee.EmployeeType = employeeDto.EmployeeType;
            employee.LastModifiedBy = 1; 
            employee.LastModifiedOn = DateTime.UtcNow;

            // Mark the entity as updated
            return _employeeRepository.Update(employee); 
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.Get(id);
            if (employee is { })
                return _employeeRepository.Delete(employee) > 0;
            return false;
        }

       

      

        
    }
}
