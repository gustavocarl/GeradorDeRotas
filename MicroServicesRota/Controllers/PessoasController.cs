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

        [HttpGet("{id:length(24)}", Name = "GetPessoa")]
        public ActionResult<Pessoa> Get(string id)
        {
            var buscarPessoa = _pessoaServices.Get(id);
            if (buscarPessoa == null)
                return BadRequest("Pessoa não encontrada");
            return buscarPessoa;
        }

        [HttpGet("{nome}", Name = "GetPessoaNome")]
        public ActionResult<Pessoa> GetNome(string nome)
        {
            var buscarPessoa = _pessoaServices.GetNome(nome);
            if (buscarPessoa == null)
                return BadRequest("Pessoa não encontrada");
            return buscarPessoa;
        }

        [HttpPost]
        public ActionResult<Pessoa> Create(Pessoa novaPessoa)
        {
         
            var buscarPessoa = _pessoaServices.Get(novaPessoa.Id);

            _pessoaServices.Create(novaPessoa);
            return CreatedAtRoute("GetPessoa", new { id = novaPessoa.Id }, novaPessoa);

        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Pessoa pessoaIn)
        {
            var buscarPessoa = _pessoaServices.Get(id);

            if (buscarPessoa == null)
                return BadRequest("Pessoa não cadastrada");
            
            _pessoaServices.Update(id, pessoaIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var buscarPessoa = _pessoaServices.Get(id);
            if (buscarPessoa == null)
                return BadRequest("Pessoa não encontrada");
            
            _pessoaServices.Delete(buscarPessoa);
            return NoContent();
        }

    }
}
