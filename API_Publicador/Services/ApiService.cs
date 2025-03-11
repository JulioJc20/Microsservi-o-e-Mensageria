using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace API_Publicador.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(); 
        }
        public async Task<string> ChamarApiConsumidora(string nome)
        {
            var url = $"https://localhost:7067/pessoa/{nome}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return "Erro ao chamar a API Consumidora";

        }
      

        public async Task<string> ChamarApiCep(string cep)
        {
            var url = $"https://viacep.com.br/ws/{cep}/json/";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return "Erro ao chamar a Api que consulta o CEP";
        }







    }
}
