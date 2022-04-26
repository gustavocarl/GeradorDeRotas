using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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

                using (var stream = new FileStream(caminhoFinal, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }
                foiSalvoComSucesso = true;
                return foiSalvoComSucesso;


            }
            catch (Exception)
            {
                return foiSalvoComSucesso;
            }
        }

        public static void EscreverStringNoDiretorio(List<string> texto = null, string stringUnica = null, string titulo = "a", string caminhoWeb = "")
        {
            string arquivo = titulo + ".txt";
            string diretorio = @"\Arquivo\";
            string caminhoFinal = caminhoWeb + diretorio + arquivo;

            if (texto != null)
            {
                using (StreamWriter escrever = new StreamWriter(caminhoFinal))
                {
                    texto.ForEach(textoUnico =>
                    {
                        escrever.WriteLine(textoUnico);
                    });
                }
            }
            else if (stringUnica != null)
            {
                using (StreamWriter escrever = new StreamWriter(caminhoFinal))
                    escrever.WriteLine(stringUnica);
            }
        }

    }
}
