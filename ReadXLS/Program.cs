using Model;
using ReadXLS.Read;
using System;
using System.Collections.Generic;

namespace ReadXLS
{
    public class Program
    {
        static void Main(string[] args)
        {
            var servicos = ReadAndWriteFile.ReadXls();

            foreach(var item in servicos)
            {
                Console.WriteLine($"\nOS: {item.OS} " +
                    $"\nCidade: {item.Cidade} " +
                    $"\nBase: {item.Base} " +
                    $"\nServiço: {item.Servico} " +
                    $"\nEndereço: {item.Endereco} " +
                    $"\nNúmero: {item.Numero} " +
                    $"\nComplemento: {item.Complemento} " +
                    $"\nCEP: {item.CEP} " +
                    $"\nBairro: {item.Bairro} ");
            }
        }
    }
}
