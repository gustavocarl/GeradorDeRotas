using Model;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeradorDeRotas.Service
{
    public class PessoaService
    {
        private readonly  HttpClient client = new HttpClient();

        public PessoaService()
        {

        }

        public List<Pessoa> Get()
        {

            try
            {
                HttpResponseMessage response = client.GetAsync("https://localhost:44370/api/Pessoas").Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var pessoasJson = JsonConvert.DeserializeObject<List<Pessoa>>(responseBody);
                return pessoasJson;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public Pessoa Get(string id) => _pessoa.Find(pessoa => pessoa.Id == id).FirstOrDefault();

        //public void Create(Pessoa pessoa) => _pessoa.InsertOne(pessoa);

        //public void Update(string id, Pessoa pessoa) => _pessoa.ReplaceOne(x => x.Id == id, pessoa);

        //public void Remove(string id) => _pessoa.DeleteOne(pessoa => pessoa.Id == id);

    }
}
