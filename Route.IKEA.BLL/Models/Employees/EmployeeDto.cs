﻿using Microsoft.AspNetCore.Http;
using Route.IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.BLL.Models.Employees
{
	public class EmployeeDto
	{
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int? Age { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string Gender { get; set; } =null!;

        public string EmployeeType { get; set; } = null!;
        [Display(Name = "Department Id")]
        public int? DepartmentId { get; set; }

        [Display(Name = "Department Name")]
        public string? DepartmentName { get; set; }

        public string? Image { get; set; }
    }
}
