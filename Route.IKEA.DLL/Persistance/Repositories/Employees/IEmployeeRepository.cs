using Route.IKEA.DAL.Entities.Department;
using Route.IKEA.DAL.Entities.Employee;
using Route.IKEA.DAL.Persistance.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.DAL.Persistance.Repositories.Employees
{
    public interface IEmployeeRepository :IGenericRepository<Employee>
    {
       
    }
}
