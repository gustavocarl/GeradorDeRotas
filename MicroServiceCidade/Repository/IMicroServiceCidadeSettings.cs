namespace MicroServiceCidade.Repository
{
    public interface IMicroServiceCidadeSettings
    {
        string CidadeCollectionName { get; set; }

        string ConnectionString { get; set; }
        
        string DatabaseName { get; set; }
    }
}
