using System.Collections.Generic;
using AspNetCore.FeatureManagement.UI.Configuration.GroupFeature;
using AspNetCore.FeatureManagement.UI.Core.Models;
using AspNetCore.FeatureManagement.UI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleFeaturesApi.Constants;
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

                string STORAGE_MODE = "SQL_SERVER";

                switch (STORAGE_MODE)
                {
                    case "IN_MEMORY":
                        c.AddInMemoryStorage();
                        break;
                    case "SQL_SERVER":
                        c.AddSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"));
                        break;
                }

                c
                    .ServerFeature(FeatureNames.WelcomeMessage, "Welcome to my Blog")
                    .ServerFeature(FeatureNames.Delay, 1000, "Animation delay", uiSuffix: "ms")
                    .ClientFeature(FeatureNames.Beta, defaultValue: false, configuration: new GroupFeatureConfiguration<bool>
                    {
                        Groups = new List<GroupFeature<bool>>
                        {
                            new GroupFeature<bool> { Group = "Ring1", Value = true },
                            new GroupFeature<bool> { Group = "Ring2", Value = true },
                            new GroupFeature<bool> { Group = "Ring3", Value = false },
                            new GroupFeature<bool> { Group = "Ring4", Value = false }
                        }
                    })
                    .ClientFeature(FeatureNames.Theme, themes[0], "Choose a theme for the frontend", themes);

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
