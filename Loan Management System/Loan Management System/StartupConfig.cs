using Microsoft.Extensions.FileProviders;

namespace Loan_Management_System
{
    public class StartupConfig
    {
        public static void ConfigureWebHost(IWebHostBuilder webBuilder)
        {
            webBuilder.UseStartup<StartupConfig>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            // Map internal APIs
            app.Map("/api", apiApp =>
            {
                // Configure your internal APIs here
                // For example: apiApp.UseEndpoints(endpoints => endpoints.MapControllers());
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
