using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NIJ.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NIJ.Web.Models.Infra;
using Microsoft.AspNetCore.Identity;

namespace NIJ.Web
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
            services.AddDbContext<IESContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IESConnection")));
            services.AddControllersWithViews();
            services.AddIdentity<UserAplication, IdentityRole>().AddEntityFrameworkStores<IESContext>().AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/Infra/Acessar";
                options.AccessDeniedPath = "/Infra/AcessoNegado";
            });
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

            //Retorno de status do erro.
            //app.UseStatusCodePages();
            
            //executa redicionamento passando o erro.
            app.UseStatusCodePagesWithReExecute("/Home/Error/", "?statusCode={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");               
                
            });

        }
    }
}
