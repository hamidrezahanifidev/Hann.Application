using System;
using System.IO;
using Hahn.ApplicatonProcess.Application.Data.Context;
using Hahn.ApplicatonProcess.Application.Service.Implementation;
using Hahn.ApplicatonProcess.Application.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using FluentValidation;
using Hahn.ApplicatonProcess.Application.Domain.Entities;
using Hahn.ApplicatonProcess.Application.Data.Validators;
using System.Linq;

namespace Hahn.ApplicatonProcess.Application.Web
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
            services.AddControllers()
                .AddFluentValidation();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hahn.ApplicationProcess", Version = "v1" });
                var filePath = Path.Combine(AppContext.BaseDirectory, "Hahn.ApplicatonProcess.Application.Web.xml");
                var filePath2 = Path.Combine(AppContext.BaseDirectory, "Hahn.ApplicatonProcess.Application.Service.xml");
                c.IncludeXmlComments(filePath);
                c.IncludeXmlComments(filePath2);
            });

            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseInMemoryDatabase(databaseName: "MyDb"));
            services.AddHttpClient();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAssetService, AssetService>();
            services.AddTransient<IValidator<Asset>, AssetValidator>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                    builder =>
                    {
                        builder.WithOrigins(Configuration.GetSection("AppSettings:aureliaFront").Value)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
            });
        }

        public RequestLocalizationOptions GetLocalizationOptions()
        {
            var cultures = Configuration.GetSection("Cultures")
                .GetChildren().ToDictionary(x => x.Key, x => x.Value);

            var supportedCultures = cultures.Keys.ToArray();

            var localizationOptions = new RequestLocalizationOptions()
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            return localizationOptions;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRequestLocalization(GetLocalizationOptions());
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("MyPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
        }
    }
}
