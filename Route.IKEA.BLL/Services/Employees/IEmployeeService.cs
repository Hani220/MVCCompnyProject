using Route.IKEA.BLL.Models.Departments;
using Route.IKEA.BLL.Models.Employees;
using Route.IKEA.DAL.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees();

        EmployeeDetailsDto? GetEmployeeById(int id);

        int CreateEmployee(CreatedEmployeeDto employeeDto);

        int UpdateEmployee(UpdatedEmployeeDto employeeDto);

        bool DeleteEmployee(int id);
    }
}
