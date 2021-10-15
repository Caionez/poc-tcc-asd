using System.ComponentModel.DataAnnotations;
using InfoCadastraisWebApp.Models;
using System.Collections.Generic;

namespace InfoCadastraisWebApp.ViewModels
{
    public class ConsultaPrestadorViewModel
    {
        public string Especialidade { get; set; }

        [Display(Name = "Busca em conveniados externos")]
        public bool BuscaExterna { get; set; }

        public List<Prestador> PrestadoresEncontrados { get; set; }
    }
}