using System;
using System.ComponentModel.DataAnnotations;

namespace InfoCadastraisBroker.Models
{
    public class Consulta
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataConsulta { get; set; }
        public Prestador Prestador { get; set; }
        public string Especialidade { get; set; }
        public Conveniado Conveniado { get; set; }
    }
}