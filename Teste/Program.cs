using Domain.Commands.Inputs;
using Domain.Commands.OmirBrasil.Results;
using Domain.Entities;
using Infra.AzureTables;
using Infra.WebCrowley;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EDUARDO BALTAZAR FERNANDES -> " + NormalizarNome("EDUARDO BALTAZAR FERNANDES"));
            Console.WriteLine("eduardo baltazar fernandes -> " + NormalizarNome("eduardo baltazar fernandes"));
            Console.WriteLine("EDUARDO de FERNANDES -> " + NormalizarNome("EDUARDO de FERNANDES"));
            Console.WriteLine("EDUARDO De FERNANDES -> " + NormalizarNome("EDUARDO De FERNANDES"));

            Console.ReadKey();
        }

        private static string NormalizarNome(string nome)
        {
            var nomeNormalizado = string.Empty;

            var nomes = nome.Split(' ').ToList();

            nomes.ForEach(x =>
            {
                x = x.ToLower();

                if (x.Length > 2)
                {
                    x = char.ToUpper(x[0]) + x.Substring(1);
                }

                nomeNormalizado += x + " ";
            });

            return nomeNormalizado.Trim();
        }
    }
}
