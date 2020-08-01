using System.Collections.Generic;
using AspNetCore.FeatureManagement.UI.Core.Models;
using AspNetCore.FeatureManagement.UI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleFeaturesApi.Services;

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
                var themes = new List<string>
                {
                    "light",
                    "dark"
                };

                string STORAGE_MODE = "IN_MEMORY";

                if (STORAGE_MODE == "IN_MEMORY")
                    c.AddInMemoryStorage();
                if (STORAGE_MODE == "SQL_SERVER")
                    c.AddSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"));

                c
                    .ServerFeature("Beta", true)
                    .ServerFeature("WelcomeMessage", "Welcome to my Blog")
                    .ServerFeature("Delay", 1000, "Animation delay", uiSuffix: "ms")
                    .ClientFeature("Theme", themes[0], "Choose a theme for the frontend", themes);

                c.AuthSchemes.Add(new NoAuthenticationScheme());
                c.AuthSchemes.Add(new QueryAuthenticationScheme { Key = "Username" });
            });

            services.AddScoped<IFeaturesAuthService, SampleFeaturesAuthService>();

            services.AddHttpContextAccessor();

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
