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

        [HttpGet("{Cidade}", Name = "GetCidade")]
        public ActionResult<Cidade> GetCidadeNome(string cidade)
        {
            var buscarCidade = _cidadeServices.Get(cidade);

            if (buscarCidade == null)
                return BadRequest("Cidade não encontrada");

            return buscarCidade;
        }

        [HttpPost]
        public ActionResult<Cidade> Create(Cidade novaCidade)
        {
            var buscarCidade = _cidadeServices.Get(novaCidade.Nome);

            if (buscarCidade != null)
                return BadRequest("Cidade já cadastrada");

            _cidadeServices.Create(novaCidade);

            return CreatedAtRoute("GetCidade", new { cidade = novaCidade.Nome }, novaCidade);
        }

        [HttpPut("{cidade}")]
        public IActionResult Update(string cidade, Cidade cidadeIn)
        {
            var buscarCidade = _cidadeServices.Get(cidade);

            if (buscarCidade == null)
                return NotFound("Cidade não cadastrada");

            _cidadeServices.Update(cidade, cidadeIn);

            return NoContent();

        }

        [HttpDelete("{Cidade}")]
        public IActionResult Delete(string cidade)
        {
            var buscarCidade = _cidadeServices.Get(cidade);

            if (buscarCidade == null)
                return NotFound("Cidade não encontrada");

            _cidadeServices.Delete(cidade);
            return NoContent();
        }
    }
}
