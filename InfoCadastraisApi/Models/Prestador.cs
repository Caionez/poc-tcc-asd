using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InfoCadastraisApi.Models
{
    public class Prestador
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<EspecialidadePrestador> Especialidades { get; set; }
    }
}