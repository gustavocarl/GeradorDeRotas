using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MVCGeradorDeRotas.Services;
using System.Collections.Generic;
using System.Linq;

namespace MVCGeradorDeRotas.Controllers
{
    public class RotasController : Controller
    {

        IWebHostEnvironment _appEnvironment;
        private readonly RotaServices _rotaServices;

        public RotasController(IWebHostEnvironment appEnvironment, RotaServices rotas)
        {
            _appEnvironment = appEnvironment;
            _rotaServices = rotas;
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
        public ActionResult Servicos(List<string> selecionarCabecalhos)
        {
            if (!selecionarCabecalhos.Contains("OS") &&
                !selecionarCabecalhos.Contains("CIDADE") &&
                !selecionarCabecalhos.Contains("BASE") &&
                !selecionarCabecalhos.Contains("SERVIÇO") &&
                !selecionarCabecalhos.Contains("ENDEREÇO") &&
                !selecionarCabecalhos.Contains("NUMERO") &&
                !selecionarCabecalhos.Contains("COMPLEMENTO") &&
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

            var servicosDuplicados = servicos.GroupBy(servico => servico.ToString()).Where(x => x.Count() > 1);

            foreach(var servico in servicosDuplicados)
            {
                buscarServicos.Add(servico.Key);
            }

            var lerArquivoExcel = LeituraDeArquivos.LerArquivoExcel(selecionarCabecalhos, _appEnvironment.WebRootPath);

            ViewBag.Servicos = buscarServicos;

            return View();
        }

    }
}
