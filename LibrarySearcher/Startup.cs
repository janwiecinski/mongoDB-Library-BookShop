using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataAcces.DAL.Repository;
using DataAcces.DAL.Models;
using DataAcces.DAL;
using DataAcces.DAL.Interfaces;
using AutoMapper;
using LibrarySearcher.Services;
using Amazon.S3;

namespace LibrarySearcher
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
            var connstring = Configuration.GetSection("DatabaseConnection:ConnectionString").Value;
            var database = Configuration.GetSection("DatabaseConnection:Database").Value;
            

            services.AddScoped<IMyDatabaseWrapper>(sp => new MyDatabaseWrapper(connstring, database));
            services.AddTransient<IRepository<BookModel>, GenericRepository<BookModel>>();
            services.AddTransient<IRepository<Author>, GenericRepository<Author>>();
            services.AddTransient<IRepository<Client>, GenericRepository<Client>>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IAuthorService, AuthorService>();

            services.AddAutoMapper();
            services.AddMvc();
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Book}/{action=Index}/{id?}");
                routes.MapRoute(
                   name: "bookSend",
                   template: "{controller=Book}/{action=CreateBookPackage}/{id?}");
            });
        }
    }
}
