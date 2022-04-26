using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;
using System.IO;

namespace MVCGeradorDeRotas.Controllers
{
    public class DownloadsController : Controller
    {
        IWebHostEnvironment _appEnvironment;

        public DownloadsController(IWebHostEnvironment appEnvironment)
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
                    ViewBag.Role = "Admin";
            }
            else
            {
                user = "Não Logado";
                authenticate = false;
                ViewBag.Role = "";
            }

            ViewBag.User = user;
            ViewBag.Authenticate = authenticate;

            var diretorio = _appEnvironment.WebRootPath + @"\Arquivo\";
            string[] arquivos = Directory.GetFiles(diretorio);

            List<Rotas> rotas = new List<Rotas>();

            foreach (var arquivo in arquivos)
            {
                var split = arquivo.Split(@"\");
                var nome = split[split.Length - 1];

                var extensao = nome.Split(".")[1];

                if (extensao == "docx")
                    rotas.Add(new Rotas { NomeDoArquivo = nome, CaminhoCompleto = arquivo });

            }

            return View(rotas);
        }

        public FileResult DownloadArquivo(string nomeDoArquivo)
        {
            var diretorio = _appEnvironment.WebRootPath + @"\Arquivo\";

            var caminhoFinal = diretorio + nomeDoArquivo;
            byte[] bytes = System.IO.File.ReadAllBytes(caminhoFinal);

            string tipoConteudo = "application/octet-stream";

            return File(bytes, tipoConteudo, nomeDoArquivo);

        }
    }
}