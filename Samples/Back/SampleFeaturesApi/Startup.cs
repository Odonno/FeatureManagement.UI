using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SampleFeaturesApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFeatures(c =>
            {
                c.AddSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"))
                    .Feature("Beta", true)
                    .Feature("DarkTheme", false, "Enable dark theme in the frontend")
                    .Feature("WelcomeMessage", "Welcome to my Blog");
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(c => 
            {
                c.SetIsOriginAllowed(_ => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseFeatures();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapFeaturesUI();
            });
        }
    }
}
