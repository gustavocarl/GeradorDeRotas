using Microsoft.AspNetCore.Mvc;
using Model;
using MVCGeradorDeRotas.Services;
using System.Threading.Tasks;

namespace MVCGeradorDeRotas.Controllers
{
    public class CidadesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var buscarCidades = await CidadeServices.Get();
            return View(buscarCidades);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Cidade>> Create(Cidade novaCidade)
        {
            var inserirCidade = await CidadeServices.PostCidade(novaCidade);

            if (inserirCidade == null)
                return BadRequest("Não é possível inserir a cidade");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var cidade = await CidadeServices.GetId(id);
            if (cidade == null)
                return NotFound("Cidade não encontrada");

            return View(cidade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Cidade editarCidade)
        {
            var cidade = await CidadeServices.GetId(id);

            if (cidade == null)
                return NotFound("Cidade não encontrada");

            var cidadeEditada = await CidadeServices.PutCidade(id, editarCidade);

            if (cidadeEditada == null)
                return NotFound("API está fora do ar, tente novamente");

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();

            var cidade = await CidadeServices.GetId(id);
            if (cidade == null)
                return NotFound("Cidade não encontrada");

            return View(cidade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, Cidade removerCidade)
        {
            var cidade = await CidadeServices.GetId(id);

            if (cidade == null)
                return BadRequest("Cidade não encontrada");

            var deletarCidade = await CidadeServices.DeleteCidade(id, removerCidade);

            return RedirectToAction(nameof(Index));
        }
    }
}
