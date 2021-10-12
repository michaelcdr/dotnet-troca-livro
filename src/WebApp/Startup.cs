using FluentValidation;
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
using System.Reflection;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.CasosDeUsos.DisponibilizarLivroParaTroca;
using TrocaLivro.Aplicacao.CasosDeUsos.LogarUsuario;
using TrocaLivro.Aplicacao.HttpClients;
using TrocaLivro.Aplicacao.Mapping;
using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Services;

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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpClient<LivroApiClient>(config => { config.BaseAddress = new Uri(API_URL); });
            services.AddHttpClient<UsuarioApiClient>(config => { config.BaseAddress = new Uri(API_URL); });
            services.AddAutoMapper(typeof(UsuarioProfile));
            services.AddAutoMapper(typeof(LivroProfile));
            
            services.AddTransient<IValidator<DisponibilizarLivroParaTrocaViewModel>, DisponibilizarLivroParaTrocaValidator>();
            services.AddTransient<IValidator<AvaliarLivroViewModel>, AvaliarLivroValidator>();
            
            services.AddMvc().AddRazorOptions(options =>
            {
                options.ViewLocationFormats.Add("/{0}.cshtml");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env , IMemoryCache memoryCache)
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

            HttpClient httpClient = new HttpClient() { BaseAddress = new Uri(API_URL) };

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