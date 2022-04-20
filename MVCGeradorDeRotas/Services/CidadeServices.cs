using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVCGeradorDeRotas.Services
{
    public class CidadeServices
    {
        private static readonly string _baseUri = "https://localhost:44366/api/";

        public CidadeServices() { }

        public static async Task<List<Cidade>> Get()
        {
            var cidadeJson = new List<Cidade>();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);

                    HttpResponseMessage response = await client.GetAsync("Cidades");
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        cidadeJson = JsonConvert.DeserializeObject<List<Cidade>>(responseBody);
                    }
                    return cidadeJson;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<Cidade> Get(string id)
        {
            var cidadeJson = new Cidade();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);

                    HttpResponseMessage response = await client.GetAsync("Cidades/" + id);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        cidadeJson = JsonConvert.DeserializeObject<Cidade>(responseBody);
                    }
                    return cidadeJson;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
