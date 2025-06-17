using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkFollow.Services;
using WorkFollow.Web.Models;

namespace WorkFollow.Web
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
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromDays(7);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //factory method tanımlamaları
            services.AddFactory<IGenericRepository, GenericRepository>();
            services.AddFactory<IKullaniciService, KullaniciService>();
            services.AddFactory<IGrupService, GrupService>();
            services.AddFactory<IProjeService, ProjeService>();
            services.AddFactory<IEtkinlikService, EtkinlikService>();
            services.AddFactory<ITblNotService, TblNotService>();
            services.AddFactory<IKullaniciNotService, KullaniciNotService>();
            services.AddFactory<IDokumanService, DokumanService>();
            services.AddFactory<IKullaniciDokumanService, KullaniciDokumanService>();

            //ilk atamaları yapabilmek için
            GenericModels.Nesne();

            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();
            app.UseSession();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

/// <summary>
///     factory Method
/// </summary>
public static class ServiceCollectionExtensions
{
    public static void AddFactory<TService, TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    {
        services.AddTransient<TService, TImplementation>();
        services.AddScoped<TService, TImplementation>();
        services.AddSingleton<Func<TService>>(x => () => x.GetService<TService>());
    }
}