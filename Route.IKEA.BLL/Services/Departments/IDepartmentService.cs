﻿using Route.IKEA.BLL.Models.Departments;
using Route.IKEA.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.BLL.Services.Departments
{
	public interface IDepartmentService
	{
		Task <IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();

		Task <DepartmentDetailsDto?> GetDepartmentByIdAsync(int id);

		Task <int> CreateDepartmentAsync (CreatedDepartmentDto departmentDto);

		Task <int> UpdateDepartmentAsync (UpdatedDepartmentDto departmentDto);

		Task <bool> DeleteDepartmentAsync (int id);
	}
}
