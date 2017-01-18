using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using MemeDB.Services;
using MemeDB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace MemeDB
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.Production.json")
                            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(Configuration);
            services.AddScoped<IMemeData, SqlMemeData>();
            services.AddDbContext<MemeDbContext>(options => 
                    options.UseSqlServer(Configuration.GetConnectionString("MemeAzureDB")));
            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<MemeDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseExceptionHandler(new ExceptionHandlerOptions
                    {
                        ExceptionHandler = context => context.Response.WriteAsync("Oops!")
                    });
                }

                app.UseFileServer();
                app.UseIdentity();

            
                app.UseMvc(ConfigureRoutes);
            }

            catch (Exception ex)
            {
                app.Run(async context =>
                {
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(ex.Message);
                });
            }

        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // /Home/Index/
            
            // Convention based routing
            routeBuilder.MapRoute("Default",
                "{controller=Home}/{action=Index}/{id?}");            
        }
    }
}
