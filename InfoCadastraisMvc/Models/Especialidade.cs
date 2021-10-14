using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InfoCadastraisMvc.Models
{
    public class Especialidade
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        
        public ICollection<Prestador> Prestadores { get; set; }
    }
}