namespace ContasApp.Presentation.Models
{
    /// <summary>
    /// Modelo de dados da para o resultado da consulta de contatos
    /// </summary>
    public class ContasConsultaResultadoViewModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public DateTime? Data { get; set; }
        public decimal? Valor { get; set; }
        public string? Categoria { get; set; }
        public string? Tipo { get; set; }
        public string? Observacoes { get; set; }
    }
}