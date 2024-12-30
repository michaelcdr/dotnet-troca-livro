# 📚 Book Exchange Platform

Uma plataforma de troca de livros desenvolvida em .NET Core 8, seguindo os princípios de Domain-Driven Design (DDD) e Clean Architecture.

## 🏗️ Arquitetura

O projeto é dividido em duas principais aplicações:
- **API REST**: Desenvolvida em .NET Core 8, responsável pela lógica de negócios e persistência de dados
- **Frontend MVC**: Interface web que consome a API

### Estrutura da Solução

```
dotnet-troca-livro/
├── src/
│   ├── TrocaLivro.Dominio/              # Entidades e regras de negócio
│   ├── TrocaLivro.Aplicacao/            # Casos de uso e DTOs
│   ├── TrocaLivro.Infra/                # Implementações de persistência e serviços externos
│   ├── TrocaLivro.Api/                  # API REST
│   └── TrocaLivro.MVC/                  # Aplicação MVC Frontend
└── tests/
    ├── TrocaLivro.Testes.Unidade/
    └── TrocaLivro.Testes.Integracao/
```

## ⚙️ Tecnologias Utilizadas

- **.NET Core 8**
- **Entity Framework Core**
- **Dapper**
- **SQL Server**
- **MediatR**
- **AutoMapper**
- **FluentValidation**
- **Swagger/OpenAPI**
- **ASP.NET Core MVC**
- **Bootstrap 5**

## 🚀 Funcionalidades

- Cadastro e autenticação de usuários
- Cadastro de livros disponíveis para troca
- Busca de livros por título, autor, categoria

## 📋 Pré-requisitos

- .NET Core SDK 8.0 ou superior
- SQL Server
- Visual Studio 2022 ou VS Code

## 🔧 Configuração

1. Clone o repositório
```bash
git clone https://github.com/michaelcdr/dotnet-troca-livro.git
```

2. Configure a string de conexão no arquivo `appsettings.json` em ambos os projetos (API e MVC)

3. Execute as migrations
```bash
cd src/TrocaLivro.Api
dotnet ef database update
```

4. Execute os projetos
```bash
# Para a API
cd src/TrocaLivro.Api
dotnet run

# Para o frontend MVC
cd src/TrocaLivro.MVC
dotnet run
```

## 🏛️ Padrões e Princípios

  - Bounded Contexts
  - Entidades e Value Objects
  - Agregados e Repositórios
  - Eventos de Domínio
  - Separação de camadas
  - Inversão de Dependência
  - Use Cases
  - Interfaces adaptadoras

## 📦 Endpoints da API

A documentação completa da API está disponível através do Swagger UI em `/swagger` quando a aplicação está em execução.

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## 📞 Contato

Michael - michaelcdr.costa@gmail.com

Link do Projeto: [https://github.com/michaelcdr/dotnet-troca-livro](https://github.com/michaelcdr/dotnet-troca-livro)
