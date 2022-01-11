using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using ProjetoLinx.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Data
{
    public class ProjetoLinxContext : DbContext
    {
        public ProjetoLinxContext(DbContextOptions<ProjetoLinxContext> options) : base(options)
        {
            var sqlServerOptionsExtension =
                options.FindExtension<SqlServerOptionsExtension>();
            StringConnection = sqlServerOptionsExtension.ConnectionString;
        }

        public string StringConnection { get; }

        // Definir tabelas do banco

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Anuncio> Anuncios { get; set; }
        public DbSet<PropostaParaAnuncio> Propostas { get; set; }
        public DbSet<Reuniao> Reunioes { get; set; }
        public DbSet<Topico> Topicos { get; set; }
        public DbSet<Anexo> Anexos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();

            #region Mapeamento tabela usuarios

            modelBuilder.Entity<Usuario>().HasKey(x => x.Id);

            // Nome de usuario
            modelBuilder.Entity<Usuario>().Property(x => x.Nome).HasMaxLength(50);
            modelBuilder.Entity<Usuario>().Property(x => x.Nome).HasColumnType("varchar(50)");
            modelBuilder.Entity<Usuario>().Property(x => x.Nome).IsRequired();


            // Email
            modelBuilder.Entity<Usuario>().Property(x => x.Email).HasMaxLength(200);
            modelBuilder.Entity<Usuario>().Property(x => x.Email).HasColumnType("varchar(200)");
            modelBuilder.Entity<Usuario>().Property(x => x.Email).IsRequired();
            modelBuilder.Entity<Usuario>().HasIndex(x => x.Email).IsUnique();


            // Senha
            modelBuilder.Entity<Usuario>().Property(x => x.Senha).HasMaxLength(200);
            modelBuilder.Entity<Usuario>().Property(x => x.Senha).HasColumnType("varchar(200)");
            modelBuilder.Entity<Usuario>().Property(x => x.Senha).IsRequired();


            // Telefone
            modelBuilder.Entity<Usuario>().Property(x => x.Telefone).HasMaxLength(12);
            modelBuilder.Entity<Usuario>().Property(x => x.Telefone).HasColumnType("varchar(12)");

            // Celular
            modelBuilder.Entity<Usuario>().Property(x => x.Celular).HasMaxLength(13);
            modelBuilder.Entity<Usuario>().Property(x => x.Celular).HasColumnType("varchar(13)");


            // CNPJ
            modelBuilder.Entity<Usuario>().Property(x => x.Cnpj).HasMaxLength(14);
            modelBuilder.Entity<Usuario>().Property(x => x.Cnpj).HasColumnType("varchar(14)");
            modelBuilder.Entity<Usuario>().Property(x => x.Cnpj).IsRequired();


            // Razão Social
            modelBuilder.Entity<Usuario>().Property(x => x.RazaoSocial).HasMaxLength(115);
            modelBuilder.Entity<Usuario>().Property(x => x.RazaoSocial).HasColumnType("varchar(115)");
            modelBuilder.Entity<Usuario>().Property(x => x.RazaoSocial).IsRequired();


            // Tipo de Usuário
            modelBuilder.Entity<Usuario>().Property(x => x.TipoUsuario).IsRequired();


            // Data criação
            modelBuilder.Entity<Usuario>().Property(x => x.DataCriacao).HasDefaultValueSql("getdate()");

            #endregion

            #region Mapeamento tabela anuncios

            modelBuilder.Entity<Anuncio>().HasKey(x => x.Id);

            // Foreign Key IdUsuario
            modelBuilder.Entity<Anuncio>()
                        .HasOne<Usuario>(o => o.Usuario)
                        .WithMany(c => c.Anuncios)
                        .HasForeignKey(s => s.IdUsuario);


            // Titulo Do anuncio
            modelBuilder.Entity<Anuncio>().Property(x => x.Titulo).HasMaxLength(50);
            modelBuilder.Entity<Anuncio>().Property(x => x.Titulo).HasColumnType("varchar(50)");
            modelBuilder.Entity<Anuncio>().Property(x => x.Titulo).IsRequired();

            // Descrição do anuncio
            modelBuilder.Entity<Anuncio>().Property(x => x.Descricao).HasMaxLength(300);
            modelBuilder.Entity<Anuncio>().Property(x => x.Descricao).HasColumnType("varchar(300)");
            modelBuilder.Entity<Anuncio>().Property(x => x.Descricao).IsRequired();

            // Status do anuncio
            modelBuilder.Entity<Anuncio>().Property(x => x.Status).IsRequired();


            // Data criação
            modelBuilder.Entity<Anuncio>().Property(x => x.DataCriacao).HasDefaultValueSql("getdate()");

            #endregion

            #region Mapeamento tabela propostas

            modelBuilder.Entity<PropostaParaAnuncio>().HasKey(x => x.Id);

            // Foreign Key UsuarioId
            modelBuilder.Entity<PropostaParaAnuncio>()
                        .HasOne<Usuario>(o => o.Usuario)
                        .WithMany(c => c.Propostas)
                        .HasForeignKey(s => s.FornecedorId);

            // Foreign Key AnuncioId
            modelBuilder.Entity<PropostaParaAnuncio>()
                        .HasOne<Anuncio>(o => o.Anuncio)
                        .WithMany(c => c.Propostas)
                        .HasForeignKey(s => s.AnuncioId);

            // Status da proposta
            modelBuilder.Entity<PropostaParaAnuncio>().Property(x => x.Status).IsRequired();

            // Data criação
            modelBuilder.Entity<PropostaParaAnuncio>().Property(x => x.DataCriacao).HasDefaultValueSql("getdate()");

            #endregion

            #region Mapeamento tabela reunioes

            modelBuilder.Entity<Reuniao>().HasKey(x => x.Id);

            // Foreign Key FornecedorId
            modelBuilder.Entity<Reuniao>()
                        .HasOne<Usuario>(o => o.Fornecedor)
                        .WithMany(c => c.ReunioesFornecedores)
                        .HasForeignKey(s => s.FornecedorId);

            // Foreign Key LojistaId
            modelBuilder.Entity<Reuniao>()
                        .HasOne<Usuario>(o => o.Lojista)
                        .WithMany(c => c.ReunioesLojistas)
                        .HasForeignKey(s => s.LojistaId);

            // Local da Reunião
            modelBuilder.Entity<Reuniao>().Property(x => x.Local).HasMaxLength(300);
            modelBuilder.Entity<Reuniao>().Property(x => x.Local).HasColumnType("varchar(300)");

            // Titulo Do anuncio
            modelBuilder.Entity<Reuniao>().Property(x => x.TituloAnuncio).HasMaxLength(50);
            modelBuilder.Entity<Reuniao>().Property(x => x.TituloAnuncio).HasColumnType("varchar(50)");
            modelBuilder.Entity<Reuniao>().Property(x => x.TituloAnuncio).IsRequired();

            // Data criação
            modelBuilder.Entity<Reuniao>().Property(x => x.DataCriacao).HasDefaultValueSql("getdate()");

            #endregion

            #region Mapeamento tabela topicos

            modelBuilder.Entity<Topico>().HasKey(x => x.Id);

            // Foreign Key ReuniaoId
            modelBuilder.Entity<Topico>()
                        .HasOne<Reuniao>(o => o.Reuniao)
                        .WithMany(c => c.Topicos)
                        .HasForeignKey(s => s.ReuniaoId);

            // Titulo do Topico
            modelBuilder.Entity<Topico>().Property(x => x.Titulo).HasMaxLength(50);
            modelBuilder.Entity<Topico>().Property(x => x.Titulo).HasColumnType("varchar(50)");
            modelBuilder.Entity<Topico>().Property(x => x.Titulo).IsRequired();

            #endregion

            #region Mapeamento tabela Anexos

            modelBuilder.Entity<Anexo>().HasKey(x => x.Id);

            // Foreign Key ReuniaoId
            modelBuilder.Entity<Anexo>()
                        .HasOne<Reuniao>(o => o.Reuniao)
                        .WithMany(c => c.Anexos)
                        .HasForeignKey(s => s.ReuniaoId);

            // Nome do Anexo
            modelBuilder.Entity<Anexo>().Property(x => x.Nome).HasMaxLength(200);
            modelBuilder.Entity<Anexo>().Property(x => x.Nome).HasColumnType("varchar(200)");
            modelBuilder.Entity<Anexo>().Property(x => x.Nome).IsRequired();

            // Caminho do Anexo
            modelBuilder.Entity<Anexo>().Property(x => x.Caminho).HasMaxLength(300);
            modelBuilder.Entity<Anexo>().Property(x => x.Caminho).HasColumnType("varchar(300)");
            modelBuilder.Entity<Anexo>().Property(x => x.Caminho).IsRequired();

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
