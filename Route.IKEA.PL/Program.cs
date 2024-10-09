using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Route.IKEA.BLL.Common.Services.Attachments;
using Route.IKEA.BLL.Services.Departments;
using Route.IKEA.BLL.Services.Employees;
using Route.IKEA.DAL.Entities.Identity;
using Route.IKEA.DAL.Persistance.Data;
using Route.IKEA.DAL.Persistance.Repositories.Departments;
using Route.IKEA.DAL.Persistance.Repositories.Employees;
using Route.IKEA.DAL.Persistance.UnitOfWork;
using Route.IKEA.PL.Mapping;


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
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();  
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); 

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();  
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();  
            builder.Services.AddTransient<IAttachmentService , AttachmentService>();

            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;


                options.User.RequireUniqueEmail = true;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);

            })
             .AddEntityFrameworkStores<ApplicationDbContext>();
            //.AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/SignIn"; // Path to the login page
                options.AccessDeniedPath = "/Home/Error"; // Path for access denied
                options.LogoutPath = "/Account/SignIn"; // Redirect to login after sign-out
            });





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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion

            app.Run();
        }
    }
}
