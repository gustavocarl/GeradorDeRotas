namespace MicroServicesRota.Repository
{
    public class MicroServicePessoaSettings : IMicroServicePessoaSettings
    {

        public string PessoaCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
