using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos.LogarUsuario;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.Helpers;
using TrocaLivro.Aplicacao.Mapping;
using TrocaLivro.Dominio.Responses;
using WebApp.Configurations;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services
    .AddControllersWithViews()
    .AddFluentValidation();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services
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

string API_URL = builder.Configuration.GetSection("ApiPath").Value;
builder.Services.Configure<AmbienteConfigHelper>(builder.Configuration.GetSection(nameof(AmbienteConfigHelper)));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClients(API_URL);
builder.Services.AddValidators();

builder.Services.AddAutoMapper(typeof(UsuarioProfile));
builder.Services.AddAutoMapper(typeof(LivroProfile));

builder.Services.AddMvc().AddRazorOptions(options =>
{
    options.ViewLocationFormats.Add("/{0}.cshtml");
});


var app = builder.Build();
AppServicesHelper.Services = app.Services;

if (app.Environment.IsDevelopment())
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

using(var sc = app.Services.CreateScope())
{
    var memoryCache = sc.ServiceProvider.GetService<IMemoryCache>();
    var memoryCacheOptions = new MemoryCacheEntryOptions();
    memoryCache.Set("ADM_TOKEN", appResponse.Dados.Token, memoryCacheOptions);
}

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
app.Run();