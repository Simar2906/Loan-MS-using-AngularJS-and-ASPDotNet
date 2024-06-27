using Loan_Management_System.Data;
using Loan_Management_System.Models;
using Loan_Management_System.Repository.LoanData;
using Loan_Management_System.Repository.UserData;
using Loan_Management_System.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Npgsql;

namespace Loan_Management_System
{
    public class StartupConfig
    {
        public static void ConfigureWebHost(IWebHostBuilder webBuilder)
        {
            webBuilder.UseStartup<StartupConfig>();
        }

        public IConfiguration Configuration { get; }

        public StartupConfig(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<UserRepository>();
            services.AddScoped<LoanRepository>();
            services.AddTransient<UserService>();
            services.AddTransient<LoanService>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
                          .EnableSensitiveDataLogging() // Enable this for debugging purposes
                          .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            // Map internal API
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Map API controllers
            });
            // Serve the AngularJS app from the "src" directory
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "src")),
                RequestPath = "",
                EnableDefaultFiles = true
            });

            // Set the default route to AngularJS app
            app.Use(async (context, next) =>
            {
                context.Request.Path = "/index.html";
                await next();
            });

            // Additional configuration, if needed
        }
    }
}
