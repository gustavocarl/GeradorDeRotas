using MicroServicesEquipe.Repository;
using Model;
using MongoDB.Driver;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServicesEquipe.Service
{
    public class EquipeServices
    {
        private readonly IMongoCollection<Equipe> _equipe;

        public EquipeServices(IMicroServiceEquipeSettings settings)
        {
        
            var equipe = new MongoClient(settings.ConnectionString);
            var database = equipe.GetDatabase(settings.DatabaseName);
            _equipe = database.GetCollection<Equipe>(settings.EquipeCollectionName);
        
        }

        public List<Equipe> Get() => _equipe.Find(equipe => true).ToList();

        public Equipe Get(string nome) => _equipe.Find(equipe => equipe.Nome == nome).SingleOrDefault();

        public async Task<Equipe> Create (Equipe equipe)
        {
            
            var buscarCidadeAPI = await BuscarAPI.BuscarCidadeAPI(equipe.Cidade.Nome);

            equipe.Cidade = buscarCidadeAPI;
            _equipe.InsertOne(equipe);
            return equipe;

        }

        public void Update(string nome, Equipe equipeIn) => _equipe.ReplaceOne(equipe => equipe.Nome == nome, equipeIn);

        public void Delete(string nome) => _equipe.DeleteOne(equipe => equipe.Nome == nome);
    }
}
