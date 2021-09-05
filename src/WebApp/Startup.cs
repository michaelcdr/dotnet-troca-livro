using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.HttpClients;
using TrocaLivro.Aplicacao.Mapping;

namespace WebApp
{
    public class Startup
    {
        private const string API_URL = "https://localhost:5001/api/v1.0/";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "TrocaLivro";
                    options.LoginPath = "/Usuario/Login";
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnSignedIn = async context => { await Task.CompletedTask; },
                        OnSigningIn = async context => { await Task.CompletedTask; },
                        OnValidatePrincipal = async context => { await Task.CompletedTask; }
                    };
                });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpClient<LivroApClient>(config => { config.BaseAddress = new Uri(API_URL); });
            services.AddHttpClient<UsuarioApiClient>(config => { config.BaseAddress = new Uri(API_URL); });
            services.AddAutoMapper(typeof(UsuarioProfile));
            services.AddAutoMapper(typeof(LivroProfile));

            services.AddMvc().AddRazorOptions(options =>
            {
                options.ViewLocationFormats.Add("/{0}.cshtml");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}