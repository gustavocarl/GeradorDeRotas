using Aspose.Words;
using Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace ReadXLS.Read
{
    public class ReadAndWriteFile
    {
        public static List<Rotas> ReadXls()
        {
            var response = new List<Rotas>();

            FileInfo existingFile = new FileInfo(@"C:\Xls\GeradorDeRotas.xlsx");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet planilha = package.Workbook.Worksheets[0];

                int colCount = planilha.Dimension.End.Column;
                int rowCount = planilha.Dimension.End.Row;

                for (int row = 1; row <= 11; row++)
                {
                    //var rota = new Rotas();
                    //rota.OS = planilha.Cells[row, 10].Value.ToString();
                    //rota.Cidade = planilha.Cells[row, 19].Value.ToString();
                    //rota.Base = planilha.Cells[row, 20].Value.ToString();
                    //rota.Servico = planilha.Cells[row, 23].Value.ToString();
                    //rota.Endereco = planilha.Cells[row, 27].Value.ToString();
                    //rota.Numero = planilha.Cells[row, 28].Value.ToString();
                    //rota.Complemento = planilha.Cells[row, 29].Value.ToString();
                    //rota.CEP = planilha.Cells[row, 30].Value.ToString();
                    //rota.Bairro = planilha.Cells[row, 32].Value.ToString();
                    //response.Add(rota);
                }

                Document file = new Document();
                DocumentBuilder builder = new DocumentBuilder(file);

                Font font = builder.Font;
                font.Size = 14;
                font.Bold = true;
                font.Color = System.Drawing.Color.Black;
                font.Name = "Segoe UI";
                font.Underline = Underline.Single;

                builder.Writeln("ROTA DE TRABALHO - " + DateTime.Now);
                builder.Writeln("\n\n");

                foreach (var item in response)
                {
                    //var linha = $"OS: {item.OS}, Base: {item.Base}" +
                    //    $"\nCEP: {item.CEP}" +
                    //    $"\nEndereço: {item.Endereco} Nº: {item.Numero}" +
                    //    $"\nBairro: {item.Bairro} Complemento: {item.Complemento}" +
                    //    $"\nServiço: {item.Servico}" +
                    //    $"\n\n";
                    font.Size = 9;
                    font.Bold = false;
                    //builder.Writeln(linha);
                }

                file.Save(@"C:\Doc\Ordem de Servico.docx");

            }
            return response;
        }
    }
}
