using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Route.IKEA.BLL.Models.Departments;
using Route.IKEA.PL.ViewModels.Department;

namespace Route.IKEA.PL.Mapping
{
    public class MappingProfile :   Profile 
    {
        public MappingProfile()
        {
            #region Employee

            #endregion

            #region Department



            CreateMap<DepartmentDetailsDto, DepartmentViewModel>()
                /* .ForMember(dest => dest.Name ,config => config.MapFrom(src => src.Name))*/

               // .ReverseMap()
                /* .ForMember(dest => dest.Name ,config => config.MapFrom(src => src.Name))*/
                ;

            CreateMap<DepartmentViewModel, UpdatedDepartmentDto>();

            CreateMap<DepartmentViewModel, CreatedDepartmentDto>();

            #endregion
        }
    }
}
