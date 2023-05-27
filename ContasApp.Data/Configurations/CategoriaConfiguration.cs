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
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            // Definindo o nome da tabela no banco de dados
            builder.ToTable("Categoria");

            // Definindo a chave primária da tabela
            builder.HasKey(c => c.Id);

            //Mapeando os campos na tabela
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.Tipo)
                .HasColumnName("Tipo")
                .IsRequired();

            builder.Property(c => c.UsuarioId)
                .HasColumnName("UsuarioId")
                .IsRequired();

            // Mapeando o relacionamento OneToMany
            builder.HasOne(c => c.Usuario) // Categoria TEM 1 usuário
                .WithMany(u => u.Categorias) // Usuário TEM MUITAS Categorias
                .HasForeignKey(c => c.UsuarioId) // Chave estrangeira
                .OnDelete(DeleteBehavior.NoAction); // definindo o padrão de deleção dos dados para NoAction.
        }
    }
}