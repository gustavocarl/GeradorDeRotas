using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DeleteServices
    {
        static readonly HttpClient client = new HttpClient();


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


    }
}
