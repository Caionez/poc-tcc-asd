using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace InfoCadastraisWebApp.Models
{
    public class Conveniado
    {
        [JsonProperty("id")]
        [Key]
        public int Id { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("endereco")]
        public string Endereco { get; set; }

        public ICollection<Prestador> Prestadores { get; set; }
    }
}