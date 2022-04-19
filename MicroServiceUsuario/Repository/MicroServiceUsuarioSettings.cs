namespace MicroServiceUsuario.Config
{
    public class MicroServiceUsuarioSettings : IMicroServiceUsuarioSettings
    {
        public string UsuarioCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
