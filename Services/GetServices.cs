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
    public class GetServices
    {
        static readonly HttpClient client = new HttpClient();

        public static async Task<List<Cidade>> BuscarTodasCidades()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44366/api/Cidades");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var cidadeJson = JsonConvert.DeserializeObject<List<Cidade>>(responseBody);
                return cidadeJson;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<Cidade> BuscarCidadeAPI(string id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44366/api/Cidades/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var pessoaJson = JsonConvert.DeserializeObject<Cidade>(responseBody);
                return pessoaJson;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Cidade> BuscarCidadeNomeAPI(string nome)
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

        public static async Task<List<Pessoa>> BuscarTodasPessoas()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44370/api/Pessoas");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var pessoasJson = JsonConvert.DeserializeObject<List<Pessoa>>(responseBody);
                return pessoasJson;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Pessoa> BuscarPessoaIdAPI(string id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44370/api/Pessoas/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var pessoaJson = JsonConvert.DeserializeObject<Pessoa>(responseBody);
                return pessoaJson;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Pessoa> BuscarPessoaNomeAPI(string nome)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44370/api/Pessoas/" + nome);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var pessoaJson = JsonConvert.DeserializeObject<Pessoa>(responseBody);
                return pessoaJson;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<Equipe>> BuscarTodasEquipes()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44381/api/Equipes");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var equipeJson = JsonConvert.DeserializeObject<List<Equipe>>(responseBody);
                return equipeJson;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Equipe> BuscarEquipeAPI(string nome)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44381/api/Equipes/" + nome);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var equipeJson = JsonConvert.DeserializeObject<Equipe>(responseBody);
                return equipeJson;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<Usuario>> BuscarTodosUsuarios()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44315/api/Usuarios");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var usuarioJson = JsonConvert.DeserializeObject<List<Usuario>>(responseBody);
                return usuarioJson;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
