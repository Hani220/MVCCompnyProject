using Route.IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.BLL.Models.Employees
{
	public class CreatedEmployeeDto
	{
        [MaxLength(50 , ErrorMessage ="Max Lenghth of Name is 50 chars")]
        [MinLength(3, ErrorMessage = "Max Lenghth of Name is 5 chars")]
        public string Name { get; set; } = null!;

        [Range(22,30)]
        public int? Age { get; set; }

       
        public string? Address { get; set; }

       
        public decimal Salary { get; set; }

        
        public bool IsActive { get; set; }

        public string? Email { get; set; }

        [Display(Name="Phone Number")]
        [Phone] 
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }


    }
}
