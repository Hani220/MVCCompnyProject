using Microsoft.EntityFrameworkCore;
using Route.IKEA.DAL.Entities.Departments;
using Route.IKEA.DAL.Persistance.Data;
using Route.IKEA.DAL.Persistance.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.DAL.Persistance.Repositories.Departments
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }



}

