using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCGeradorDeRotas.Controllers
{
    public class UsuariosController : Controller
    {
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

            //var buscarUsuario = await UsuarioServices.Get();
            //return View(buscarUsuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Usuario>> Login(Usuario usuarioLogin)
        {

            var buscarUsuario = await UsuarioServices.GetLogin(usuarioLogin.Login);

            if (buscarUsuario != null)
            {
                if (usuarioLogin.Senha == buscarUsuario.Senha)
                {
                    List<Claim> usuarioClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, buscarUsuario.Login),
                        new Claim("Role", buscarUsuario.Role),
                        new Claim(ClaimTypes.Role, buscarUsuario.Role),
                    };

                    var identificacao = new ClaimsIdentity(usuarioClaims, "Usuario");
                    var usuarioPrincipal = new ClaimsPrincipal(new[] { identificacao });

                    await HttpContext.SignInAsync(usuarioPrincipal);

                    return RedirectToRoute(new { controller = "Uploads", action = "Index" });

                }
            }

            ViewBag.Message = "Usuário ou senha inválidos";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
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

        public IActionResult Edit()
        {
            //if (id == null)
            //    return NotFound();

            //var usuario = await UsuarioServices.GetId(id);
            //if (usuario == null)
            //    return NotFound("Usuário não encontrado");

            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit()
        //{
        //var usuario = await UsuarioServices.GetId(id);

        //if (usuario == null)
        //    return NotFound("Pessoa não encontrada");

        //var usuarioEditado = await UsuarioServices.PutUsuario(id, editarUsuario);
        //if (usuarioEditado == null)
        //    return NotFound("API está fora do ar, tente novamente");

        //    return RedirectToAction(nameof(Index));
        //}

        public IActionResult Delete()
        {
            //if (id == null)
            //    return NoContent();

            //var usuario = await UsuarioServices.GetId(id);
            //if (usuario == null)
            //    return NotFound("Usuário não encontrado");

            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete()
        //{
        //var usuario = await UsuarioServices.GetId(id);

        //if(usuario == null)
        //    return BadRequest("Pessoa não encontrada");

        //var deletarUsuario = await UsuarioServices.DeleteUsuario(id, removerUsuario);

        //if (deletarUsuario == null)
        //    return NotFound("API está fora do ar, tente novamente");

        //    return RedirectToAction(nameof(Index));
        //}

    }
}
