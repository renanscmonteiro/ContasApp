using ContasApp.Data.Contexts;
using ContasApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>
    {
        public List<Categoria> GetByUsuario(Guid? usuarioId)
        {
            using (var context = new DataContext())
            {
                return context.Categoria
                    .Where(c => c.UsuarioId == usuarioId)
                    .OrderBy(c => c.Nome)
                    .ToList();
            }
        }
    }
}