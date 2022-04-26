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

        public static List<Cidade> LerArquivoTxtCidades(string caminhoWeb)
        {
            List<Cidade> cidades = new List<Cidade>();

            string diretorio = @"\Arquivo\";

            string caminho = caminhoWeb + diretorio + "municipios.txt";

            using (StreamReader leitura = new StreamReader(caminho))
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

        public static List<IDictionary<string, string>> LerArquivoExcel(List<string> columns, string pathWebRoot)
        {
            List<string> plan = new();
            string folder = @"\Arquivo\";

            List<IDictionary<string, string>> listDictonary = new();

            FileInfo excelFile = new FileInfo(pathWebRoot + folder + "Planilha.xlsx");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                int cols = worksheet.Dimension.End.Column;
                int rows = worksheet.Dimension.End.Row;

                IDictionary<string, string> data = new Dictionary<string, string>();

                for (int row = 2; row < rows; row++)
                {
                    data = new Dictionary<string, string>();

                    for (int col = 1; col < cols; col++)
                    {
                        columns.ForEach(column =>
                        {
                            if (worksheet.Cells[1, col].Value.ToString() == column)
                            {
                                if (worksheet.Cells[row, col].Value == null)
                                    data.Add(column, "");
                                else
                                    data.Add(column, worksheet.Cells[row, col].Value.ToString());
                            }
                        });

                    }
                    if (data.Count > 1) listDictonary.Add(data);
                }
            }

            return listDictonary;
        }

        public static List<string> LerArquivoNoDiretorio(string titulo, string caminhoWeb)
        {
            List<string> texto = new List<string>();

            string arquivo = titulo + ".txt";
            string diretorio = @"\Arquivo\";
            string caminhoFinal = caminhoWeb + diretorio + arquivo;

            using (StreamReader leitura = new StreamReader(caminhoFinal))
            {
                var linha = leitura.ReadLine();
                while(linha != null)
                {
                    texto.Add(linha);
                    linha = leitura.ReadLine();
                }
            }
            return texto;
        }

        public static string LerArquivoStringNoDiretorio(string titulo, string caminhoWeb)
        {
            string texto = "";

            string arquivo = titulo + ".txt";
            string diretorio = @"\Arquivo\";
            string caminhoFinal = caminhoWeb + diretorio + arquivo;

            using(StreamReader leitura = new StreamReader(caminhoFinal))
            {
                var linha = leitura.ReadLine();
                texto = linha;
            }
            return texto;

        }

        public static string RetornoDoServico(string titulo, string servico, string caminhoWeb)
        {
            string resultado = "";

            string arquivo = titulo + ".txt";
            string diretorio = @"\Arquivo\";
            string caminhoFinal = caminhoWeb + diretorio + arquivo;

            using (StreamReader leitura = new StreamReader(caminhoFinal))
            {
                var linha = leitura.ReadLine();
                while(linha != null)
                {
                    var separacao = linha.Split(";");
                    for(int i = 0; i < separacao.Length; i++)
                    {
                        if(separacao[i] == servico)
                        {
                            resultado = resultado + linha + "\n";
                        }
                    }
                    linha = leitura.ReadLine();
                }
            }
            return resultado;
        }

    }
}
