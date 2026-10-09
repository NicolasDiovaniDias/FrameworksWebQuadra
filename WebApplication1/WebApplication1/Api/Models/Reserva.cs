namespace WebApplication1.Api.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public int QuadraID { get; set; }
        public Quadra Quadra { get; set; }
        private Reserva() { }
        public Reserva(DateTime Inicio, DateTime Fim)
        {
            if(Inicio >= Fim)
            {
                throw new ArgumentException("o valor de inicio não pode ser maior que o de fim");
            }
            this.Inicio = Inicio;
            this.Fim = Fim;
        }
    }
}
