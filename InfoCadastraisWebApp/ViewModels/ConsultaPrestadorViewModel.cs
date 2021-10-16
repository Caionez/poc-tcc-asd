using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using InfoCadastraisWebApp.DTOs;

namespace InfoCadastraisWebApp.ViewModels
{
    public class ConsultaPrestadorViewModel
    {
        public string Especialidade { get; set; }

        [Display(Name = "Busca em conveniados externos")]
        public bool BuscaExterna { get; set; }

        public List<PrestadorDTO> PrestadoresEncontrados { get; set; }
    }
}