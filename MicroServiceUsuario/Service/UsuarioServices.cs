using MicroServiceUsuario.Config;
using Model;
using MongoDB.Driver;
using System.Collections.Generic;

namespace MicroServiceUsuario.Service
{
    public class UsuarioServices
    {
        private readonly IMongoCollection<Usuario> _usuario;

        public UsuarioServices(IMicroServiceUsuarioSettings settings)
        {

            var usuario = new MongoClient(settings.ConnectionString);
            var database = usuario.GetDatabase(settings.DatabaseName);
            _usuario = database.GetCollection<Usuario>(settings.UsuarioCollectionName);

        }

        public List<Usuario> Get() => _usuario.Find(usuario => true).ToList();

        public Usuario Get(string id) => _usuario.Find<Usuario>(usuario => usuario.Id == id).FirstOrDefault();

        public Usuario GetNome(string nome) => _usuario.Find<Usuario>(usuario => usuario.NomeCompleto == nome).FirstOrDefault();

        public Usuario GetLogin(string login) => _usuario.Find<Usuario>(usuario => usuario.Login == login).FirstOrDefault();

        public Usuario Create(Usuario novoUsuario)
        {
            _usuario.InsertOne(novoUsuario);
            return novoUsuario;
        }

        public void Update(string id, Usuario pessoaIn) => _usuario.ReplaceOne(pessoa => pessoa.Id == id, pessoaIn);

        public void Delete(Usuario pessoaIn) => _usuario.DeleteOne(pessoa => pessoa.Id == pessoaIn.Id);


    }
}
