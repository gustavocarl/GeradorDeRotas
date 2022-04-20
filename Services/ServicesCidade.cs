using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServicesCidade
    {
        private static readonly string _baseUri = "https://localhost:44366/api/";

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

        public static async Task<Cidade> GetId(string id)
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

        public static async Task<Cidade> GetNome(string nome)
        {
            var cidadeJson = new Cidade();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);

                    HttpResponseMessage response = await client.GetAsync("Cidades/" + nome);
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
        public static async Task<Cidade> PostCidade(Cidade novaCidade)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);
                var cidadeJson = JsonConvert.SerializeObject(novaCidade);
                var content = new StringContent(cidadeJson, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("MicroServiceCidade", content);
                if (result.IsSuccessStatusCode)
                    return novaCidade;
                else
                    novaCidade = null;
                return novaCidade;
            }
        }

        public static async Task<Cidade> PutCidade(Cidade editarCidade)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);
                var cidadeJson = JsonConvert.SerializeObject(editarCidade);
                var content = new StringContent(cidadeJson, Encoding.UTF8, "application/json");
                var result = await client.PutAsync("MicroServiceCidade", content);
                if (result.IsSuccessStatusCode)
                    return editarCidade;
                else
                    editarCidade = null;
                return editarCidade;
            }
        }

        public static async Task<Cidade> DeleteCidade(Cidade removerCidade)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    var cidadeJson = JsonConvert.SerializeObject(removerCidade);
                    var content = new StringContent(cidadeJson, Encoding.UTF8, "application/json");
                    var result = await client.DeleteAsync("MicroServiceCidade");
                    if (result.IsSuccessStatusCode)
                        return removerCidade;
                    else
                        removerCidade = null;
                    return removerCidade;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
