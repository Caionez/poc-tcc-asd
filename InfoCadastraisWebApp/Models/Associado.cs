using System;
using System.ComponentModel.DataAnnotations;

namespace InfoCadastraisWebApp.Models
{
    public class Associado
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }

        public PlanoAssociado Plano { get; set; }
    }
}