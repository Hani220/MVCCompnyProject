using Microsoft.EntityFrameworkCore;
using Route.IKEA.DAL.Entities.Department;
using Route.IKEA.DAL.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.DAL.Persistance.Repositories.Departments
{
	public class DepartmentRepository : IDepartmentRepository
	{
		private readonly ApplicationDbContext _dbContext;

		public DepartmentRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IEnumerable<Department> GetAll(bool withAsNoTracking = true)
		{
			if(!withAsNoTracking) 
				return _dbContext.Departments.AsNoTracking().ToList();

			return _dbContext.Departments.ToList();
		}

		public IQueryable<Department> GetAllAsIQueryable()
		{
			return _dbContext.Departments;
		}

		public Department? Get(int id)
		{
			var department = _dbContext.Departments.Find(id);
			return department;
		}

		public int Add(Department entity)
		{
			_dbContext.Departments.Add(entity);
			return _dbContext.SaveChanges();
		}


		public int Update(Department entity)
		{
			_dbContext.Departments.Update(entity);
			return _dbContext.SaveChanges();
		}
		public int Delete(Department entity)
		{
			_dbContext.Departments.Remove(entity);
			return _dbContext.SaveChanges();
		}

		
	}
}
