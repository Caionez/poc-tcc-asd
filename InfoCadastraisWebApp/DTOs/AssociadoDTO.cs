using System;

namespace InfoCadastraisWebApp.DTOs
{
    public class AssociadoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
    }
}