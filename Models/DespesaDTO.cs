using Agro.API.Entidades;

namespace Agro.Models
{
    public class DespesaDto
    {

        public Guid anoSafraId { get; set; }
        public Guid despesaCategoriaId { get; set; }
        public string descricao { get; set; }
        public DateTime dataRegistro { get; set; }
        public decimal valor { get; set; }

        public Despesa ConverterDespesaDTOEmDespesa(DespesaDto despesaDto)
        {
            return new Despesa
            {
                AnoSafraId = despesaDto.anoSafraId,
                Valor = despesaDto.valor,
                Descricao = despesaDto.descricao,
                DespesaCategoriaId = despesaDto.despesaCategoriaId

            };
            
        }
    }



}

