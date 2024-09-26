using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.BLL.Models.Departments
{
	public class CreatedDepartmentDto
	{
		public string Name { get; set; }

		public string Code { get; set; }

		public string? Description { get; set; }
		[Display(Name="Date of Creation")]
		public DateOnly CreationDate { get; set; }
	

	}
}
