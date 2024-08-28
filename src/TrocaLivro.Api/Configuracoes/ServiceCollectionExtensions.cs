using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Text;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Repositorios;
using TrocaLivro.Dominio.Transacoes;
using TrocaLivro.Infra.Data;
using TrocaLivro.Infra.Repositorios.EF;
using TrocaLivro.Infra.Services;
using TrocaLivro.Infra.Transacoes;

namespace TrocaLivro.Api.Configuracoes
{
    public static class ServiceCollectionExtensions
    {
        public static void RegistrarDependenciasInfra(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUsuariosRepositorio, UsuariosRepositorio>();
            services.AddTransient<ILivrosRepositorio, LivrosRepositorio>();
            services.AddTransient<IAutoresRepositorio, AutoresRepositorio>();
            services.AddTransient<IEditorasRepositorio, EditorasRepositorio>();
            services.AddTransient<ICategoriasRepositorio, CategoriasRepositorio>();
            services.AddTransient<IGerenciadorToken, GerenciadorToken>();
        }

        public static void ConfigurarSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "TrocaLivro.Api", 
                    Version = "v1", 
                    Description = "Documentação da API"
                });

                var securityScheme = new OpenApiSecurityScheme()
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Autenticação Bearer via JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });
        }

        public static void ConfigurarIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
                var key = Encoding.ASCII.GetBytes(configuration["JwtConfig:Secret"]);

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
        }
    }
}