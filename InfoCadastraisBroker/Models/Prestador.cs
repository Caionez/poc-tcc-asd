using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InfoCadastraisBroker.Models
{
    public class Prestador
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<string> Especialidades { get; set; }
        public int IdConveniado { get; set; }
    }
}