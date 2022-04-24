using Model;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace MVCGeradorDeRotas.Services
{
    public class LeituraDeArquivos
    {
        public static bool ArquivoValido(string titulo, string extensao, string caminhoWeb)
        {
            string nomeArquivo = titulo + extensao;
            string diretorio = @"\File\";
            string caminho = caminhoWeb + diretorio + nomeArquivo;
            bool arquivoExiste = false;

            if (File.Exists(caminho))
                arquivoExiste = true;

            return arquivoExiste;
        }

        public static List<Cidade> LerArquivoTxtCidades(string caminhoWeb)
        {
            List<Cidade> cidades = new List<Cidade>();

            string diretorio = @"\Arquivo\";

            string caminho = caminhoWeb + diretorio + "municipios.txt";

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

        public static void OrdenarExcelCEP(string titulo, string extensao, string caminhoWeb)
        {
            string nomeArquivo = titulo + extensao;
            string diretorio = @"\Arquivo\";
            string caminhoFinal = caminhoWeb + diretorio + nomeArquivo;

            List<string> cabecalhoExcel = new List<string>();

            FileInfo arquivoExcel = new FileInfo(caminhoWeb + diretorio + "Planilha.xlsx");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(arquivoExcel))
            {
                ExcelWorkbook pastaDeTrabalho = package.Workbook;

                ExcelWorksheet planilha = package.Workbook.Worksheets[0];

                int colunas = planilha.Dimension.End.Column;
                int linhas = planilha.Dimension.End.Row;
                int colunaCep = 0;

                for (int coluna = 1; coluna <= colunas; coluna++)
                {
                    if (planilha.Cells[1, coluna].Value.ToString().ToUpper() == "CEP")
                    {
                        colunaCep = coluna - 1;
                        break;
                    }
                }

                planilha.Cells[2, 1, linhas, colunas].Sort(colunaCep, false);

                package.Save();
            }
        }

        public static List<IDictionary<string, string>> LerArquivoExcel(List<string> listaColunas, string caminhoWeb)
        {
            List<string> lista = new List<string>();

            string diretorio = @"\Arquivo\";

            List<IDictionary<string, string>> listDictonary = new List<IDictionary<string, string>>();

            FileInfo arquivoExcel = new FileInfo(caminhoWeb + diretorio + "Planilha.xlsx");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage())
            {

                ExcelWorksheet planilha = package.Workbook.Worksheets[0];

                int colunas = planilha.Dimension.End.Column;
                int linhas = planilha.Dimension.End.Row;

                IDictionary<string, string> data = new Dictionary<string, string>();

                for (int linha = 2; linha < linhas; linha++)
                {
                    data = new Dictionary<string, string>();

                    for (int coluna = 1; coluna < colunas; coluna++)
                    {
                        listaColunas.ForEach(coluna =>
                        {
                            if (planilha.Cells[1, colunas].Value.ToString() == coluna)
                            {
                                if (planilha.Cells[linhas, colunas].Value == null)
                                    data.Add(coluna, "");
                                else
                                    data.Add(coluna, planilha.Cells[linhas, colunas].Value.ToString());
                            }
                        });
                    }
                    if (data.Count > 1) listDictonary.Add(data);
                }
            }
            return listDictonary;
        }


        public static List<string> LeituraCabecalhoArquivoExcel(string caminhoWeb)
        {
            List<string> cabecalhoExcel = new List<string>();
            string diretorio = @"\Arquivo\";

            FileInfo arquivoExcel = new FileInfo(caminhoWeb + diretorio + "Planilha.xlsx");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(arquivoExcel))
            {

                ExcelWorksheet planilha = package.Workbook.Worksheets[0];
                int colunas = planilha.Dimension.End.Column;

                for (int i = 1; i < colunas; i++)
                {
                    cabecalhoExcel.Add(planilha.Cells[1, i].Value.ToString());
                }
            }
            return cabecalhoExcel;
        }


        public static List<string> LeituraColunasArquivoExcel(string caminhoWeb, string nomeColuna)
        {
            List<string> servicos = new List<string>();

            string diretorio = @"\Arquivo\";

            FileInfo arquivoExcel = new FileInfo(caminhoWeb + diretorio + "Planilha.xlsx");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(arquivoExcel))
            {
                ExcelWorksheet planilha = package.Workbook.Worksheets[0];

                int colunas = planilha.Dimension.End.Column;
                int linhas = planilha.Dimension.End.Row;

                for (int coluna = 1; coluna < colunas; coluna++)
                {
                    if (planilha.Cells[1, coluna].Value.ToString() == nomeColuna)
                    {
                        for (int linha = 2; linha < linhas; linha++)
                        {
                            if (planilha.Cells[linha, coluna].Value != null)
                                servicos.Add(planilha.Cells[linha, coluna].Value.ToString());
                            else
                                break;
                        }
                        coluna = colunas;
                    }
                }
            }
            return servicos;
        }
    }
}
