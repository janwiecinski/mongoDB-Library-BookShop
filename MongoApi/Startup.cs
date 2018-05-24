using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using DataAcces.DAL.Repository;
using DataAcces.DAL.Models;
using DataAcces.DAL;
using AutoMapper;
using DataAcces.DAL.Interfaces;
using Hangfire;
using Microsoft.Owin;
using MongoApi.Utilts;
using MongoApi.Controllers;

[assembly: OwinStartup(typeof(MongoApi.Startup))]

namespace MongoApi
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
            services.Configure<IISOptions>(options => { options.ForwardClientCertificate = false; options.AutomaticAuthentication = true; });

            services.AddTransient<IRepository<BookModel>, GenericRepository<BookModel>>();
            services.AddTransient<IGenericService<BookModel>, GenericService<BookModel>>();
            services.AddScoped<IMyDatabase<BookModel>, MyDatabase<BookModel>>();
            services.AddHangfire(x => x.UseSqlServerStorage(@"Server=JANEK1985\SQLEXPRESS; Database=Hangfire; Integrated Security=SSPI;"));

            services.AddAutoMapper();
            
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
            app.UseHangfireServer();
            app.UseHangfireDashboard();
        }
    }
}
