using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreMVCSQL.Models
{
    [Table("Logistica")]
    public class Logistica
    {
        [Column("Id")]
        [Display(Name ="Código")]
        public int Id { get; set; }

        [Column("Nome")]
        [Display(Name = "Nome")]
        public string? Nome { get; set; }

        public string? TipoCargueiro { get; set; }


        public DateTime DataSaida { get; set; }

        public DateTime DataRetorno { get; set; }

        public string? QtdObtida { get; set; }

        public string? MinerioObtido { get; set; }

        public DateTime DataRecebido { get; set; }

        public string? ValorCarga { get; set; }

        public string? MinerioSaida { get; set; }


    }
}
