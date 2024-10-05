using Route.IKEA.DAL.Persistance.Repositories.Departments;
using Route.IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.DAL.Persistance.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IEmployeeRepository EmployeeRepository { get;  }

        public IDepartmentRepository DepartmentRepository { get; }

       public Task <int> CompleteAsync();

      


    }
}
