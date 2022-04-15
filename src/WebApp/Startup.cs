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
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.CasosDeUsos.DisponibilizarLivroParaTroca;
using TrocaLivro.Aplicacao.CasosDeUsos.LogarUsuario;
using TrocaLivro.Aplicacao.Helpers;
using TrocaLivro.Aplicacao.HttpClients;
using TrocaLivro.Aplicacao.Mapping;
using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Responses;

namespace WebApp
{
    public static class ServiceProviderExtensions
    {
        public static T ResolveWith<T>(this IServiceProvider provider, params object[] parameters) where T : class =>
            ActivatorUtilities.CreateInstance<T>(provider, parameters);
    }
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
            services.AddHttpClient<LivroApiClient>(config => { config.BaseAddress = new Uri(API_URL); });
            services.AddHttpClient<UsuarioApiClient>(config => { config.BaseAddress = new Uri(API_URL); });
            services.AddHttpClient<PacoteApiClient>(config => { config.BaseAddress = new Uri(API_URL); });
            services.AddHttpClient<AutorApiClient>(config => { config.BaseAddress = new Uri(API_URL); });
            services.AddHttpClient<SubCategoriaApiClient>(config => { config.BaseAddress = new Uri(API_URL); });
            services.AddHttpClient<CategoriaApiClient>(config => { config.BaseAddress = new Uri(API_URL); });
            services.AddHttpClient<EditoraApiClient>(config => { config.BaseAddress = new Uri(API_URL); });

            services.AddAutoMapper(typeof(UsuarioProfile));
            services.AddAutoMapper(typeof(LivroProfile));

            services.AddTransient<IValidator<DisponibilizarLivroParaTrocaViewModel>, DisponibilizarLivroParaTrocaValidator>();
            services.AddTransient<IValidator<AvaliarLivroViewModel>, AvaliarLivroValidator>();
            services.AddTransient<IValidator<EditarUsuarioCommand>, EditarUsuarioCommandValidator>();

            services.AddTransient<IValidator<CriarAutorViewModel>, CriarAutorViewModelValidator>();
            services.AddTransient<IValidator<CriarAutorCommand>, CriarAutorCommandValidator>();

            services.AddTransient<IValidator<CriarCategoriaViewModel>, CriarCategoriaViewModelValidator>();
            services.AddTransient<IValidator<CriarCategoriaCommand>, CriarCategoriaCommandValidator>();

            services.AddTransient<IValidator<CriarSubCategoriaViewModel>, CriarSubCategoriaViewModelValidator>();
            services.AddTransient<IValidator<CriarSubCategoriaCommand>, CriarSubCategoriaCommandValidator>();

            services.AddTransient<IValidator<CriarEditoraViewModel>, CriarEditoraViewModelValidator>();

            

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
                app.UseExceptionHandler("/Home/Error");
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