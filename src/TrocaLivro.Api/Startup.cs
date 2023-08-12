using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TrocaLivro.Api.Configuracoes;
using TrocaLivro.Api.Filtros;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.Mapping;
using TrocaLivro.Aplicacao.Services;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Infra.Configuracoes;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JwtConfiguracao>(Configuration.GetSection("JwtConfig"));

            string connStr = Configuration.GetConnectionString("ContextConnection");
            services.AddDbContext<ApplicationDbContext>(opt => { opt.UseSqlServer(connStr); });
            services.ConfigurarSwagger();
            services.AddMvc(option => {
                option.Filters.Add(typeof(ErrorResponseFilter));
            });
            services.ConfigurarIdentity(Configuration);
            services.AddCors();
            services.AddControllers();

            var assembly = AppDomain.CurrentDomain.Load("TrocaLivro.Aplicacao");
            services.AddMediatR(assembly);
            
            services.AddAutoMapper(typeof(UsuarioProfile));
            services.AddAutoMapper(typeof(LivroProfile));
            services.AddTransient<IValidator<EditarUsuarioCommand>, EditarUsuarioCommandValidator>();

            services.RegistrarDependenciasInfra();
            services.AddApiVersioning();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                              IWebHostEnvironment env, 
                              UserManager<Usuario> userManager, 
                              RoleManager<TipoUsuario> roleManager, 
                              ApplicationDbContext context)
        {
            
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrocaLivro.Api v1"));
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(x => x
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetIsOriginAllowed(origin => true) // allow any origin
              .AllowCredentials()); // allow credentials

            app.UseAuthentication();
            app.UseAuthorization();

            var gerador = new GeradorDadosPadroesDaAplicacao(userManager, roleManager,context);
            gerador.Gerar().GetAwaiter().GetResult();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
