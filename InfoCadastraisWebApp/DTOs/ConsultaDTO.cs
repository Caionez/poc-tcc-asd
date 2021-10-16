using System;
using System.ComponentModel.DataAnnotations;

namespace InfoCadastraisWebApp.DTOs
{
    public class ConsultaDTO
    {
        public int Id { get; set; }
        public DateTime DataConsulta { get; set; }

        public string NomePrestador { get; set; }
        public string NomeEspecialidade { get; set; }
        public string NomeConveniado { get; set; }
    }
}