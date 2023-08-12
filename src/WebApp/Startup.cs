using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.CasosDeUsos.LogarUsuario;
using TrocaLivro.Aplicacao.Helpers;
using TrocaLivro.Aplicacao.Mapping;
using TrocaLivro.Dominio.Responses;
using WebApp.IOC;

namespace WebApp
{
    public class Startup
    {
        private readonly string API_URL = String.Empty;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            API_URL = configuration.GetSection("ApiPath").Value;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                    .AddFluentValidation();

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
            
            services.Configure<AmbienteConfigHelper>(Configuration.GetSection(nameof(AmbienteConfigHelper)));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpClients(API_URL);
            services.AddValidators();

            services.AddAutoMapper(typeof(UsuarioProfile));
            services.AddAutoMapper(typeof(LivroProfile));

            services.AddMvc().AddRazorOptions(options =>
            {
                options.ViewLocationFormats.Add("/{0}.cshtml");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env , IMemoryCache memoryCache)
        {
            AppServicesHelper.Services = app.ApplicationServices;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/erro/500");
                app.UseStatusCodePagesWithRedirects("/erro/{0}");
                app.UseHsts();
            }

            HttpClient httpClient = new() { BaseAddress = new Uri(API_URL) };

            var resposta = httpClient
                .PostAsJsonAsync("Usuario/Logar", new LogarUsuarioModel { Usuario = "michael", Senha = "123456" })
                .GetAwaiter().GetResult();            

            var appResponse = resposta.Content.ReadFromJsonAsync<AppResponse<LogarUsuarioResultado>>().GetAwaiter().GetResult();

            var memoryCacheOptions = new MemoryCacheEntryOptions();
            memoryCache.Set("ADM_TOKEN", appResponse.Dados.Token, memoryCacheOptions); 

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