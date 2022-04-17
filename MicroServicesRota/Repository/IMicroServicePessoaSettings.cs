namespace MicroServicesRota.Repository
{
    public interface IMicroServicePessoaSettings
    {

        string PessoaCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

    }
}
