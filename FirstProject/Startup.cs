using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using FirstProject.Services;
using AutoMapper;
using FirstProject.Models;
//ing Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;

namespace FirstProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Creating these variables for DOCKER Environment variable.

            var server = Configuration["DBServer"] ?? "localhost";
            var port = Configuration["DBPort"] ?? "1433";
            var password = Configuration["DBPassword"] ?? "PA$$w0rd2019";
            var database= Configuration["DATABASE"] ?? "Student";
            var user = Configuration["DBUser"] ?? "sa";

            services.AddDbContext<DBContext>(options =>
                    options.UseSqlServer($"Server={server},{port};Initial Catalog={database};user Id={user};Password={password}"));

           
            services.AddControllers();

            services.AddDbContext<DBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DBContext")));

            services.AddTransient<IStudent,StudentRepo>();

            services.AddAutoMapper(typeof(UserProfile));

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen( c => {

                //Documnet API
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Swagger UI",
                    Version = "v1",
                    Description = "This API is use to call and post the data for Student and all the below APIs"
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            }
                );

            services.AddControllersWithViews();
        }

        //Craete this method for Automigration
        public static void SeedData(DBContext context)
        {
            System.Console.WriteLine("Applying Migrations..");
            context.Database.Migrate();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Create this method for auto seed Data.
            using (var  serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<DBContext>());
            }

            

            //System.Console.WriteLine("Applying Migrations...");
            ////Migrating all DB when container lanch and App runs
            //context.Database.Migrate();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
               
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMvc();

            
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
