namespace SampleFeaturesApi;

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
                .ServerFeature(FeatureNames.GameSeasons, defaultValue: "Summer", configuration: new TimeWindowFeatureConfiguration<string>
                {
                    TimeWindows = new List<TimeWindowFeature<string>>
                    {
                        new TimeWindowFeature<string> { StartDate = new DateTime(2020, 01, 01), EndDate = new DateTime(2020, 04, 01), Value = "Winter" },
                        new TimeWindowFeature<string> { StartDate = new DateTime(2020, 04, 01), EndDate = new DateTime(2020, 07, 01), Value = "Spring" },
                        new TimeWindowFeature<string> { StartDate = new DateTime(2020, 07, 01), EndDate = new DateTime(2020, 10, 01), Value = "Summer" },
                        new TimeWindowFeature<string> { StartDate = new DateTime(2020, 10, 01), EndDate = new DateTime(2021, 01, 01), Value = "Fall" }
                    }
                })
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
