using Microsoft.EntityFrameworkCore;
using Route.IKEA.DAL.Persistance.Data;
using Route.IKEA.DAL.Persistance.Repositories.Departments;
using Route.IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.DAL.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly ApplicationDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository  => new EmployeeRepository(_dbContext) ;

        public IDepartmentRepository DepartmentRepository => new DepartmentRepository(_dbContext);

        public UnitOfWork(ApplicationDbContext dbContext) // Ask CLR For create an object from class 
                                                          //"ApplicationDbContext" Implicitly
        {
            _dbContext = dbContext;


        }


        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
           await _dbContext.DisposeAsync();
        }
    }
}
    

