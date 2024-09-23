﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.DLL.Models.Department
{
	public class Department :ModelBase
	{
		public string Name { get; set; }
		
		public string Code { get; set; }

		public string? Description { get; set; }

		public DateTime CreationDate { get; set; }
	}
}
