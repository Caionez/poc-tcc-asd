using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using InfoCadastraisWebApp.Models;
using Newtonsoft.Json;
using System;

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
        private readonly string UrlBroker = Environment.GetEnvironmentVariable("UrlBroker");

        public InfosCadastraisBroker()
        {
            _client = new HttpClient();
        }

        public async Task<IEnumerable<Conveniado>> BuscarConveniados()
        {
            HttpResponseMessage response = await _client.GetAsync($"{UrlBroker}/conveniados");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IEnumerable<Conveniado>>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<IEnumerable<Consulta>> BuscarConsultasAssociadoPorConveniado(int idConveniado, int idAssociado)
        {
            var url = $"{UrlBroker}/consultas/{idConveniado}/{idAssociado}";

            HttpResponseMessage response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var resultado = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Consulta>>(resultado);
            }

            return null;
        }

        public async Task<IEnumerable<Prestador>> BuscarPrestadoresPorEspecialidade(string nomeEspecialidade)
        {
            var url = $"{UrlBroker}/prestadores/{nomeEspecialidade}";

            HttpResponseMessage response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IEnumerable<Prestador>>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }
    }
}