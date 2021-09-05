using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using TrocaLivro.Api.Filtros;
using TrocaLivro.Aplicacao.Mapping;
using TrocaLivro.Aplicacao.Services;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Repositorios;
using TrocaLivro.Dominio.Services;
using TrocaLivro.Dominio.Transacoes;
using TrocaLivro.Infra.Configuracoes;
using TrocaLivro.Infra.Data;
using TrocaLivro.Infra.Repositorios.EF;
using TrocaLivro.Infra.Services;
using TrocaLivro.Infra.Transacoes;

namespace TrocaLivro.Api
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
            services.Configure<JwtConfiguracao>(Configuration.GetSection("JwtConfig"));

            services.AddCors();
            services.AddControllers();

            string connStr = Configuration.GetConnectionString("ContextConnection");

            services.AddDbContext<ApplicationDbContext>(opt => { opt.UseSqlServer(connStr); });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TrocaLivro.Api", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddMvc(option => {
                option.Filters.Add(typeof(ErrorResponseFilter));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer("JwtBearer", options => {
                var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);

                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = false
                };
            });

            // definições de regras de segurança para a password
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.User.RequireUniqueEmail = false;
            });
            
            services.AddIdentity<Usuario, TipoUsuario>()
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddRoleManager<RoleManager<TipoUsuario>>()
              .AddDefaultTokenProviders();
            
            var assembly = AppDomain.CurrentDomain.Load("TrocaLivro.Aplicacao");
            services.AddMediatR(assembly);
            
            services.AddAutoMapper(typeof(UsuarioProfile));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUsuariosRepositorio, UsuariosRepositorio>();
            services.AddTransient<ILivrosRepositorio, LivrosRepositorio>();
            services.AddTransient<IAutoresRepositorio, AutoresRepositorio>();
            services.AddTransient<IEditorasRepositorio, EditorasRepositorio>();
            services.AddTransient<ICategoriasRepositorio, CategoriasRepositorio>();

            services.AddTransient<ILivroService, LivroService>();
            services.AddTransient<IAutorService, AutorService>();
            services.AddTransient<IEditoraService, EditoraService>();
            services.AddTransient<IGeradorToken, GeradorToken>();

            services.AddApiVersioning();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            UserManager<Usuario> userManager, RoleManager<TipoUsuario> roleManager, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrocaLivro.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(x => x
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetIsOriginAllowed(origin => true) // allow any origin
              .AllowCredentials()); // allow credentials

            app.UseAuthentication();
            //app.UseAuthorization();

            var gerador = new GeradorDadosPadroesDaAplicacao(userManager, roleManager,context);
            gerador.Gerar().GetAwaiter().GetResult();

            //app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),@"Resources")),
            //    RequestPath = new PathString("/Resources")
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
