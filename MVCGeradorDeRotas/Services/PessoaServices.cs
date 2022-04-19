using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVCGeradorDeRotas.Services
{
    public class PessoaServices
    {

        private static readonly string _baseUri = "https://localhost:44370/api/";
        public PessoaServices() { }

        public static async Task<List<Pessoa>> Get()
        {
            var pessoasJson = new List<Pessoa>();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    HttpResponseMessage response = await client.GetAsync("Pessoas");
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        pessoasJson = JsonConvert.DeserializeObject<List<Pessoa>>(responseBody);
                    }

                    return pessoasJson;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
