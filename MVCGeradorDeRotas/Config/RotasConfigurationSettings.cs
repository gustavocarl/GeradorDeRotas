namespace MVCGeradorDeRotas.Repository
{
    public class RotasConfigurationSettings : IRotasConfigurationSettings
    {
        public string RotasCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
