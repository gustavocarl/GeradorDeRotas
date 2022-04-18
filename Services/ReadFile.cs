﻿using Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ReadFile
    {

        public static List<Rotas> ReadXls()
        {
            var response = new List<Rotas>();
            bool pularPrimeiraLinha = false;

            string path = @"C:\Xls\GeradorDeRotas.xlsx";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(path))
            {
                ExcelWorksheet planilha = package.Workbook.Worksheets[0];

                int colCount = planilha.Dimension.End.Column;
                int rowCount = planilha.Dimension.End.Row;

                for (int row = 1; row <= 11; row++)
                {
                    if (pularPrimeiraLinha)
                    {

                        var rota = new Rotas();
                        rota.OS = planilha.Cells[row, 10].Value.ToString();
                        rota.Cidade = planilha.Cells[row, 19].Value.ToString();
                        rota.Base = planilha.Cells[row, 20].Value.ToString();
                        rota.Servico = planilha.Cells[row, 23].Value.ToString();
                        rota.Endereco = planilha.Cells[row, 27].Value.ToString();
                        rota.Numero = planilha.Cells[row, 28].Value.ToString();
                        rota.Complemento = planilha.Cells[row, 29].Value.ToString();
                        rota.CEP = planilha.Cells[row, 30].Value.ToString();
                        rota.Bairro = planilha.Cells[row, 32].Value.ToString();
                        response.Add(rota);

                    }
                    pularPrimeiraLinha = true;
                }

                var dataAtual = DateTime.Now.ToString("dd-MM-yyyy");
                var file = new StreamWriter(@"C:\Doc\Ordem de Servico " + dataAtual + ".doc");

                file.WriteLine("Nome, email\n\n");

                foreach (var item in response)
                {
                    var linha = $"OS: {item.OS}, Base: {item.Base}" +
                        $"\nCEP: {item.CEP}" +
                        $"\nEndereço: {item.Endereco} Nº: {item.Numero}" +
                        $"\nBairro: {item.Bairro} Complemento: {item.Complemento}" +
                        $"\nServiço: {item.Servico}" +
                        $"\n\n";
                    file.WriteLine(linha);
                }
                file.Close();
            }
            return response;
        }
    }
}