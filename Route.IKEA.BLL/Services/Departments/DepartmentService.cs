using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Route.IKEA.BLL.Models.Departments;
using Route.IKEA.DAL.Entities.Departments;
using Route.IKEA.DAL.Persistance.Repositories.Departments;
using Route.IKEA.DAL.Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.BLL.Services.Departments
{
	
	public class DepartmentService : IDepartmentService
	{
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }




        public async Task <IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
			var departmentRepo = _unitOfWork.DepartmentRepository;
				var departments = await  departmentRepo
				.GetIQueryable()
                .Where(D => !D.IsDeleted) // Exclude soft-deleted records
                .Select(department => new DepartmentDto
                {
                    Code = department.Code,
                    Name = department.Name,
                    CreationDate = department.CreationDate,
                    Id = department.Id
                })
                .AsNoTracking()
                .ToListAsync();

            return   departments;
        }

        public async Task <DepartmentDetailsDto?> GetDepartmentByIdAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id);

			// Check if the department is soft-deleted and exclude it
			if (department is { })
            {
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
            }

            return null;
        }


        public async Task <int> CreateDepartmentAsync(CreatedDepartmentDto departmentDto)
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
			_unitOfWork.DepartmentRepository.Add(department);
			return await _unitOfWork.CompleteAsync();
		}


		public async Task<int> UpdateDepartmentAsync(UpdatedDepartmentDto departmentDto)
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
			 _unitOfWork.DepartmentRepository.Update(department);
			return await _unitOfWork.CompleteAsync();
		}

		public async Task <bool> DeleteDepartmentAsync(int id)
		{
			var departmentRepo =  _unitOfWork.DepartmentRepository;


            var department = await departmentRepo.GetAsync(id);
			if (department is { }) 
				 departmentRepo.Delete(department) ;

			return await _unitOfWork.CompleteAsync()>0 ;

			

		}
		
			
		


		
	}
}
