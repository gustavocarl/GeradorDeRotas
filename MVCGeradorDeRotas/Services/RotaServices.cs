using Model;
using MongoDB.Driver;
using MVCGeradorDeRotas.Repository;
using System.Collections.Generic;

namespace MVCGeradorDeRotas.Services
{
    public class RotaServices
    {
        private readonly IMongoCollection<Rotas> _rotas;

        public RotaServices(IRotasConfigurationSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _rotas = database.GetCollection<Rotas>(settings.RotasCollectionName);
        }

        public List<Rotas> Get()
        {
            List<Rotas> rotas = new List<Rotas>();
            rotas = _rotas.Find(rotas => true).ToList();

            return rotas;
        }

        public Rotas GetId(string id)
        {
            Rotas rotas = new Rotas();
            rotas = _rotas.Find(rotas => rotas.Id == id).FirstOrDefault();

            return rotas;
        }

        public void Create(Rotas rotas)
        {
            _rotas.InsertOne(rotas);
        }

        public async void Delete(string id)
        {
            _rotas.DeleteOne(equipe => equipe.Id == id);
        }
    }
}
