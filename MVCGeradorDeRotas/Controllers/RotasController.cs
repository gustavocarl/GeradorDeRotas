using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Model;
using MongoDB.Driver;
using MVCGeradorDeRotas.Repository;
using MVCGeradorDeRotas.Services;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVCGeradorDeRotas.Controllers
{
    public class RotasController : Controller
    {

        IWebHostEnvironment _appEnvironment;
        private readonly IRotasConfigurationSettings _rotaServices;

        public RotasController(IWebHostEnvironment appEnvironment, IRotasConfigurationSettings rotasServices)
        {
            _appEnvironment = appEnvironment;
            _rotaServices = rotasServices;
        }

        public IActionResult Index()
        {
            string user = "Anonymous";
            bool authenticate = false;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                user = HttpContext.User.Identity.Name;
                authenticate = true;

                if (HttpContext.User.IsInRole("Admin"))
                    ViewBag.Role = "Admin";
                else
                    ViewBag.Role = "User";
            }
            else
            {
                user = "Não Logado";
                authenticate = false;
                ViewBag.Role = "";
            }

            var diretorio = _appEnvironment.WebRootPath + @"\Arquivo\";
            string[] arquivos = Directory.GetFiles(diretorio);
            bool temPlanilha = false;

            foreach (var arquivo in arquivos)
            {
                var split = arquivo.Split(@"\");
                var nome = split[split.Length - 1];

                if (nome == "Planilha.xlsx")
                    temPlanilha = true;
            }

            if (!temPlanilha)
            {
                return BadRequest("Não foi carregado nenhum arquivo Excel para leitura");
            }

            var cabecalho = LeituraDeArquivos.LeituraCabecalhoArquivoExcel(_appEnvironment.WebRootPath);

            ViewBag.User = user;
            ViewBag.Authenticate = authenticate;
            ViewBag.Cabecalho = cabecalho;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Servicos(List<string> selecionarCabecalhos)
        {

            ViewBag.User = HttpContext.User.Identity.Name;
            ViewBag.Authenticate = true;

            if (HttpContext.User.IsInRole("Admin"))
                ViewBag.Role = "Admin";
            else
                ViewBag.Role = "User";

            if (!selecionarCabecalhos.Contains("OS") ||
                !selecionarCabecalhos.Contains("CIDADE") ||
                !selecionarCabecalhos.Contains("BASE") ||
                !selecionarCabecalhos.Contains("SERVIÇO") ||
                !selecionarCabecalhos.Contains("ENDEREÇO") ||
                !selecionarCabecalhos.Contains("NUMERO") ||
                !selecionarCabecalhos.Contains("COMPLEMENTO") ||
                !selecionarCabecalhos.Contains("CEP") &&
                !selecionarCabecalhos.Contains("BAIRRO"))
            {
                return BadRequest("OS, CIDADE, BASE, SERVIÇO, ENDEREÇO, NUMERO, COMPLEMENTO, CEP E BAIRRO - são campos obrigatórios");
            }

            List<string> servicos = new List<string>();
            List<string> buscarServicos = new List<string>();

            selecionarCabecalhos.ForEach(cabecalhos =>
            {
                if (cabecalhos.ToUpper() == "SERVIÇO" || cabecalhos.ToUpper() == "SERVICO")
                {
                    servicos = LeituraDeArquivos.LeituraColunasArquivoExcel(_appEnvironment.WebRootPath, cabecalhos);
                }
            });

            servicos.Sort((x, y) => x.CompareTo(y));

            var servicosDuplicados = servicos
                .GroupBy(servico => servico.ToString())
                .Where(x => x.Count() > 1);

            foreach (var servico in servicosDuplicados)
            {
                buscarServicos.Add(servico.Key);
            }

            var lerArquivoExcel = LeituraDeArquivos.LerArquivoExcel(selecionarCabecalhos, _appEnvironment.WebRootPath);

            EscreverArquivo.EscreverStringNoDiretorio(selecionarCabecalhos, null, "cabecalho", _appEnvironment.WebRootPath);

            ViewBag.Servicos = buscarServicos;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cidades(string servico)
        {

            ViewBag.User = HttpContext.User.Identity.Name;
            ViewBag.Authenticate = true;

            if (HttpContext.User.IsInRole("Admin"))
                ViewBag.Role = "Admin";
            else
                ViewBag.Role = "User";

            var colunas = LeituraDeArquivos.LerArquivoNoDiretorio("cabecalho", _appEnvironment.WebRootPath);
            var selecionarCabecalhos = LeituraDeArquivos.LerArquivoExcel(colunas, _appEnvironment.WebRootPath);

            List<string> listarCidades = new List<string>();
            List<IDictionary<string, string>> dicionarioServico = new List<IDictionary<string, string>>();

            colunas.ForEach(coluna =>
            {
                if (coluna.ToUpper() == "CIDADE")
                {
                    for (int i = 0; i < selecionarCabecalhos.Count; i++)
                    {
                        dicionarioServico = selecionarCabecalhos
                        .Where(data => data.ContainsKey(coluna))
                        .Where(data => data.Values.Contains(servico))
                        .ToList();
                    }
                }
            });

            dicionarioServico.ForEach(dicio =>
            {
                listarCidades.Add(dicio["CIDADE"]);
            });

            listarCidades = listarCidades.Distinct().ToList();

            ViewBag.Cidades = listarCidades;
            ViewBag.Servico = servico;

            EscreverArquivo.EscreverStringNoDiretorio(null, servico, "servico", _appEnvironment.WebRootPath);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Equipes(string cidade)
        {
            ViewBag.User = HttpContext.User.Identity.Name;
            ViewBag.Authenticate = true;

            if (HttpContext.User.IsInRole("Admin"))
                ViewBag.Role = "Admin";
            else
                ViewBag.Role = "User";

            var servico = LeituraDeArquivos.LerArquivoStringNoDiretorio("servico", _appEnvironment.WebRootPath);
            var colunas = LeituraDeArquivos.LerArquivoNoDiretorio("cabecalho", _appEnvironment.WebRootPath);
            var selecionarCabecalhos = LeituraDeArquivos.LerArquivoExcel(colunas, _appEnvironment.WebRootPath);

            List<string> cidades = new List<string>();
            List<IDictionary<string, string>> dicionarioServicoECidade = new List<IDictionary<string, string>>();

            colunas.ForEach(coluna =>
            {
                if (coluna.ToUpper() == "SERVIÇO")
                {
                    for (int i = 0; i < selecionarCabecalhos.Count; i++)
                    {
                        dicionarioServicoECidade = selecionarCabecalhos
                        .Where(data => data.ContainsKey(coluna))
                        .Where(data => data.Values.Contains(servico))
                        .Where(data => data.Values.Contains(cidade))
                        .ToList();
                    }
                }
            });

            var quantidadeServico = dicionarioServicoECidade.Count;

            var listarEquipes = await EquipeServices.Get();

            List<Equipe> listarEquipesSelecionadas = new();

            listarEquipes.ForEach(equipe =>
            {
                if (equipe.Cidade.Nome == cidade)
                {
                    listarEquipesSelecionadas.Add(equipe);
                }
            });

            if (listarEquipes == null)
                return BadRequest("Não disponível");

            EscreverArquivo.EscreverStringNoDiretorio(null, cidade, "cidade", _appEnvironment.WebRootPath);

            ViewBag.Cidades = cidade;
            ViewBag.QuantidadeServico = quantidadeServico;

            return View(listarEquipesSelecionadas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GerarRotas(List<string> selecionarEquipes)
        {
            ViewBag.User = HttpContext.User.Identity.Name;
            ViewBag.Authenticate = true;

            if (HttpContext.User.IsInRole("Admin"))
                ViewBag.Role = "Admin";
            else
                ViewBag.Role = "User";

            if (selecionarEquipes.Count < 1)
                return BadRequest("Pelo menos uma equipe deve ser selecionada");

            List<Equipe> listarEquipesPorNome = new List<Equipe>();

            listarEquipesPorNome = await EquipeServices.Get();

            var servico = LeituraDeArquivos.LerArquivoStringNoDiretorio("servico", _appEnvironment.WebRootPath);
            var cidade = LeituraDeArquivos.LerArquivoStringNoDiretorio("cidade", _appEnvironment.WebRootPath);
            var colunas = LeituraDeArquivos.LerArquivoNoDiretorio("cabecalho", _appEnvironment.WebRootPath);
            var selecionarCabecalhos = LeituraDeArquivos.LerArquivoExcel(colunas, _appEnvironment.WebRootPath);

            List<string> listarEquipesSelecionadas = new List<string>();
            List<IDictionary<string, string>> dicionarioServicosECidades = new();

            colunas.ForEach(coluna =>
            {
                if (coluna.ToUpper() == "SERVIÇO")
                {
                    for (int i = 0; i < selecionarCabecalhos.Count; i++)
                    {
                        dicionarioServicosECidades = selecionarCabecalhos
                        .Where(data => data.ContainsKey(coluna))
                        .Where(data => data.Values.Contains(servico))
                        .Where(data => data.Values.Contains(cidade))
                        .ToList();
                    }
                }
            });

            listarEquipesPorNome.ForEach(equipe =>
            {
                if (equipe.Cidade.Nome == cidade)
                {
                    listarEquipesSelecionadas.Add(equipe.Nome);
                }
            });


            List<string> outrasColunas = new List<string>();

            colunas.ForEach(coluna =>
            {
                if (coluna != "OS"
                && coluna != "CIDADE"
                && coluna != "BASE"
                && coluna != "SERVIÇO"
                && coluna != "ENDEREÇO"
                && coluna != "NUMERO"
                && coluna != "COMPLEMENTO"
                && coluna != "CEP"
                && coluna != "BAIRRO"
                )
                {
                    outrasColunas.Add(coluna);
                }
            });

            //if ((dicionarioServicosECidades.Count / selecionarEquipes.Count) > 5)
            //{
            //    return BadRequest("A quantidade máxima de serviços por equipe é 5");
            //}


            /* Begin Header DOC */

            Document document = new();

            Section section = document.AddSection();

            Paragraph emptyParagraph = section.AddParagraph();
            emptyParagraph.AppendText(" ");
            emptyParagraph = section.AddParagraph();
            emptyParagraph.AppendText(" ");

            Paragraph paragraphTitle = section.AddParagraph();
            TextRange trTitle = paragraphTitle.AppendText($"ROTA DE TRABALHO - {DateTime.Now.Date.ToString("dd/MM/yyyy")}");
            trTitle.CharacterFormat.FontSize = 23;
            trTitle.CharacterFormat.Bold = true;
            trTitle.CharacterFormat.FontName = "Arial";
            paragraphTitle.Format.HorizontalAlignment = HorizontalAlignment.Center;
            paragraphTitle.Format.LineSpacing = 15;

            Paragraph paragraphDate = section.AddParagraph();
            TextRange trDate = paragraphDate.AppendText($"{servico}");
            trDate.CharacterFormat.FontSize = 17;
            trDate.CharacterFormat.Bold = true;
            trDate.CharacterFormat.FontName = "Arial";
            paragraphDate.Format.HorizontalAlignment = HorizontalAlignment.Center;

            emptyParagraph = section.AddParagraph();
            emptyParagraph.AppendText(" ");
            emptyParagraph = section.AddParagraph();
            emptyParagraph.AppendText(" ");

            /* End Header DOC */

            /* Begin Equip */

            if (listarEquipesPorNome.Count < 2)
            {
                Paragraph paragraphEquip = section.AddParagraph();
                TextRange trEquip = paragraphEquip.AppendText($"Nome da Equipe: {listarEquipesPorNome[0].Nome}");
                trEquip.CharacterFormat.FontSize = 14;
                trEquip.CharacterFormat.Bold = true;
                trEquip.CharacterFormat.FontName = "Arial";

                emptyParagraph = section.AddParagraph();
                emptyParagraph.AppendText(" ");

                dicionarioServicosECidades.ForEach(dictionary =>
                {
                    Paragraph paragraph1 = section.AddParagraph();
                    TextRange tr1 = paragraph1.AppendText($"Contrato: {(dictionary.ContainsKey("CONTRATO") ? dictionary["CONTRATO"] : "                     ")} - Assinante: {(dictionary.ContainsKey("ASSINANTE") ? dictionary["ASSINANTE"] : "                            ")} - Período:     :     /     :     .");
                    tr1.CharacterFormat.FontSize = 12;
                    tr1.CharacterFormat.Bold = true;
                    tr1.CharacterFormat.FontName = "Arial";
                    tr1.CharacterFormat.UnderlineStyle = UnderlineStyle.Single;
                    paragraph1.Format.LineSpacing = 15;

                    Paragraph paragraph2 = section.AddParagraph();
                    TextRange tr2 = paragraph2.AppendText($"Endereço: {dictionary["ENDEREÇO"]}, {dictionary["NUMERO"]}, {dictionary["BAIRRO"]}, {dictionary["CIDADE"]}, CEP: {dictionary["CEP"]}  - {(dictionary.ContainsKey("TELEFONE 1") ? dictionary["TELEFONE 1"] : "")}");
                    tr2.CharacterFormat.FontName = "Arial";
                    paragraph2.Format.LineSpacing = 15;

                    Paragraph paragraph3 = section.AddParagraph();
                    TextRange tr3 = paragraph3.AppendText($"O.S.:{dictionary["OS"]} - ");
                    tr3.CharacterFormat.FontName = "Arial";

                    tr3 = paragraph3.AppendText($"Tipo OS: {(dictionary.ContainsKey("TIPO OS") ? dictionary["TIPO OS"] : "_______________")}");
                    tr3.CharacterFormat.TextColor = Color.White;
                    //tr3.CharacterFormat.TextBackgroundColor = Color.Red;
                    tr3.CharacterFormat.FontName = "Arial";
                    paragraph3.Format.LineSpacing = 15;

                    if (outrasColunas.Count > 0)
                    {
                        for (int col = 0; col < outrasColunas.Count; col++)
                        {
                            Paragraph paragraph4 = section.AddParagraph();
                            TextRange tr4 = paragraph4.AppendText($"{outrasColunas[col]}: {dictionary[outrasColunas[col]]}");
                            tr4.CharacterFormat.FontName = "Arial";
                            paragraph4.Format.LineSpacing = 15;
                        }
                    }

                    emptyParagraph = section.AddParagraph();
                    emptyParagraph.AppendText(" ");
                });
            }
            else if (listarEquipesPorNome.Count == dicionarioServicosECidades.Count)
            {
                var i = 0;

                dicionarioServicosECidades.ForEach(dictionary =>
                {
                    Paragraph paragraphEquip = section.AddParagraph();
                    TextRange trEquip = paragraphEquip.AppendText($"Nome da Equipe: {listarEquipesPorNome[i].Nome}");
                    trEquip.CharacterFormat.FontSize = 14;
                    trEquip.CharacterFormat.Bold = true;
                    trEquip.CharacterFormat.FontName = "Arial";

                    emptyParagraph = section.AddParagraph();
                    emptyParagraph.AppendText(" ");

                    Paragraph paragraph1 = section.AddParagraph();
                    TextRange tr1 = paragraph1.AppendText($"Contrato: {(dictionary.ContainsKey("CONTRATO") ? dictionary["CONTRATO"] : "                     ")} - Assinante: {(dictionary.ContainsKey("ASSINANTE") ? dictionary["ASSINANTE"] : "                            ")} - Período:     :     /     :     .");
                    tr1.CharacterFormat.FontSize = 12;
                    tr1.CharacterFormat.Bold = true;
                    tr1.CharacterFormat.FontName = "Arial";
                    tr1.CharacterFormat.UnderlineStyle = UnderlineStyle.Single;
                    paragraph1.Format.LineSpacing = 15;

                    Paragraph paragraph2 = section.AddParagraph();
                    TextRange tr2 = paragraph2.AppendText($"Endereço: {dictionary["ENDEREÇO"]}, {dictionary["NUMERO"]}, {dictionary["BAIRRO"]}, {dictionary["CIDADE"]}, CEP: {dictionary["CEP"]}  - {(dictionary.ContainsKey("TELEFONE 1") ? dictionary["TELEFONE 1"] : "")}");
                    tr2.CharacterFormat.FontName = "Arial";
                    paragraph2.Format.LineSpacing = 15;

                    Paragraph paragraph3 = section.AddParagraph();
                    TextRange tr3 = paragraph3.AppendText($"O.S.:{dictionary["OS"]} - ");
                    tr3.CharacterFormat.FontName = "Arial";

                    tr3 = paragraph3.AppendText($"Tipo OS: {(dictionary.ContainsKey("TIPO OS") ? dictionary["TIPO OS"] : "_______________")}");
                    tr3.CharacterFormat.TextColor = Color.White;
                    //tr3.CharacterFormat.TextBackgroundColor = Color.Red;
                    tr3.CharacterFormat.FontName = "Arial";
                    paragraph3.Format.LineSpacing = 15;

                    if (outrasColunas.Count > 0)
                    {
                        for (int col = 0; col < outrasColunas.Count; col++)
                        {
                            Paragraph paragraph4 = section.AddParagraph();
                            TextRange tr4 = paragraph4.AppendText($"{outrasColunas[col]}: {dictionary[outrasColunas[col]]}");
                            tr4.CharacterFormat.FontName = "Arial";
                            paragraph4.Format.LineSpacing = 15;
                        }
                    }

                    emptyParagraph = section.AddParagraph();
                    emptyParagraph.AppendText(" ");

                    i++;
                });
            }
            else if (listarEquipesPorNome.Count > 1)
            {
                int serviceSplit = listarEquipesSelecionadas.Count / listarEquipesSelecionadas.Count;
                decimal restSplit = dicionarioServicosECidades.Count % listarEquipesSelecionadas.Count;
                int listEquip = 0;

                if (restSplit > 5)
                {
                    serviceSplit = 5;
                    restSplit = Math.Ceiling((decimal)dicionarioServicosECidades.Count % 5);
                }

                for (int k = 0; k < dicionarioServicosECidades.Count; k++)
                {

                    emptyParagraph = section.AddParagraph();
                    emptyParagraph.AppendText(" ");

                    listEquip++;

                    for (int i = 0; i < serviceSplit; i++)
                    {
                        var dictionary = dicionarioServicosECidades[k];

                        Paragraph paragraph1 = section.AddParagraph();
                        TextRange tr1 = paragraph1.AppendText($"Contrato: {(dictionary.ContainsKey("CONTRATO") ? dictionary["CONTRATO"] : "                     ")} - Assinante: {(dictionary.ContainsKey("ASSINANTE") ? dictionary["ASSINANTE"] : "                            ")} - Período:     :     /     :     .");
                        tr1.CharacterFormat.FontSize = 12;
                        tr1.CharacterFormat.Bold = true;
                        tr1.CharacterFormat.FontName = "Arial";
                        tr1.CharacterFormat.UnderlineStyle = UnderlineStyle.Single;
                        paragraph1.Format.LineSpacing = 15;

                        Paragraph paragraph2 = section.AddParagraph();
                        TextRange tr2 = paragraph2.AppendText($"Endereço: {dictionary["ENDEREÇO"]}, {dictionary["NUMERO"]}, {dictionary["BAIRRO"]}, {dictionary["CIDADE"]}, CEP: {dictionary["CEP"]}  - {(dictionary.ContainsKey("TELEFONE 1") ? dictionary["TELEFONE 1"] : "")}");
                        tr2.CharacterFormat.FontName = "Arial";
                        paragraph2.Format.LineSpacing = 15;

                        Paragraph paragraph3 = section.AddParagraph();
                        TextRange tr3 = paragraph3.AppendText($"O.S.:{dictionary["OS"]} - ");
                        tr3.CharacterFormat.FontName = "Arial";

                        tr3 = paragraph3.AppendText($"Tipo OS: {(dictionary.ContainsKey("TIPO OS") ? dictionary["TIPO OS"] : "_______________")}");
                        tr3.CharacterFormat.TextColor = Color.White;
                        //tr3.CharacterFormat.TextBackgroundColor = Color.Red;
                        tr3.CharacterFormat.FontName = "Arial";
                        paragraph3.Format.LineSpacing = 15;

                        if (outrasColunas.Count > 0)
                        {
                            for (int col = 0; col < outrasColunas.Count; col++)
                            {
                                Paragraph paragraph4 = section.AddParagraph();
                                TextRange tr4 = paragraph4.AppendText($"{outrasColunas[col]}: {dictionary[outrasColunas[col]]}");
                                tr4.CharacterFormat.FontName = "Arial";
                                paragraph4.Format.LineSpacing = 15;
                            }
                        }

                        emptyParagraph = section.AddParagraph();
                        emptyParagraph.AppendText(" ");

                        k++;
                    }

                    k--;

                    if (restSplit == dicionarioServicosECidades.Count - (k + 1) && restSplit != 0)
                    {
                        //k++;

                        Paragraph paragraphEquipRest = section.AddParagraph();
                        TextRange trEquipRest = paragraphEquipRest.AppendText($"Nome da Equipe: {listarEquipesPorNome[listEquip].Nome}");
                        trEquipRest.CharacterFormat.FontSize = 14;
                        trEquipRest.CharacterFormat.Bold = true;
                        trEquipRest.CharacterFormat.FontName = "Arial";

                        emptyParagraph = section.AddParagraph();
                        emptyParagraph.AppendText(" ");


                        for (int i = 0; i < restSplit; i++)
                        {
                            var dictionary = dicionarioServicosECidades[k];

                            Paragraph paragraph1 = section.AddParagraph();
                            TextRange tr1 = paragraph1.AppendText($"Contrato: {(dictionary.ContainsKey("CONTRATO") ? dictionary["CONTRATO"] : "                     ")} - Assinante: {(dictionary.ContainsKey("ASSINANTE") ? dictionary["ASSINANTE"] : "                            ")} - Período:     :     /     :     .");
                            tr1.CharacterFormat.FontSize = 12;
                            tr1.CharacterFormat.Bold = true;
                            tr1.CharacterFormat.FontName = "Arial";
                            tr1.CharacterFormat.UnderlineStyle = UnderlineStyle.Single;
                            paragraph1.Format.LineSpacing = 15;

                            Paragraph paragraph2 = section.AddParagraph();
                            TextRange tr2 = paragraph2.AppendText($"Endereço: {dictionary["ENDEREÇO"]}, {dictionary["NUMERO"]}, {dictionary["BAIRRO"]}, {dictionary["CIDADE"]}, CEP: {dictionary["CEP"]}  - {(dictionary.ContainsKey("TELEFONE 1") ? dictionary["TELEFONE 1"] : "")}");
                            tr2.CharacterFormat.FontName = "Arial";
                            paragraph2.Format.LineSpacing = 15;

                            Paragraph paragraph3 = section.AddParagraph();
                            TextRange tr3 = paragraph3.AppendText($"O.S.:{dictionary["OS"]} - ");
                            tr3.CharacterFormat.FontName = "Arial";

                            tr3 = paragraph3.AppendText($"Tipo OS: {(dictionary.ContainsKey("TIPO OS") ? dictionary["TIPO OS"] : "_______________")}");
                            tr3.CharacterFormat.TextColor = Color.White;
                            //tr3.CharacterFormat.TextBackgroundColor = Color.Red;
                            tr3.CharacterFormat.FontName = "Arial";
                            paragraph3.Format.LineSpacing = 15;

                            if (outrasColunas.Count > 0)
                            {
                                for (int col = 0; col < outrasColunas.Count; col++)
                                {
                                    Paragraph paragraph4 = section.AddParagraph();
                                    TextRange tr4 = paragraph4.AppendText($"{outrasColunas[col]}: {dictionary[outrasColunas[col]]}");
                                    tr4.CharacterFormat.FontName = "Arial";
                                    paragraph4.Format.LineSpacing = 15;
                                }
                            }

                            emptyParagraph = section.AddParagraph();
                            emptyParagraph.AppendText(" ");

                            k++;
                        }
                    }
                }
            }

            servico = servico.Replace("Ç", "C").Replace("ç", "c").Replace("Ã", "A").Replace("ã", "a").Replace("É", "e").Replace("é", "e").Replace(" ", "");
            cidade = cidade.Replace("Ç", "C").Replace("ç", "c").Replace("Ã", "A").Replace("ã", "a").Replace("É", "e").Replace("é", "e").Replace(" ", "");

            string nameFile = $"Rota - {servico} - {cidade} - {DateTime.Now.Date.ToString("ddMMyyyy")}.docx";

            document.SaveToFile(_appEnvironment.WebRootPath + @"\Arquivo\" + nameFile, FileFormat.Docx);

            /* FIM DOC*/

            RemoverArquivo.RemoverArquivoDoDiretorio("cabecalho", ".txt", _appEnvironment.WebRootPath);
            RemoverArquivo.RemoverArquivoDoDiretorio("servico", ".txt", _appEnvironment.WebRootPath);
            RemoverArquivo.RemoverArquivoDoDiretorio("cidade", ".txt", _appEnvironment.WebRootPath);

            return View();
        }

    }
}
