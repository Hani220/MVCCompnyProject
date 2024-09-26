﻿using System.ComponentModel.DataAnnotations;

namespace Route.IKEA.PL.ViewModels.Department
{
    public class DepartmentEditViewModel
    {
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        [Display(Name = "Creation Date")]
        public DateOnly CreationDate { get; set; }
    }
}
