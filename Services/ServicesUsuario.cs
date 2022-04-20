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
    public class ServicesUsuario
    {
        private static readonly string _baseUri = "https://localhost:44315/api/Usuarios";

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

        public static async Task<Usuario> GetId(string id)
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

        public static async Task<Usuario> GetLogin(string login)
        {
            var usuarioJson = new Usuario();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);

                    HttpResponseMessage response = await client.GetAsync("Usuarios/" + login);
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

        public static async Task<Usuario> PostUsuario(Usuario novoUsuario)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    var usuarioJson = JsonConvert.SerializeObject(novoUsuario);
                    var content = new StringContent(usuarioJson, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync("MicroServiceUsuario", content);
                    if (result.IsSuccessStatusCode)
                        return novoUsuario;
                    else
                        novoUsuario = null;
                    return novoUsuario;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Usuario> PutUsuario(Usuario editarUsuario)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    var jsonUsuario = JsonConvert.SerializeObject(editarUsuario);
                    var content = new StringContent(jsonUsuario, Encoding.UTF8, "application/json");
                    var result = await client.PutAsync("MicroServiceUsuario", content);
                    if (result.IsSuccessStatusCode)
                        return editarUsuario;
                    else
                        editarUsuario = null;
                    return editarUsuario;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Usuario> DeleteUsuario(Usuario removerUsuario)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    var usuarioJson = JsonConvert.SerializeObject(removerUsuario);
                    var content = new StringContent(usuarioJson, Encoding.UTF8, "application/json");
                    var result = await client.DeleteAsync("MicroServiceCidade");
                    if (result.IsSuccessStatusCode)
                        return removerUsuario;
                    else
                        removerUsuario = null;
                    return removerUsuario;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
