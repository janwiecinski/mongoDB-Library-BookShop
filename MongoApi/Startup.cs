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
using MongoApi.Models;

[assembly: OwinStartup(typeof(MongoApi.Startup))]

namespace MongoApi
{
    public class Startup
    { 
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
           
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connstring = Configuration.GetSection("DatabaseConnection:ConnectionString").Value;
            var database = Configuration.GetSection("DatabaseConnection:Database").Value;
            var sendMessParam = Configuration.GetSection("SendMessageParams");
            var hangFireConnString = Configuration.GetSection("HangFireDatabaseConnection").Value;

            services.Configure<SendMessageParams>(sendMessParam);
            services.AddScoped<IMyDatabaseWrapper>(sp=> new MyDatabaseWrapper(connstring, database));
            services.AddTransient<IRepository<BookModel>, GenericRepository<BookModel>>();
            services.AddTransient<IBookModelService, BookModelService>();
            services.AddHangfire(x => x.UseSqlServerStorage(hangFireConnString));

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
