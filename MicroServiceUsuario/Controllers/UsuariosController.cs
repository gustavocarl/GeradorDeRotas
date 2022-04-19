using MicroServiceUsuario.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;

namespace MicroServiceUsuario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly UsuarioServices _usuarioServices;

        public UsuariosController(UsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }

        [HttpGet]
        public ActionResult<List<Usuario>> GetAll() =>
            _usuarioServices.Get();

        [HttpGet("{id:length(24)}", Name = "GetUsuario")]
        public ActionResult<Usuario> Get(string id)
        {
            var buscarUsuario = _usuarioServices.Get(id);
            if (buscarUsuario == null)
                return BadRequest("Usuário não encontrado");
            return buscarUsuario;
        }

        [HttpGet("{nome}", Name = "GetUsuarioNome")]
        public ActionResult<Usuario> GetNome(string nome)
        {
            var buscarUsuario = _usuarioServices.GetNome(nome);
            if (buscarUsuario == null)
                return BadRequest("Usuário não encontrado");
            return buscarUsuario;
        }

        [HttpGet("login/{login}", Name = "GetUsuarioLogin")]
        public ActionResult<Usuario> GetLogin(string login)
        {
            var buscarUsuario = _usuarioServices.GetLogin(login);
            if (buscarUsuario == null)
                return BadRequest("Usuário não encontrado");
            return buscarUsuario;
        }

        [HttpPost]
        public ActionResult<Usuario> Create(Usuario novoUsuario)
        {

            var buscarUsuario = _usuarioServices.GetLogin(novoUsuario.Login);

            if (buscarUsuario != null)
                return BadRequest("Usuário já cadastrado");

            _usuarioServices.Create(novoUsuario);
            return CreatedAtRoute("GetUsuario", new { id = novoUsuario.Id }, novoUsuario);

        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Usuario usuarioIn)
        {
            var buscarUsuario = _usuarioServices.Get(id);

            if (buscarUsuario == null)
                return BadRequest("Usuário não cadastrado");

            _usuarioServices.Update(id, usuarioIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var buscarUsuario = _usuarioServices.Get(id);
            if (buscarUsuario == null)
                return BadRequest("Usuário não encontrado");

            _usuarioServices.Delete(buscarUsuario);
            return NoContent();
        }

    }
}
