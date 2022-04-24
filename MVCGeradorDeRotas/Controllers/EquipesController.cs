using Microsoft.AspNetCore.Mvc;
using Model;
using MVCGeradorDeRotas.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCGeradorDeRotas.Controllers
{
    public class EquipesController : Controller
    {
        public async Task<IActionResult> Index()
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

            var buscarEquipes = await EquipeServices.Get();

            return View(buscarEquipes);
        }

        public async Task<IActionResult> Create()
        {

            var pessoa = await PessoaServices.Get();

            if (pessoa == null)
            {
                return BadRequest("Pessoa não cadastrada");
            }

            var cidade = await CidadeServices.Get();

            if (cidade == null)
            {
                return BadRequest("Cidade não cadastrada");
            }

            List<Pessoa> pessoaSemEquipe = pessoa.FindAll(pessoa => pessoa.NomeEquipe == "");

            ViewBag.Pessoa = pessoaSemEquipe;
            ViewBag.Cidade = cidade;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Equipe>> Create(Equipe equipe, List<string> selecionarPessoa)
        {

            List<Pessoa> escolherPessoa = new List<Pessoa>();

            foreach (string pessoaSelecionada in selecionarPessoa)
            {
                Pessoa pessoa = await PessoaServices.GetNome(pessoaSelecionada);

                if (pessoa == null)
                {
                    BadRequest("Não é possível inserir a pessoa");
                }

                pessoa.NomeEquipe = equipe.Nome;
                escolherPessoa.Add(pessoa);
            }

            equipe.Pessoa = escolherPessoa;

            var inserirEquipe = await EquipeServices.PostEquipe(equipe);

            if (inserirEquipe == null)
                return BadRequest("Não é possível inserir a equipe");

            foreach (Pessoa pessoa in escolherPessoa)
            {
                var inserirPessoa = await PessoaServices.PutPessoa(pessoa.Id, pessoa);

                if (inserirEquipe == null)
                    return BadRequest("Não é possível inserir a equipe");
            }

            if (string.IsNullOrEmpty(equipe.Nome))
                return BadRequest("Equipe não cadastrada");

            if (selecionarPessoa.Count < 1)
                return BadRequest("É necessário selecionar pelo menos uma pessoa para a equipe");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {

            if (id == null)
                return BadRequest("Equipe não encontrada");

            var equipe = await EquipeServices.GetId(id);

            if (equipe == null)
                return NotFound();

            return View(equipe);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, Equipe removerEquipe)
        {

            var equipe = await EquipeServices.GetId(id);

            if (equipe == null)
                return BadRequest("Equipe não encontrada");

            var equipeRemovida = await EquipeServices.DeleteEquipe(id, removerEquipe);

            if (equipeRemovida == null)
                return BadRequest("Não é possível remover a equipe");

            foreach (Pessoa pessoa in equipe.Pessoa)
            {
                pessoa.NomeEquipe = "";

                var inserirPessoa = await PessoaServices.PutPessoa(id, pessoa);

                if (inserirPessoa == null)
                    return BadRequest("Pessoa não encontrada");
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
