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

        public Equipe Get(string id) => _equipe.Find(equipe => equipe.Id == id).SingleOrDefault();

        public Equipe GetEquipeNome(string nome) => _equipe.Find(equipe => equipe.Nome == nome).SingleOrDefault();

        public async Task<Equipe> Create(Equipe novaEquipe)
        {
            var listaPessoas = new List<Pessoa>();

            foreach (var item in novaEquipe.Pessoa)
            {
                try
                {
                    Pessoa verificarPessoa = await ServicesPessoa.GetNome(item.NomeCompleto);
                    UpdateServices.UpdatePessoa(item.NomeCompleto, new Pessoa()
                    {
                        Id = verificarPessoa.Id,
                        NomeCompleto = verificarPessoa.NomeCompleto,
                        NomeEquipe = verificarPessoa.NomeEquipe
                    });
                    listaPessoas.Add(verificarPessoa);
                }
                catch (System.Exception)
                {
                    throw;
                }
            }

            var buscarCidade = await ServicesCidade.GetNome(novaEquipe.Cidade.Nome);

            novaEquipe.Pessoa = listaPessoas;
            novaEquipe.Cidade = buscarCidade;

            _equipe.InsertOne(novaEquipe);
            return novaEquipe;
        }

        public void Update(string id, Equipe equipeIn) => _equipe.ReplaceOne(equipe => equipe.Id == id, equipeIn);

        public void Delete(Equipe equipeIn) => _equipe.DeleteOne(equipe => equipe.Id == equipeIn.Id);
    }
}
