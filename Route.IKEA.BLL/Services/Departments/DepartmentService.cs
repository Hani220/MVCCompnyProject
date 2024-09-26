using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Route.IKEA.BLL.Models.Departments;
using Route.IKEA.DAL.Entities.Department;
using Route.IKEA.DAL.Persistance.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.BLL.Services.Departments
{
	
	public class DepartmentService : IDepartmentService
	{
		private readonly DepartmentRepository _departmentRepository;

        public DepartmentService(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        


        public IEnumerable<DepartmentDto> GetAllDepartments()
		{
			var departments = _departmentRepository.GetAllAsIQueryable().Select(department => new DepartmentDto
			{
				Code = department.Code,
				Name = department.Name,
				CreationDate = department.CreationDate,
				Id = department.Id
			}).AsNoTracking().ToList();
			
			return departments;
		}

		public DepartmentDetailsDto? GetDepartmentById(int id)
		{
			var department = _departmentRepository.Get(id);

			if (department is not null)
				return new DepartmentDetailsDto()
				{
					Id = department.Id,
					Code = department.Code,
					Name = department.Name,
					CreationDate = department.CreationDate,
					CreatedBy = department.CreatedBy,
					CreatedOn = department.CreatedOn,
					LastModifiedBy = department.LastModifiedBy,
					LastModifiedOn = department.LastModifiedOn,
				};
			return null;

		}

		public int CreateDepartment(CreatedDepartmentDto departmentDto)
		{
			var department = new Department()
			{
				Code = departmentDto.Code,
				Name = departmentDto.Name,
				Description = departmentDto.Description,
				CreationDate = departmentDto.CreationDate,
				CreatedBy = 1,
				CreatedOn = DateTime.UtcNow,
				LastModifiedBy = 1,
				LastModifiedOn = DateTime.UtcNow
			};
			return	_departmentRepository.Add(department);
		}


		public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
		{
			var department = new Department()
			{
				Id = departmentDto.Id,
				Code = departmentDto.Code,
				Name = departmentDto.Name,
				Description = departmentDto.Description,
				CreationDate = departmentDto.CreationDate,
				LastModifiedBy = 1,
				LastModifiedOn = DateTime.UtcNow
			};
			return _departmentRepository.Update(department);
		}

		public bool DeleteDepartment(int id)
		{

			var department = _departmentRepository.Get(id);
			if (department is { }) 
				return _departmentRepository.Delete(department) > 0;
			return false;

		}
		
			
		


		
	}
}
