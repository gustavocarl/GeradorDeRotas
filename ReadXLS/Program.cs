using Model;
using ReadXLS.Read;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadXLS
{
    public class Program
    {
        static void Main(string[] args)
        {
            var servicos = CidadesAPI.GetCidadesAPI();

            for (int i = 0; i < 10; i++)
			{
                Console.WriteLine(servicos.ToString());
            }

        }
    }
}
