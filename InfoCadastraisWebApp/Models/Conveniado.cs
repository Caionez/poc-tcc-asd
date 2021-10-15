using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InfoCadastraisWebApp.Models
{
    public class Conveniado
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }

        public ICollection<Prestador> Prestadores { get; set; }
    }
}