using Microsoft.AspNetCore.Mvc;
using Model;
using MVCGeradorDeRotas.Services;
using System.Threading.Tasks;

namespace MVCGeradorDeRotas.Controllers
{
    public class PessoasController : Controller
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

            var buscarPessoas = await PessoaServices.Get();
            return View(buscarPessoas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Pessoa>> Create(Pessoa novaPessoa)
        {
            var inserirPessoa = await PessoaServices.PostPessoa(novaPessoa);

            if (inserirPessoa == null)
                return BadRequest("Não é possível inserir a pessoa");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var pessoa = await PessoaServices.GetId(id);

            if (pessoa == null)
                return NotFound("Pessoa não encontrada");

            return View(pessoa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Pessoa editarPessoa)
        {
            var pessoa = await PessoaServices.GetId(id);

            if (pessoa == null)
                return NotFound("Pessoa não encontrada");

            var pessoaEditada = await PessoaServices.PutPessoa(id, editarPessoa);

            if (pessoaEditada == null)
                return NotFound("API está fora do ar, tente novamente");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();

            var pessoa = await PessoaServices.GetId(id);

            if (pessoa == null)
                return NotFound("Pessoa não encontrada");

            return View(pessoa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, Pessoa removerPessoa)
        {
            var pessoa = await PessoaServices.GetId(id);

            if (pessoa == null)
                return BadRequest("Pessoa não encontrada");

            var deletarPessoa = await PessoaServices.DeletePessoa(id, removerPessoa);
            
            if (deletarPessoa == null)
                return NotFound("API está fora do ar, tente novamente");

            return RedirectToAction(nameof(Index));
        }


    }
}
