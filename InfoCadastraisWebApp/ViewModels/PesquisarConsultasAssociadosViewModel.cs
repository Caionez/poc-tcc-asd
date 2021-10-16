using System.Collections.Generic;
using InfoCadastraisWebApp.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InfoCadastraisWebApp.ViewModels
{
    public class PesquisarConsultasAssociadosViewModel
    {
        public int IdAssociado { get; set; }
        public int IdConveniado { get; set; }
        public SelectList Associados { get; set; }

        public SelectList Conveniados { get; set; }

        public List<ConsultaDTO> ConsultasEncontradas { get; set; }
    }
}