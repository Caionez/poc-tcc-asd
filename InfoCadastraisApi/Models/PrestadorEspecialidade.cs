using System.ComponentModel.DataAnnotations;

namespace InfoCadastraisApi.Models
{
    public class PrestadorEspecialidade
    {
        [Key]
        public int Id { get; set; }
        public int IdPrestador { get; set; }
        public int IdEspecialidade { get; set; }
    }
}