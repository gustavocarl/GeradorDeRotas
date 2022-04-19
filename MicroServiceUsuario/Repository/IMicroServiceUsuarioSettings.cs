namespace MicroServiceUsuario.Config
{
    public interface IMicroServiceUsuarioSettings
    {
        string UsuarioCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

    }
}
