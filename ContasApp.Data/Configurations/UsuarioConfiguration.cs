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
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //Definindo o nome da tabela no Banco de Dados
            builder.ToTable("Usuario");

            //Definindo a chave primária da tabela
            builder.HasKey(u => u.Id);

            //Definindo os campos da tabela
            builder.Property(u => u.Id)
                .HasColumnName("Id");

            builder.Property(u => u.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("Email")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Senha)
                .HasColumnName("Senha")
                .HasMaxLength(40)
                .IsRequired();

            //Criando índice para o campo e-mail tornando-o único no banco de dados
            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}