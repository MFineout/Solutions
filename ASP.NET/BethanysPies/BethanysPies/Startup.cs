using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPies.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BethanysPies
{
    public class Startup
    {
        // After appsettings.json gets read out via CreateDefaultBuilder (program.cs) it arrives in an instance of IConfiguration, which gets passed into the application via constructor injection. In the IConfiguration object we have access to properties set in appsettings.
        public IConfiguration Configuration { get; } 

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration; //set local configuration to configuration being injected
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))); //retrieves ConnectionString from appsettings.json


            // When asked for I(nRepository) return instance of nRepository
            
            services.AddScoped<IPieRepository, PieRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            //when user comes to the site, it will create a scoped shopping cart using the GetCart method. In other words, the GetCart method is going to be invoked when the user sends a request. That gives me the ability to check if the cart ID is already in the session, if not, I pass it into this session and I return the ShoppingCart itself down here. This way, I'm sure that when a user comes to the site, a shopping cart will be associated with that request. And since it's scoped, it means it all interacts with that same shopping cart, within that same request, we'll use that same ShoppingCart.

            services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp));


            //indicates that Identity needs to use Entity Framework to store its data, and it's going to use your AppDbContext, which inherits from IdentityDbContext to do so.

            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
            


            // Before EF is setup use mock hard-coded data sets.
            /*  services.AddScoped<IPieRepository, MockPieRepository>();
              services.AddScoped<ICategoryRepository, MockCategoryRepository>(); */

            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddControllersWithViews();
            services.AddRazorPages(); //needed for identity features (authorization) because of scaffolded files
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession(); // this must come before use routing
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages(); //Also needed for scaffolded indentity features
                
            });
        }
    }
}
