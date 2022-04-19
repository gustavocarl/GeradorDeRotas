using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVCGeradorDeRotas.Services
{
    public class EquipeServices
    {
        private static readonly string _baseUri = "https://localhost:44381/api/";

        public EquipeServices() { }

        public static async Task<List<Equipe>> Get()
        {
            var equipeJson = new List<Equipe>();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);

                    HttpResponseMessage response = await client.GetAsync("Equipes");
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        equipeJson = JsonConvert.DeserializeObject<List<Equipe>>(responseBody);
                    }
                    return equipeJson;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
