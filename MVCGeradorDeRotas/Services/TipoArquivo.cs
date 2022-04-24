using Microsoft.AspNetCore.Http;
using System.IO;

namespace MVCGeradorDeRotas.Services
{
    public class TipoArquivo
    {
        public static bool EhArquivoExcel(IFormFile arquivo)
        {
            var extensao = "." + arquivo.FileName.Split(".")[arquivo.FileName.Split(".").Length - 1];
            return (extensao == ".xlsx" || extensao == ".xls");

        }

    }
}
