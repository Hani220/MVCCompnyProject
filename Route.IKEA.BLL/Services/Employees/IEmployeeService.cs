using Route.IKEA.BLL.Models.Departments;
using Route.IKEA.BLL.Models.Employees;
using Route.IKEA.DAL.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        Task <IEnumerable<EmployeeDto>> GetEmployeesAsync(string search);

        Task <EmployeeDetailsDto?> GetEmployeeByIdAsync(int id);

        Task <int> CreateEmployeeAsync(CreatedEmployeeDto employeeDto);

       Task <int> UpdateEmployeeAsync(UpdatedEmployeeDto employeeDto);

       Task  <bool> DeleteEmployeeAsync(int id);
    }
}
