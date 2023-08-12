using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.CasosDeUsos.DisponibilizarLivroParaTroca;
using TrocaLivro.Aplicacao.HttpClients;
using TrocaLivro.Aplicacao.ViewModels;

namespace WebApp.IOC
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHttpClients(this IServiceCollection provider, string apiUrl)
        {
            provider.AddHttpClient<LivroApiClient>(config => { config.BaseAddress = new Uri(apiUrl); });
            provider.AddHttpClient<UsuarioApiClient>(config => { config.BaseAddress = new Uri(apiUrl); });
            provider.AddHttpClient<PacoteApiClient>(config => { config.BaseAddress = new Uri(apiUrl); });
            provider.AddHttpClient<AutorApiClient>(config => { config.BaseAddress = new Uri(apiUrl); });
            provider.AddHttpClient<SubCategoriaApiClient>(config => { config.BaseAddress = new Uri(apiUrl); });
            provider.AddHttpClient<CategoriaApiClient>(config => { config.BaseAddress = new Uri(apiUrl); });
            provider.AddHttpClient<EditoraApiClient>(config => { config.BaseAddress = new Uri(apiUrl); });
        }

        public static void AddValidators(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IValidator<DisponibilizarLivroParaTrocaViewModel>, DisponibilizarLivroParaTrocaValidator>();
            serviceCollection.AddTransient<IValidator<AvaliarLivroViewModel>, AvaliarLivroValidator>();
            serviceCollection.AddTransient<IValidator<EditarUsuarioCommand>, EditarUsuarioCommandValidator>();
            serviceCollection.AddTransient<IValidator<CriarAutorViewModel>, CriarAutorViewModelValidator>();
            serviceCollection.AddTransient<IValidator<CriarAutorCommand>, CriarAutorCommandValidator>();
            serviceCollection.AddTransient<IValidator<CriarCategoriaViewModel>, CriarCategoriaViewModelValidator>();
            serviceCollection.AddTransient<IValidator<CriarCategoriaCommand>, CriarCategoriaCommandValidator>();
            serviceCollection.AddTransient<IValidator<CriarSubCategoriaViewModel>, CriarSubCategoriaViewModelValidator>();
            serviceCollection.AddTransient<IValidator<CriarSubCategoriaCommand>, CriarSubCategoriaCommandValidator>();
            serviceCollection.AddTransient<IValidator<CriarEditoraViewModel>, CriarEditoraViewModelValidator>();
        }
    }
}
