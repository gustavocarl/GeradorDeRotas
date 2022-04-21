using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
                    var result = await client.PostAsync("Usuarios/", content);
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

        public static async Task<Usuario> PutUsuario(string id, Usuario editarUsuario)
        {
            var usuario = new Usuario();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    var jsonUsuario = JsonConvert.SerializeObject(editarUsuario);
                    var content = new StringContent(jsonUsuario, Encoding.UTF8, "application/json");
                    var result = await client.PutAsync($"Usuarios/{editarUsuario.Id}", content);
                    if (result.IsSuccessStatusCode)
                        return editarUsuario;
                    else
                        editarUsuario = null;
                    return editarUsuario;
                }
            }
            catch (HttpRequestException)
            {
                usuario = null;
                return usuario;
            }
        }

        public static async Task<Usuario> DeleteUsuario(string id, Usuario usuario)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    var result = await client.DeleteAsync("Usuarios/" + id);
                    if (result.IsSuccessStatusCode)
                        return usuario;
                    else
                        usuario = null;
                    return usuario;
                }
            }
            catch (HttpRequestException)
            {
                usuario = null;
                return usuario;
            }
        }
    }
}
