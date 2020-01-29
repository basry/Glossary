using Glossary.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag.AspNetCore;

namespace Glossary
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.ConfigureServices(Configuration);

            services.AddSwaggerDocument((settings) =>
            {
                settings.Title = "Glossary Service";
                settings.Version = "v1";
                settings.Description = "Glossary Service APIs";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUi3();
            }
            else
            {
                app.UseHsts();
                app.UseSwagger();
                app.UseSwaggerUi3(c => c.SwaggerRoutes.Add(new SwaggerUi3Route("v1", "/Prod/swagger/v1/swagger.json")));
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMvc();

        }
    }
}
