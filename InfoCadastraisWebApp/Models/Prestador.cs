using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InfoCadastraisWebApp.Models
{
    public class Prestador
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Formacao { get; set; }

        public ICollection<Especialidade> Especialidades { get; set; }
        public Conveniado Conveniado { get; set; }
    }
}