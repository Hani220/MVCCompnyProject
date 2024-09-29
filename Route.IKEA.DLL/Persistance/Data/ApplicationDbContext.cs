﻿using Microsoft.EntityFrameworkCore;
using Route.IKEA.DAL.Entities.Department;
using Route.IKEA.DAL.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.DAL.Persistance.Data
{
	public class ApplicationDbContext :DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

		public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
