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
    public class ServicesPessoa
    {
        private static readonly string _baseUri = "https://localhost:44370/api/";

        public ServicesPessoa() { }

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

        public static async Task<Pessoa> GetId(string id)
        {
            var pessoasJson = new Pessoa();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    HttpResponseMessage response = await client.GetAsync("Pessoas/" + id);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        pessoasJson = JsonConvert.DeserializeObject<Pessoa>(responseBody);
                    }
                    return pessoasJson;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Pessoa> GetNome(string nome)
        {
            var pessoasJson = new Pessoa();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    HttpResponseMessage response = await client.GetAsync("Pessoas/" + nome);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        pessoasJson = JsonConvert.DeserializeObject<Pessoa>(responseBody);
                    }
                    return pessoasJson;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Pessoa> PostPessoa(Pessoa novaPessoa)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    var jsonPessoa = JsonConvert.SerializeObject(novaPessoa);
                    var content = new StringContent(jsonPessoa, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync("MicroServicesPessoa", content);
                    if (result.IsSuccessStatusCode)
                        return novaPessoa;
                    else
                        novaPessoa = null;
                    return novaPessoa;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Pessoa> PutPessoa(Pessoa editarPessoa, Pessoa pessoaIn)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    var jsonPessoa = JsonConvert.SerializeObject(editarPessoa);
                    var content = new StringContent(jsonPessoa, Encoding.UTF8, "application/json");
                    var result = await client.PutAsync("MicroServicesPessoa", content);
                    if (result.IsSuccessStatusCode)
                        return editarPessoa;
                    else
                        editarPessoa = null;
                    return editarPessoa;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Pessoa> DeletePessoa(Pessoa removerPessoa)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    var pessoaJson = JsonConvert.SerializeObject(removerPessoa);
                    var content = new StringContent(pessoaJson, Encoding.UTF8, "application/json");
                    var result = await client.DeleteAsync("MicroServicesPessoa");
                    if (result.IsSuccessStatusCode)
                        return removerPessoa;
                    else
                        removerPessoa = null;
                    return removerPessoa;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}