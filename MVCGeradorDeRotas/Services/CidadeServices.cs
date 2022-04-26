using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MVCGeradorDeRotas.Services
{
    public class CidadeServices
    {
        private static readonly string _baseUri = "https://localhost:44366/api/";
        
        private static readonly string _baseUriCidades = "https://servicodados.ibge.gov.br/api/v1/localidades/estados/SP/";
        
        public CidadeServices() { }

        public static async Task<List<Cidade>> Get()
        {
            var cidadeJson = new List<Cidade>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);

                    HttpResponseMessage response = await client.GetAsync("Cidades/");
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

		//public static async Task<List<Cidade>> GetCidadesIBGE()
		//{
		//	var cidadesJson = new List<Cidade>();

		//	try
		//	{
		//		using (var client = new HttpClient())
		//		{
		//			client.BaseAddress = new Uri(_baseUriCidades);

		//			HttpResponseMessage response = await client.GetAsync("municipios?view=nivelado");
		//			response.EnsureSuccessStatusCode();
		//			if (response.IsSuccessStatusCode)
		//			{
		//				string responseBody = response.Content.ReadAsStringAsync().Result;
		//				cidadesJson = JsonConvert.DeserializeObject<List<Cidade>>(responseBody);
		//			}
		//			return cidadesJson;
		//		}
		//	}
		//	catch (HttpRequestException)
		//	{
		//		cidadesJson = null;
		//		return cidadesJson;
		//	}
		//}

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
                var result = await client.PostAsync("Cidades/", content);
                if (result.IsSuccessStatusCode)
                    return novaCidade;
                else
                    novaCidade = null;
                return novaCidade;
            }
        }

        public static async Task<Cidade> PutCidade(string id, Cidade editarCidade)
        {
            var cidade = new Cidade();
            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    var cidadeJson = JsonConvert.SerializeObject(editarCidade);
                    var content = new StringContent(cidadeJson, Encoding.UTF8, "application/json");
                    var result = await client.PutAsync($"Cidades/{editarCidade.Id}", content);
                    if (result.IsSuccessStatusCode)
                        return editarCidade;
                    else
                        editarCidade = null;
                    return editarCidade;
                }
            }
            catch (HttpRequestException)
            {
                cidade = null;
                return cidade;
            }
        }

        public static async Task<Cidade> DeleteCidade(string id, Cidade cidade)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    var result = await client.DeleteAsync("Cidades/" + id);
                    if (result.IsSuccessStatusCode)
                        return cidade;
                    else
                        cidade = null;
                    return cidade;
                }
            }
            catch (HttpRequestException)
            {
                cidade = null;
                return cidade;
            }
        }

    }
}