using System.Collections.Generic;

namespace InfoCadastraisWebApp.DTOs
{
    public class PrestadorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<EspecialidadeDTO> Especialidades { get; set; }
    }
}