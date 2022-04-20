using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVCGeradorDeRotas
{
    public class UsuarioServices
    {
        private static readonly string _baseUri = "https://localhost:44315/api/";

        public UsuarioServices() { }

        public static async Task<List<Usuario>> Get()
        {
            var usuarioJson = new List<Usuario>();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);

                    HttpResponseMessage response = await client.GetAsync("Usuarios");
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        usuarioJson = JsonConvert.DeserializeObject<List<Usuario>>(responseBody);
                    }
                    return usuarioJson;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<Usuario> Get(string id)
        {
            var usuarioJson = new Usuario();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);

                    HttpResponseMessage response = await client.GetAsync("Usuarios/" + id);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        usuarioJson = JsonConvert.DeserializeObject<Usuario>(responseBody);
                    }
                    return usuarioJson;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
