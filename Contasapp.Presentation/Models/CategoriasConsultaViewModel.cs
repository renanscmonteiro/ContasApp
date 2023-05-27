using ContasApp.Data.Enums;

namespace ContasApp.Presentation.Models
{
    /// <summary>
    /// Modelo de dados da página /Categorias/Consulta
    /// </summary>
    public class CategoriasConsultaViewModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public TipoCategoria? Tipo { get; set; }
    }
}