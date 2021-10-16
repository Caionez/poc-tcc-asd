using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InfoCadastraisBroker.Models
{
    public class Conveniado
    {
        [JsonPropertyName("id")]
        [Key]
        public int Id { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
        [JsonPropertyName("endereco")]
        public string Endereco { get; set; }
    }
}