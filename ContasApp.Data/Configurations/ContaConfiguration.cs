using ContasApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Configurations
{
    public class ContaConfiguration : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            //Definindo o nome da tabela no Banco de Dados
            builder.ToTable("Conta");

            //Definindo a chave primária da tabela
            builder.HasKey(c => c.Id);

            //Mapeando os campos na tabela
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.Data)
                .HasColumnName("Data")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(c => c.Valor)
                .HasColumnName("Valor")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.Observacoes)
                .HasColumnName("Observacoes")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(c => c.CategoriaId)
                .HasColumnName("CategoriaId")
                .IsRequired();

            builder.Property(c => c.UsuarioId)
                .HasColumnName("UsuarioId")
                .IsRequired();

            // Mapeando o relacionamento OneToMany
            builder.HasOne(c => c.Categoria) // Conta tem 1 categoria
                .WithMany(ca => ca.Contas) // Categoria tem muitas contas
                .HasForeignKey(c => c.CategoriaId) // Chave estrangeira
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u => u.Usuario) // Conta tem 1 usuário
                .WithMany(c => c.Contas) // Usuario tem muitas contas
                .HasForeignKey(c => c.UsuarioId) // chave estrangeira
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}