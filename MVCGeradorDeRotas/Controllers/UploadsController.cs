using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using MVCGeradorDeRotas.Services;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MVCGeradorDeRotas.Controllers
{
    public class UploadsController : Controller
    {
        IWebHostEnvironment _appEnvironment;

        public UploadsController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
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

            ViewBag.User = user;
            ViewBag.Authenticate = authenticate;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnviarArquivo(IFormFile arquivo)
        {
            if (LeituraDeArquivos.ArquivoValido("Plan", ".xlsx", _appEnvironment.WebRootPath))
                RemoverArquivo.RemoverArquivoDoDiretorio("Plan", ".xlsx", _appEnvironment.WebRootPath);

            var caminhoArquivo = Path.GetTempFileName();
            string caminhoWeb = _appEnvironment.WebRootPath;

            if (TipoArquivo.EhArquivoExcel(arquivo))
            {
                if (!await EscreverArquivo.EscreverArquivoNoDiretorio(arquivo, caminhoWeb))
                    return BadRequest("Erro na gravação do arquivo");
            }
            else
            {
                return BadRequest("O arquivo deve ter extensão .xls ou xlsx");
            }

            ViewData["Resultado"] = $"Arquivo enviado ao servidor, tamanho total {arquivo.Length} bytes";

            LeituraDeArquivos.OrdenarExcelCEP("Plan", ".xlsx", _appEnvironment.WebRootPath);

            var cabecalho = LeituraDeArquivos.LeituraCabecalhoArquivoExcel(caminhoWeb);

            List<string> cidades = new List<string>();
            List<string> cidadeNaoRepetidas = new List<string>();

            cabecalho.ForEach(cabecalho =>
            {
                if (cabecalho == "CIDADE")
                {
                    cidades = LeituraDeArquivos.LeituraColunasArquivoExcel(caminhoWeb, cabecalho);
                }
            });

            cidades.ForEach(Cidade =>
            {
                if (!cidadeNaoRepetidas.Contains(Cidade))
                    cidadeNaoRepetidas.Add(Cidade);
            });

            foreach(var cidade in cidadeNaoRepetidas)
			{
                Cidade novaCidade = new Cidade() { Nome = cidade, UF = "SP" };

                await CidadeServices.PostCidade(novaCidade);
			}

            return View(ViewData);
        }
    }
}
