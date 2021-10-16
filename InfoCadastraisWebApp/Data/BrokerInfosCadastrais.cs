using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using InfoCadastraisWebApp.Models;
using System.Text.Json;

namespace InfoCadastraisWebApp.Data
{
    public interface IInfosCadastraisBroker
    {
        Task<IEnumerable<Conveniado>> BuscarConveniados();
        Task<IEnumerable<Consulta>> BuscarConsultasAssociadoPorConveniado(int idConveniado, int idAssociado);
        Task<IEnumerable<Prestador>> BuscarPrestadoresPorEspecialidade(string nomeEspecialidade);
    }

    public class InfosCadastraisBroker : IInfosCadastraisBroker
    {
        private readonly HttpClient _client;
        private const string UrlBroker = "https://localhost:5003/api/broker";
        private readonly string RecursoBuscaConveniados = $"{UrlBroker}/conveniados";
        private readonly string RecursoBuscaConsultasPorConveniados = $"{UrlBroker}/consultas/";
        private readonly string RecursoBuscaPrestadoresPorEspecialidade = $"{UrlBroker}/prestadores/";

        public InfosCadastraisBroker()
        {
            _client = new HttpClient();
        }

        public async Task<IEnumerable<Conveniado>> BuscarConveniados()
        {
             HttpResponseMessage response = await _client.GetAsync(RecursoBuscaConveniados);

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<IEnumerable<Conveniado>>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<IEnumerable<Consulta>> BuscarConsultasAssociadoPorConveniado(int idConveniado, int idAssociado)
        {
            var url = $"{RecursoBuscaConsultasPorConveniados}/{idConveniado}/{idAssociado}";

            HttpResponseMessage response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<IEnumerable<Consulta>>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<IEnumerable<Prestador>> BuscarPrestadoresPorEspecialidade(string nomeEspecialidade)
        {
            var url = $"{RecursoBuscaPrestadoresPorEspecialidade}/{nomeEspecialidade}";

            HttpResponseMessage response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<IEnumerable<Prestador>>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }
    }
}