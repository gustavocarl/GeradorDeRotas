namespace MVCGeradorDeRotas.Repository
{
    public interface IRotasConfigurationSettings
    {
        string RotasCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}
