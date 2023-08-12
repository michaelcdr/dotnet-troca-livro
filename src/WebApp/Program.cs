using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.Helpers;
using TrocaLivro.Aplicacao.Mapping;
using WebApp.IOC;


namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

//var builder = WebApplication.CreateBuilder(args);
//builder.Configuration
//    .SetBasePath(builder.Environment.ContentRootPath)
//    .AddJsonFile("appsettings.json", true, true)
//    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
//    .AddEnvironmentVariables();

//builder.Services
//    .AddControllersWithViews()
//    .AddFluentValidation();

//builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
//builder.Services
//    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.Cookie.Name = "TrocaLivro";
//        options.LoginPath = "/Usuario/Login";
//        options.Events = new CookieAuthenticationEvents
//        {
//            OnSignedIn = async context => { await Task.CompletedTask; },
//            OnSigningIn = async context => { await Task.CompletedTask; },
//            OnValidatePrincipal = async context => { await Task.CompletedTask; }
//        };
//    });

//string API_URL = builder.Configuration.GetSection("ApiPath").Value;
//builder.Services.Configure<AmbienteConfigHelper>(builder.Configuration.GetSection(nameof(AmbienteConfigHelper)));
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddHttpClients(API_URL);
//builder.Services.AddValidators();

//builder.Services.AddAutoMapper(typeof(UsuarioProfile));
//builder.Services.AddAutoMapper(typeof(LivroProfile));

//builder.Services.AddMvc().AddRazorOptions(options =>
//{
//    options.ViewLocationFormats.Add("/{0}.cshtml");
//});