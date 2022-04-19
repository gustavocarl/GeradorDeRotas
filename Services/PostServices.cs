using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Services
{
    public class PostServices
    {

        static readonly HttpClient client = new HttpClient();

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

    }
}
