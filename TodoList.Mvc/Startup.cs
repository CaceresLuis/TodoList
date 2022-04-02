using System;
using MediatR;
using TodoList.Mvc.Models.Entity;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using TodoList.Mvc.Core.Exepctions;
using TodoList.Mvc.Core.Validations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Mvc.Core.TodoModule.Infrastructure.Repository;
using TodoList.Mvc.Core.TodoModule.Aplication.TodoService.Create;

namespace TodoList.Mvc
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
            services.AddControllersWithViews().AddFluentValidation(conf => conf.RegisterValidatorsFromAssemblyContaining<ConfigValidations>());

            //Config Datacontex
            services.AddDbContext<DataContext>(conf =>
            {
                conf.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //Config MediatoR
            services.AddMediatR(typeof(CreateTodoCommand).Assembly);

            //Config Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ITodoRepository, TodoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<MiddelwareHandler>();
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Todos}/{action=Index}/{id?}");
            });
        }
    }
}
