using ContasApp.Data.Contexts;
using ContasApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Repositories
{
    public class ContaRepository : BaseRepository<Conta>
    {
        public List<Conta> GetAll(DateTime? dataIni, DateTime? dataFim, Guid? usuarioId)
        {
            using(var context = new DataContext())
            {
                return context.Conta
                    .Include(c => c.Categoria) // join
                    .Where(c => c.Data >= dataIni && c.Data <= dataFim && c.UsuarioId == usuarioId)
                    .OrderBy(c => c.Data)
                    .ToList();
            }
        }
    }
}
