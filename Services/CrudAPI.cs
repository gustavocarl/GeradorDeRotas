using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Services
{
    public class CrudAPI
    {

        static readonly HttpClient client = new HttpClient();

        #region Get

        #region Buscas Cidade

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

        #endregion

        #region Buscas da Pessoa

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

        #endregion

        #region Buscas Equipe

        public static async Task<List<Equipe>> BuscarTodasEquipes()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44381/api/Equipes");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var cidadeJson = JsonConvert.DeserializeObject<List<Equipe>>(responseBody);
                return cidadeJson;
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

        #endregion

        #endregion

        #region Post

        public static void PostCidade(Cidade novaCidade)
        {
            client.PostAsJsonAsync("https://localhost:44366/api/Cidades/", novaCidade);
        }


        public static void PostEquipe(Equipe novaEquipe)
        {
            client.PostAsJsonAsync("https://localhost:44381/api/Equipes/", novaEquipe);
        }

        public static void PostPessoa(Pessoa novaPessoa)
        {
            client.PostAsJsonAsync("https://localhost:44370/api/Pessoas/", novaPessoa);
        }


        #endregion

        #region Put

        public static void UpdateEquipe(string nome, Equipe equipeIn)
        {
            client.PutAsJsonAsync("https://localhost:44381/api/Equipes/" + nome, equipeIn);
        }

        public static void UpdateCidade(string nome, Cidade cidadeIn)
        {
            client.PutAsJsonAsync("https://localhost:44366/api/Cidades/" + nome, cidadeIn);
        }

        public static void UpdatePessoa(string nome, Pessoa pessoaIn)
        {
            client.PutAsJsonAsync("https://localhost:44370/api/Pessoas/" + nome, pessoaIn);
        }

        #endregion

        #region Delete

        public static void DeletePessoa(string id)
        {
            client.DeleteAsync("https://localhost:44370/api/Pessoas/" + id);
        }

        public static void DeleteCidade(string id)
        {
            client.DeleteAsync("https://localhost:44366/api/Cidades/" + id);
        }

        public static void DeleteEquipe(string id)
        {
            client.DeleteAsync("https://localhost:44381/api/Equipes/" + id);
        }


        #endregion

    }
}
