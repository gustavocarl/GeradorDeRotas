using Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace MVCGeradorDeRotas.Services
{
    public class LeituraDeArquivos
    {
        public static List<Cidade> LerArquivoTxtCidades(string caminhoWeb)
        {
            List<Cidade> cidades = new List<Cidade>();

            string arquivo = @"\Arquivo\Municipios\";

            string caminho = caminhoWeb + arquivo + "municipios-sp.txt";

            using (StreamReader leitura = new(caminho))
            {
                var linha = leitura.ReadLine();

                while (linha != null)
                {
                    cidades.Add(new Cidade { Nome = linha.Trim().Replace(",", "").Replace("\"", "") });
                    linha = leitura.ReadLine();
                }
            }
            return cidades;

        }

        public static List<IDictionary<string, string>> LerArquivoExcel(List<string>, colunas, string caminhoWeb)
        {
            List<string> planilha = new List<string>();

            string caminho = @"\Arquivo\Excel\";

            List<IDictionary<string, string>> listDictonary = new List<IDictionary<string, string>>();


        }
    }
}
