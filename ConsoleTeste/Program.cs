﻿using AngleSharp.Parser.Html;
using Domain.Commands.Handlers;
using Domain.Commands.Inputs;
using Infra;
using Infra.Repositories;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://www.omirbrasil.org.br/");
                driver.ExecuteScript("TrocaInclude('Sistema_OmirBrasil');");

                var numeroDistritos = ExtrairNumeroDeTodosOsDistritos(driver.PageSource);

                numeroDistritos.ForEach(numeroDistrito =>
                {
                    driver.ExecuteScript($"AbreFichaDistrito('{numeroDistrito}');");

                    var distritoInput = ExtratirDadosDistrito(driver.PageSource, numeroDistrito);
                    //persistir no bd

                    var codigoDosClubes = ExtrairCodigoDosClubesDoDistrito(driver.PageSource);

                    codigoDosClubes.ForEach(codigoClube =>
                    {
                        driver.ExecuteScript($"javascript:AbreFichaClube('{codigoClube}');");

                        var clubeInput = ExtratirDadosClube(driver.PageSource, Convert.ToInt32(codigoClube), numeroDistrito);


                    });
                });

                driver.Close();
                driver.Dispose();
            }
        }

        private static List<string> ExtrairNumeroDeTodosOsDistritos(string html)
        {
            var paginaDistrito = new HtmlParser().Parse(html);

            var listaDistritosHtml = paginaDistrito.QuerySelectorAll("#accordion tr")
                .Where(x => x.OuterHtml.Contains("javascript:AbreFichaDistrito"))
                .ToList();

            var distritos = new List<CriarDistritoInput>();

            return listaDistritosHtml
                .Select(x => x.QuerySelectorAll("strong").FirstOrDefault(a => a.InnerHtml.Contains("D.")).TextContent.Trim().Replace("D.", string.Empty))
                .ToList();
        }

        private static CriarDistritoInput ExtratirDadosDistrito(string htmlTexto, string numeroDistrito)
        {
            var html = new HtmlParser().Parse(htmlTexto);
            var htmlDadosDistrito = html.QuerySelector("#FichaSocio").TextContent;

            return new CriarDistritoInput
            {
                Numero = numeroDistrito,

                Mascote = htmlDadosDistrito.Split('\n')
                    .FirstOrDefault(x => x.Contains("Mascote:")).Replace("Mascote:", "").Trim(),

                Regiao = RomanoParaInteiro(htmlDadosDistrito.Split('\n')
                    .FirstOrDefault(x => x.Contains("Região:"))
                    .Replace("Região:", "").Trim()),

                Site = htmlDadosDistrito.Split('\n')
                    .FirstOrDefault(x => x.Contains("Site:")).Replace("Site:", "").Trim().ToLower(),

                Email = htmlDadosDistrito.Split('\n')
                    .FirstOrDefault(x => x.Contains("E-mail:")).Replace("E-mail:", "").Trim().ToLower()
            };
        }

        private static CriarClubeInput ExtratirDadosClube(string htmlTexto, int codigoClube, string numeroDistrito)
        {
            var html = new HtmlParser().Parse(htmlTexto);
            var htmlDadosClube = html.QuerySelector("#FichaSocio").TextContent;
            var htmlDadosPrincipaisClube = html.QuerySelectorAll("#Dados_Principais tr");

            var retorno = new CriarClubeInput
            {
                Codigo = codigoClube,
                numeroDistrito = numeroDistrito,
                DataFundacao = Convert.ToDateTime(htmlDadosClube.Split('\n')
                    .FirstOrDefault(x => x.Contains("Data de Fundação:")).Replace("Data de Fundação:", "").Trim()),
                Nome = htmlDadosClube.Substring(0, htmlDadosClube.IndexOf("D.")).Replace("\n", "").Trim(),
                RotaryPadrinho = htmlDadosClube.Split('\n')
                    .FirstOrDefault(x => x.Contains("R.C Padrinho:")).Replace("R.C Padrinho:", "").Trim(),
                Site = htmlDadosPrincipaisClube.FirstOrDefault(x => x.TextContent.Contains("Site")).TextContent.Replace("\n", "").Replace("Site", "").Trim(),
                Email = htmlDadosPrincipaisClube.FirstOrDefault(x => x.TextContent.Contains("E-mail")).TextContent.Replace("\n", "").Replace("E-mail", "").Trim(),
                Facebook = htmlDadosPrincipaisClube.FirstOrDefault(x => x.TextContent.Contains("Facebook")).TextContent.Replace("\n", "").Replace("Facebook", "").Trim()
            };

            if (htmlDadosClube.Split('\n').FirstOrDefault(x => x.Contains("Data de Fechamento:")) != null)
            {
                retorno.DataFechamento = Convert.ToDateTime(htmlDadosClube.Split('\n')
                    .FirstOrDefault(x => x.Contains("Data de Fechamento:")).Replace("Data de Fechamento:", "").Trim());
            }

            return retorno;
        }

        private static List<string> ExtrairCodigoDosClubesDoDistrito(string htmlTexto)
        {
            var html = new HtmlParser().Parse(htmlTexto);

            var codigoClubesAtivos = html
                        .QuerySelectorAll("#Guia_Clubes tr")
                        .Where(x => x.OuterHtml.Contains("javascript:AbreFichaClube"))
                        .Select(x =>
                        {
                            var texto = x.OuterHtml.Substring(x.OuterHtml.IndexOf("javascript:AbreFichaClube('"));

                            texto = texto.Replace("javascript:AbreFichaClube('", "");
                            texto = texto.Substring(0, texto.IndexOf("')"));

                            return texto;
                        });

            var codigoClubesInativos = html
                .QuerySelectorAll("#Guia_ClubesInativos tr")
                .Where(x => x.OuterHtml.Contains("javascript:AbreFichaClube"))
                .Select(x =>
                {
                    var texto = x.OuterHtml.Substring(x.OuterHtml.IndexOf("javascript:AbreFichaClube('"));

                    texto = texto.Replace("javascript:AbreFichaClube('", "");
                    texto = texto.Substring(0, texto.IndexOf("')"));

                    return texto;
                });

            var codigoClubes = new List<string>();

            codigoClubes.AddRange(codigoClubesAtivos);
            codigoClubes.AddRange(codigoClubesInativos);

            return codigoClubes;
        }

        private static int RomanoParaInteiro(string numeroRomano)
        {
            switch (numeroRomano.ToUpper())
            {
                case "I": return 1; 
                case "II": return 2;
                case "III": return 3;
                case "IV": return 4; 
                case "V": return 5;
                case "VI": return 6; 
                case "VII": return 7;
                case "VIII": return 8;
                case "IX": return 9;
                case "X": return 10;
                case "XI": return 11;
                default: return 0;
            }
        }
    }
}
