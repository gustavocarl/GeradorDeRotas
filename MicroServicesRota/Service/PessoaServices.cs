using MicroServicesRota.Repository;
using Model;
using MongoDB.Driver;
using System.Collections.Generic;

namespace MicroServicesRota.Service
{
    public class PessoaServices
    {
        private readonly IMongoCollection<Pessoa> _pessoa;

        public PessoaServices(IMicroServicePessoaSettings settings)
        {
            var pessoa = new MongoClient(settings.ConnectionString);
            var database = pessoa.GetDatabase(settings.DatabaseName);
            _pessoa = database.GetCollection<Pessoa>(settings.PessoaCollectionName);
        }

        public List<Pessoa> Get() => _pessoa.Find(pessoa => true).ToList();

        public Pessoa Get(string id) => _pessoa.Find<Pessoa>(pessoa => pessoa.Id == id).FirstOrDefault();

        public Pessoa GetNome(string nome) => _pessoa.Find<Pessoa>(pessoa => pessoa.Nome == nome).FirstOrDefault();

        public Pessoa Create(Pessoa novaPessoa)
        {
            _pessoa.InsertOne(novaPessoa);
            return novaPessoa;
        }

        public void Update(string id, Pessoa pessoaIn) => _pessoa.ReplaceOne(pessoa => pessoa.Id == id, pessoaIn);

        public void Delete(Pessoa pessoaIn) => _pessoa.DeleteOne(pessoa => pessoa.Id == pessoaIn.Id);

    }
}
