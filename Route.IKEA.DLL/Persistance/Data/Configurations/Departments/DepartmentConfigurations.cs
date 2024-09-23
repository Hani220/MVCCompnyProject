using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Route.IKEA.DLL.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.DLL.Persistance.Data.Configurations.Departments
{
	internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Department> builder)
		{
			builder.Property(D => D.Id).UseIdentityColumn(10,10);
			builder.Property(D => D.Name).HasColumnType("varchar (50)").IsRequired();
			builder.Property(D => D.Code).HasColumnType("varchar (20)").IsRequired();
			builder.Property(D => D.CreatedOn).HasDefaultValueSql("GETDATE()");
			builder.Property(D => D.CreationDate).HasComputedColumnSql("GETDATE()");


		}
	}
}
