using MicroServicesRota.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;

namespace MicroServicesPessoa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly PessoaServices _pessoaServices;

        public PessoasController(PessoaServices pessoaService)
        {
            _pessoaServices = pessoaService;
        }

        [HttpGet]
        public ActionResult<List<Pessoa>> GetAll() =>
            _pessoaServices.Get();

        [HttpGet("{Nome}", Name = "GetNome")]
        public ActionResult<Pessoa> Get(string nome)
        {
            var buscarPessoa = _pessoaServices.GetNome(nome);
            if (buscarPessoa == null)
                return BadRequest("Pessoa não encontrada");
            return buscarPessoa;
        }

        [HttpPost]
        public ActionResult<Pessoa> Create(Pessoa novaPessoa)
        {
         
            var buscarPessoa = _pessoaServices.GetNome(novaPessoa.Nome);

            _pessoaServices.Create(novaPessoa);
            return CreatedAtRoute("GetNome", new { nome = novaPessoa.Nome }, novaPessoa);

        }

        [HttpPut("{nome}")]
        public IActionResult Update(string nome, Pessoa pessoaIn)
        {
            var buscarPessoa = _pessoaServices.GetNome(nome);

            if (buscarPessoa == null)
                return BadRequest("Pessoa não cadastrada");
            
            _pessoaServices.Update(nome, pessoaIn);
            return NoContent();
        }

        [HttpDelete("id:length(24)")]
        public IActionResult Delete(string id)
        {
            var buscarPessoa = _pessoaServices.Get(id);
            if (buscarPessoa == null)
                return BadRequest("Pessoa não encontrada");
            
            _pessoaServices.Delete(buscarPessoa.Id);
            return NoContent();
        }

    }
}
