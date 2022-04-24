using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MVCGeradorDeRotas.Services
{
    public class EscreverArquivo
    {

        public static async Task<bool> EscreverArquivoNoDiretorio(IFormFile arquivo, string caminhoWeb)
        {
            bool foiSalvoComSucesso = false;
            string nomeArquivo;
			
            try
			{

                nomeArquivo = "Planilha.xlsx";

                string diretorio = @"\Arquivo\";
                string caminhoFinal = caminhoWeb + diretorio + nomeArquivo;

                using(var stream = new FileStream(caminhoFinal, FileMode.Create))
				{
                    await arquivo.CopyToAsync(stream);
				}
                foiSalvoComSucesso = true;
                return foiSalvoComSucesso;


            }
            catch(Exception)
			{
                return foiSalvoComSucesso;
			}
        }

    }
}
