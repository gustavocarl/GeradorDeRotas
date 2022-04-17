using Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public class BuscarAPI
    {
        static readonly HttpClient client = new HttpClient();

        public static async Task<Cidade> BuscarCidadeAPI(string nome)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44366/api/Cidades/" + nome);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var cidadeJson = JsonConvert.DeserializeObject<Cidade>(responseBody);
                return cidadeJson;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
