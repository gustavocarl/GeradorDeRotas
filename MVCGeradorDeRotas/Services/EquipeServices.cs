using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        public static async Task<Equipe> GetId(string id)
        {
            var equipeJson = new Equipe();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);

                    HttpResponseMessage response = await client.GetAsync("Equipes/" + id);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        equipeJson = JsonConvert.DeserializeObject<Equipe>(responseBody);
                    }
                    return equipeJson;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Equipe> GetNome(string nome)
        {
            var equipeJson = new Equipe();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);

                    HttpResponseMessage response = await client.GetAsync("Equipes/" + nome);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        equipeJson = JsonConvert.DeserializeObject<Equipe>(responseBody);
                    }
                    return equipeJson;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Equipe> PostEquipe(Equipe novaEquipe)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    var equipeJson = JsonConvert.SerializeObject(novaEquipe);
                    var content = new StringContent(equipeJson, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync("Equipes/", content);
                    if (result.IsSuccessStatusCode)
                        return novaEquipe;
                    else
                        novaEquipe = null;
                    return novaEquipe;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Equipe> PutEquipe(Equipe editarEquipe)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    var equipeJson = JsonConvert.SerializeObject(editarEquipe);
                    var content = new StringContent(equipeJson, Encoding.UTF8, "application/json");
                    var result = await client.PutAsync($"Equipes/{editarEquipe.Id}", content);
                    if (result.IsSuccessStatusCode)
                        return editarEquipe;
                    else
                        editarEquipe = null;
                    return editarEquipe;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<string> DeleteEquipe(string id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    var result = await client.DeleteAsync("Equipes/" + id);
                    if (result.IsSuccessStatusCode)
                        return "OK";
                    else
                        return "Falhou";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
