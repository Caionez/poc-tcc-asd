using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InfoCadastraisMvc.Models
{
    public class Prestador
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<Especialidade> Especialidades { get; set; }
        public Conveniado Conveniado { get; set; }
    }
}