using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Route.IKEA.DAL.Common.Enums;
using Route.IKEA.DAL.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.DAL.Persistance.Data.Configurations.Employees
{
    internal class EmployeeConfiguration :IEntityTypeConfiguration<Employee>
    {

        
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(E => E.Address).HasColumnType("varchar(100)");
            builder.Property(E => E.Salary).HasColumnType("decimal(8,2)");
            builder.Property(E => E.CreatedOn).HasDefaultValueSql("GETUTCDATE()");

            builder.Property(E => E.Geneder).HasConversion
                (
                     (gender) => gender.ToString(),
                      (gender) => (Gender)Enum.Parse(typeof(Gender), gender)
                );
            
            builder.Property(E => E.EmployeeType).HasConversion
                (
                     (type) => type.ToString(),
                      (type) => (EmployeeType)Enum.Parse(typeof(EmployeeType), type)
                );

        }
    }
}
