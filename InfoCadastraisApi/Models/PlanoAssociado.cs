using System;
using System.ComponentModel.DataAnnotations;

namespace InfoCadastraisApi.Models
{
    public class PlanoAssociado
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataVigencia { get; set; }
        public TipoPlano TipoPlano { get; set; }
        public bool PlanoEmpresarial { get; set; }
        public bool PlanoOdontologico { get; set; }

        public int AssociadoId { get; set; }
        public Associado Associado { get; set; }
    }
}