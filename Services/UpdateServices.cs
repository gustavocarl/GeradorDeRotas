using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UpdateServices
    {
        static readonly HttpClient client = new HttpClient();

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

    }
}
