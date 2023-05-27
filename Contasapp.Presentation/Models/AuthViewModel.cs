namespace ContasApp.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para as informações do usuário autenticado
    /// </summary>
    public class AuthViewModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public DateTime? DataHoraAcesso { get; set; }
    }
}