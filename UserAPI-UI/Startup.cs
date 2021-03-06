using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Syncfusion.Blazor;
using UserAPI_UI.Contractas;
using UserAPI_UI.Providers;
using UserAPI_UI.Services;

namespace UserAPI_UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSyncfusionBlazor();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBlazoredLocalStorage();
            services.AddHttpClient();
            services.AddScoped<APIAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(p =>p.GetRequiredService<APIAuthenticationStateProvider>());
            services.AddScoped<JwtSecurityTokenHandler>();
            services.AddTransient<IAuthenticationRepo, AuthenticationRepo>();
            services.AddTransient<IEmpleadoRepo, EmpleadoRepo>();
            services.AddTransient<IPuestoRepo, PuestoRepo>();
            services.AddTransient<IDepartamentoRepo, DepartamentoRepo>();
            services.AddTransient<ISitiosRepo, SitiosRepo>();
            services.AddTransient<IEstructuraRepo, EstructuraRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjg0MDA0QDMxMzgyZTMyMmUzMGhUem9ia1VCWmVEUUwyKzA5MUx3c09MTzErVnBudVJIWDVqZDd6Z1BDeDg9");
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
