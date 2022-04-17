namespace MicroServicesEquipe.Repository
{
    public class MicroServiceEquipeSettings : IMicroServiceEquipeSettings
    {
        public string EquipeCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
