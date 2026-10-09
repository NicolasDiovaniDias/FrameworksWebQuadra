namespace WebApplication1.Api.Models
{
    public class Quadra
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public List<Reserva> Reservas { get; set; } = new();
    }
}
