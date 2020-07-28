using System;
using System.Collections.Generic;
using AspNetCore.FeatureManagement.UI.Core.Models;
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
                    .ClientFeature("Theme", themes[0], "Choose a theme for the frontend", themes);

                string uniqueId = Guid.NewGuid().ToString();

                c.GetClientId = () =>
                {
                    return uniqueId;
                };
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
