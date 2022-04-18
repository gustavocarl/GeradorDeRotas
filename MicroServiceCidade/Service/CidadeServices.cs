using MicroServiceCidade.Repository;
using Model;
using MongoDB.Driver;
using System.Collections.Generic;

namespace MicroServiceCidade.Service
{
    public class CidadeServices
    {
        private readonly IMongoCollection<Cidade> _cidade;

        public CidadeServices(IMicroServiceCidadeSettings settings)
        {
            
            var cidade = new MongoClient(settings.ConnectionString);
            var database = cidade.GetDatabase(settings.DatabaseName);
            _cidade = database.GetCollection<Cidade>(settings.CidadeCollectionName);

        }
        public List<Cidade> Get() => _cidade.Find(cidade => true).ToList();

        public Cidade Get(string id) => _cidade.Find<Cidade>(cidade => cidade.Id == id).FirstOrDefault();

        public Cidade GetNome(string nome) => _cidade.Find<Cidade>(cidade => cidade.Nome == nome).FirstOrDefault();

        public Cidade Create(Cidade novaCidade)
        {
            _cidade.InsertOne(novaCidade);
            return novaCidade;
        }

        public void Update(string id, Cidade cidadeIn) => _cidade.ReplaceOne(cidade => cidade.Id == id, cidadeIn);

        public void Delete(Cidade cidadeIn) => _cidade.DeleteOne(cidade => cidade.Id == cidadeIn.Id);

        //public void Delete(string nome) => _cidade.DeleteOne(cidade => cidade.Nome == nome);

    }
}
