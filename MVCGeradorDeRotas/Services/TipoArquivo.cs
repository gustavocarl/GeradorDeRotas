using Microsoft.AspNetCore.Http;

namespace MVCGeradorDeRotas.Services
{
	public class TipoArquivo
	{
		public static bool ehArquivoExcel(IFormFile arquivo)
		{
			var extensao = "." + arquivo.FileName.Split(".")[arquivo.FileName.Split(".").Length - 1];
			return (extensao == ".xlsx" || extensao == ".xls");

		}
	}
}
