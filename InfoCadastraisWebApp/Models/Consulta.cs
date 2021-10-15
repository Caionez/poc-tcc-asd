using System;
using System.ComponentModel.DataAnnotations;

namespace InfoCadastraisWebApp.Models
{
    public class Consulta
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataConsulta { get; set; }

        public PlanoAssociado PlanoAssociado { get; set; }
        public Prestador Prestador { get; set; }
        public Especialidade Especialidade { get; set; }
        public Conveniado Conveniado { get; set; }
    }
}