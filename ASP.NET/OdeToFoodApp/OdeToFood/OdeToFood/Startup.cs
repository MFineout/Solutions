using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OdeToFood.Data;

namespace OdeToFood
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
            //Allows OdeToFoodDbContext to be taken as a constructor parameter
            services.AddDbContextPool<OdeToFoodDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("OdeToFoodDb"));
            });

            //AddScoped scopes services to particular HTTP request. Everytime framework hands out SqlRestaurantData, it will keep handing out the same instance of SRD for a single request -- This allows dbcontext to collect all changes behind the scenes 
            services.AddScoped<IRestaurantData, SqlRestaurantData>();

            //Any component that needs IRestaurantData, give them InMemoryRestaurantData()
            //Allows IRestaurantData to be used as a constructor parameter
           //services.AddSingleton<IRestaurantData, InMemoryRestaurantData>(); //adds access to restaurant data 

            services.AddRazorPages();
            services.AddControllers();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(SayHelloMiddleWare);

            app.UseHttpsRedirection();
            app.UseStaticFiles(); //serves static files from wwwroot
            app.UseNodeModules(); // serves static files from node_modules

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }

        private RequestDelegate SayHelloMiddleWare(RequestDelegate next)
        {
            return async ctx => //ctx == context
            {
                if (ctx.Request.Path.StartsWithSegments("/hello")) //if at url: ~/hello
                {
                    await ctx.Response.WriteAsync("Hello World!");
                }
                else
                {
                    await next(ctx); //return to processing pipeline
                }
            };
        }
    }
}
