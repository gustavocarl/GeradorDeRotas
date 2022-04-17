namespace MicroServicesEquipe.Repository
{
    public interface IMicroServiceEquipeSettings
    {
        string EquipeCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

    }
}
