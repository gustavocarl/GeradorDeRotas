namespace MicroServiceCidade.Repository
{
    public class MicroServiceCidadeSettings : IMicroServiceCidadeSettings
    {
        public string CidadeCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
