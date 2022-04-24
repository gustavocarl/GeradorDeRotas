using System.IO;

namespace MVCGeradorDeRotas.Services
{
    public class RemoverArquivo
    {
        public static void RemoverArquivoDoDiretorio(string titulo, string extensao, string caminhoWeb)
        {
            string nomeArquivo = titulo + extensao;
            string diretorio = @"\Arquivo\";
            string caminhoFinal = caminhoWeb + diretorio + nomeArquivo;

            if (File.Exists(caminhoFinal))
                File.Delete(caminhoFinal);
        }
    }
}
