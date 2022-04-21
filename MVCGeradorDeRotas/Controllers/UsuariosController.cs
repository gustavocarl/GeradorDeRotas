using Microsoft.AspNetCore.Mvc;
using Model;
using System.Threading.Tasks;

namespace MVCGeradorDeRotas.Controllers
{
    public class UsuariosController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var buscarUsuario = await UsuarioServices.Get();
            return View(buscarUsuario);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Usuario>> Create(Usuario novoUsuario)
        {
            var inserirUsuario = await UsuarioServices.PostUsuario(novoUsuario);

            if (inserirUsuario == null)
                return BadRequest("Não é possível inserir o usuário");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var usuario = await UsuarioServices.GetId(id);
            if (usuario == null)
                return NotFound("Usuário não encontrado");

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Usuario editarUsuario)
        {
            var usuario = await UsuarioServices.GetId(id);

            if (usuario == null)
                return NotFound("Pessoa não encontrada");

            var usuarioEditado = await UsuarioServices.PutUsuario(id, editarUsuario);
            if (usuarioEditado == null)
                return NotFound("API está fora do ar, tente novamente");
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NoContent();

            var usuario = await UsuarioServices.GetId(id);
            if (usuario == null)
                return NotFound("Usuário não encontrado");

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, Usuario removerUsuario)
        {
            var usuario = await UsuarioServices.GetId(id);

            if(usuario == null)
                return BadRequest("Pessoa não encontrada");

            var deletarUsuario = await UsuarioServices.DeleteUsuario(id, removerUsuario);
            
            if (deletarUsuario == null)
                return NotFound("API está fora do ar, tente novamente");

            return RedirectToAction(nameof(Index));
        }

    }
}
