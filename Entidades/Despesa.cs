namespace Agro.API.Entidades
{
    public class Despesa
    {
        public Guid Id { get; set; }

        // FK obrigatória para AnoSafra
        public Guid AnoSafraId { get; set; }
        public AnoSafra AnoSafra { get; set; }

        // FK obrigatória para Categoria
        public Guid DespesaCategoriaId { get; set; }
        public DespesaCategoria DespesaCategoria { get; set; }

        // Campos extras
        public string Descricao { get; set; }
        public DateTime DataRegistro { get; set; }
        public decimal Valor { get; set; }
    }
}