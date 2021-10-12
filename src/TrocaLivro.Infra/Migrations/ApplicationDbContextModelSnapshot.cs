﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Infra.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.Arquivo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descritivo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LivroId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LivroId");

                    b.ToTable("Arquivos");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.Autor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Autores");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.Avaliacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AvaliadoEm")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.Property<int>("Nota")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("LivroId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Avaliacoes");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.Editora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Editoras");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.Imagem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Altura")
                        .HasColumnType("int");

                    b.Property<int>("Largura")
                        .HasColumnType("int");

                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Nome")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("LivroId");

                    b.ToTable("Imagens");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.Livro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AlteradoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ano")
                        .HasColumnType("int");

                    b.Property<string>("CadastradoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataDaDelecao")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deletado")
                        .HasColumnType("bit");

                    b.Property<string>("DeletadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EditoraId")
                        .HasColumnType("int");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumeroPaginas")
                        .HasColumnType("int");

                    b.Property<int>("SubCategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Subtitulo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("EditoraId");

                    b.HasIndex("SubCategoriaId");

                    b.ToTable("Livros");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.LivroAutor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AutorId")
                        .HasColumnType("int");

                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AutorId");

                    b.HasIndex("LivroId");

                    b.ToTable("LivrosAutores");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.LivroDisponibilizadoParaTroca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descritivo")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("DisponibilizadoEm")
                        .HasColumnType("datetime2");

                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.Property<int>("Pontos")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioQueDisponibilizouParaTrocaId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("LivroId");

                    b.HasIndex("UsuarioQueDisponibilizouParaTrocaId");

                    b.ToTable("LivrosDisponibilizadosParaTrocas");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.SubCategoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("SubCategorias");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.TipoUsuario", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("TiposUsuarios");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("Pontos")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("TrocaLivro.Dominio.Entidades.TipoUsuario", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TrocaLivro.Dominio.Entidades.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TrocaLivro.Dominio.Entidades.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("TrocaLivro.Dominio.Entidades.TipoUsuario", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrocaLivro.Dominio.Entidades.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TrocaLivro.Dominio.Entidades.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.Arquivo", b =>
                {
                    b.HasOne("TrocaLivro.Dominio.Entidades.Livro", "Livro")
                        .WithMany("Arquivos")
                        .HasForeignKey("LivroId");

                    b.Navigation("Livro");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.Avaliacao", b =>
                {
                    b.HasOne("TrocaLivro.Dominio.Entidades.Livro", "Livro")
                        .WithMany("Avaliacoes")
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrocaLivro.Dominio.Entidades.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Livro");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.Imagem", b =>
                {
                    b.HasOne("TrocaLivro.Dominio.Entidades.Livro", "Livro")
                        .WithMany("Imagens")
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Livro");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.Livro", b =>
                {
                    b.HasOne("TrocaLivro.Dominio.Entidades.Editora", "Editora")
                        .WithMany("Livros")
                        .HasForeignKey("EditoraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrocaLivro.Dominio.Entidades.SubCategoria", "SubCategoria")
                        .WithMany("Livros")
                        .HasForeignKey("SubCategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Editora");

                    b.Navigation("SubCategoria");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.LivroAutor", b =>
                {
                    b.HasOne("TrocaLivro.Dominio.Entidades.Autor", "Autor")
                        .WithMany("LivrosAutores")
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrocaLivro.Dominio.Entidades.Livro", "Livro")
                        .WithMany("Autores")
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Autor");

                    b.Navigation("Livro");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.LivroDisponibilizadoParaTroca", b =>
                {
                    b.HasOne("TrocaLivro.Dominio.Entidades.Livro", "Livro")
                        .WithMany("DiponibilizacaoParaTrocas")
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrocaLivro.Dominio.Entidades.Usuario", "UsuarioQueDisponibilizouParaTroca")
                        .WithMany("Trocas")
                        .HasForeignKey("UsuarioQueDisponibilizouParaTrocaId");

                    b.Navigation("Livro");

                    b.Navigation("UsuarioQueDisponibilizouParaTroca");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.SubCategoria", b =>
                {
                    b.HasOne("TrocaLivro.Dominio.Entidades.Categoria", "Categoria")
                        .WithMany("SubCategorias")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.Autor", b =>
                {
                    b.Navigation("LivrosAutores");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.Categoria", b =>
                {
                    b.Navigation("SubCategorias");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.Editora", b =>
                {
                    b.Navigation("Livros");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.Livro", b =>
                {
                    b.Navigation("Arquivos");

                    b.Navigation("Autores");

                    b.Navigation("Avaliacoes");

                    b.Navigation("DiponibilizacaoParaTrocas");

                    b.Navigation("Imagens");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.SubCategoria", b =>
                {
                    b.Navigation("Livros");
                });

            modelBuilder.Entity("TrocaLivro.Dominio.Entidades.Usuario", b =>
                {
                    b.Navigation("Trocas");
                });
#pragma warning restore 612, 618
        }
    }
}
