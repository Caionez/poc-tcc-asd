namespace InfoCadastraisApi.Models
{
    public class EspecialidadePrestador
    {
        public int IdEspecialidade { get; set; }
        public Especialidade Especialidade { get; set; }
        public int IdPrestador { get; set; }
        public Prestador Prestador { get; set;}
    }
}