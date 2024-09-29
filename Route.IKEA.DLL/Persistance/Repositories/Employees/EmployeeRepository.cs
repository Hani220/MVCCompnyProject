using Microsoft.EntityFrameworkCore;
using Route.IKEA.DAL.Entities.Department;
using Route.IKEA.DAL.Entities.Employee;
using Route.IKEA.DAL.Persistance.Data;
using Route.IKEA.DAL.Persistance.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.DAL.Persistance.Repositories.Employees
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext) // ask CLR For creating object from ApplicationDbContext [Implicitly]
        {
        }
    }

}
