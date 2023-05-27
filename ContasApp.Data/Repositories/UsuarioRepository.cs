using ContasApp.Data.Contexts;
using ContasApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>
    {
        public Usuario? GetByEmail(string email)
        {
            using(var context = new DataContext())
            {
                return context.Usuario
                    .FirstOrDefault(u => u.Email.Equals(email));
            }
        }

        public Usuario? GetByEmailAndSenha(string email, string senha)
        {
            using(var context = new DataContext())
            {
                return context.Usuario
                    .FirstOrDefault(u => u.Email.Equals(email)
                                      && u.Senha.Equals(senha));
            }
        }
    }
}