using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CidadesAPI
    {
        private static readonly string _baseUri = "https://servicodados.ibge.gov.br/api/v1/localidades/estados/35/";

        public static async Task<Cidade> GetCidadesAPI()
        {
            var cidadesJson = new Cidade();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);

                    HttpResponseMessage response = await client.GetAsync("municipios");
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        cidadesJson = JsonConvert.DeserializeObject<Cidade>(responseBody);
                    }
                    return cidadesJson;
                }
            }
            catch (HttpRequestException)
            {
                cidadesJson = null;
                return cidadesJson;
            }
        }
    }
}
