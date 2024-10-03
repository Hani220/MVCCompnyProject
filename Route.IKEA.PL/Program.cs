using Microsoft.EntityFrameworkCore;
using Route.IKEA.BLL.Services.Departments;
using Route.IKEA.BLL.Services.Employees;
using Route.IKEA.DAL.Persistance.Data;
using Route.IKEA.DAL.Persistance.Repositories.Departments;
using Route.IKEA.DAL.Persistance.Repositories.Employees;


namespace Route.IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Register ApplicationDbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            // Register DepartmentRepository and DepartmentService
            // Register repositories
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();  // Register the interface with the implementation
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();  // Assuming there's an interface for DepartmentRepository

            // Register services
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();  // Register EmployeeService with its interface
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();  // Register DepartmentService with its interface




            #endregion

            var app = builder.Build();

            #region Configure Kestrel Middlewares

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion

            app.Run();
        }
    }
}
