using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.Common.Services;
using Project.Business;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Project.Service;

namespace Cloudstarter
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            IAppSettings appSettings = appSettingsSection.Get<AppSettings>();
            appSettings.SERVICE_BUS_CONNECTIONSTRING = Configuration["SERVICE_BUS_CONNECTIONSTRING"];
            appSettings.APPINSIGHTS_CONNECTIONSTRING = Configuration["APPINSIGHTS_CONNECTIONSTRING"];
            appSettings.SERVICE_BUS_CONNECTIONSTRING = Configuration["SERVICE_BUS_CONNECTIONSTRING"];


            services.AddSingleton<IAppSettings>(appSettings);

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins(appSettings.AllowedOrigins); // "http://localhost:3000","http://localhost:3001"
                                  });
            });

            var connectionString = string.Empty;

            // Used this when debugging in ENV=Production
            //if (_env.EnvironmentName == "Development")
            //{
            //    connectionString = this.Configuration.GetConnectionString("Development");
            //}
            //else
            //{
            //    connectionString = this.Configuration.GetConnectionString("Production");
            //}

            connectionString = this.Configuration.GetConnectionString("SqlConnection");
            appSettings.dbConnectionString = connectionString;

            services.AddDbContext<ProjectContext>(options => options.UseSqlServer(connectionString));
            //try
            //{
            //    var context = new ProjectContext(connectionString);
            //    context.Database.Migrate();
            //}
            //catch (Exception)
            //{

            //}

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project.Service", Version = "v1" });
            });

            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_CONNECTIONSTRING"]);

            services.AddScoped<IProjectBusinessLogic, ProjectBusinessLogic>();

            // Start background worker that will listen to the service bus.
            services.AddScoped<IServiceBus, ServiceBus>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (true) // env.IsDevelopment()
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project.Service v1"));
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
