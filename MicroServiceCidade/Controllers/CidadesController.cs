using MicroServiceCidade.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;

namespace MicroServiceCidade.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadesController : ControllerBase
    {
        private readonly CidadeServices _cidadeServices;

        public CidadesController(CidadeServices cidadeServices)
        {
            _cidadeServices = cidadeServices;
        }

        [HttpGet]
        public ActionResult<List<Cidade>> GetAll() => _cidadeServices.Get();


        [HttpGet("{id:length(24)}", Name = "GetCidade")]
        public ActionResult<Cidade> GetCidade(string id)
        {
            var buscarCidade = _cidadeServices.Get(id);

            if (buscarCidade == null)
                return BadRequest("Cidade não encontrada");

            return buscarCidade;
        }

        [HttpGet("{nome}", Name = "GetCidadeNome")]
        public ActionResult<Cidade> GetCidadeNome(string nome)
        {
            var buscarCidade = _cidadeServices.GetCidadeNome(nome);

            if (buscarCidade == null)
                return BadRequest("Cidade não encontrada");

            return buscarCidade;
        }

        [HttpPost]
        public ActionResult<Cidade> Create(Cidade novaCidade)
        {
            var buscarCidade = _cidadeServices.GetCidadeNome(novaCidade.Nome);

            if (buscarCidade != null)
                return BadRequest("Cidade já cadastrada");

            _cidadeServices.Create(novaCidade);

            return CreatedAtRoute("GetCidade", new { id = novaCidade.Id }, novaCidade);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Cidade cidadeIn)
        {
            var buscarCidade = _cidadeServices.Get(id);

            if (buscarCidade == null)
                return NotFound("Cidade não cadastrada");

            _cidadeServices.Update(id, cidadeIn);
            return NoContent();

        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var buscarCidade = _cidadeServices.Get(id);

            if (buscarCidade == null)
                return NotFound("Cidade não encontrada");

            _cidadeServices.Delete(buscarCidade);
            return NoContent();
        }
    }
}
