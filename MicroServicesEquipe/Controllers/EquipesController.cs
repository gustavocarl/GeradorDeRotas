using MicroServicesEquipe.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServicesEquipe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipesController : ControllerBase
    {
        private readonly EquipeServices _equipeServices;

        public EquipesController(EquipeServices equipeServices)
        {
            _equipeServices = equipeServices;
        }

        [HttpGet]
        public ActionResult<List<Equipe>> GetAll() => _equipeServices.Get();

        [HttpGet("{id:length(24)}", Name = "GetEquipe")]
        public ActionResult<Equipe> Get(string id)
        {
            var buscarEquipe = _equipeServices.Get(id);

            if (buscarEquipe == null)
                return BadRequest("Equipe não cadastrada");

            return buscarEquipe;
        }

        [HttpGet("{nome}", Name = "GetEquipeNome")]
        public ActionResult<Equipe> GetNome(string nome)
        {
            var buscarEquipe = _equipeServices.GetEquipeNome(nome);

            if (buscarEquipe == null)
                return BadRequest("Equipe não cadastrada");

            return buscarEquipe;
        }


        [HttpPost]
        public async Task<ActionResult<Equipe>> Create(Equipe novaEquipe)
        {
            var buscarEquipe = _equipeServices.GetEquipeNome(novaEquipe.Nome);

            if (buscarEquipe != null)
                return BadRequest("Equipe já cadastrada");

            await _equipeServices.Create(novaEquipe);

            return CreatedAtRoute("GetEquipe", new { id = novaEquipe.Id }, novaEquipe);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Equipe equipeIn)
        {
            var buscarTime = _equipeServices.Get(id);

            if (buscarTime == null)
                return BadRequest("Equipe não encontrada");

            _equipeServices.Update(id, equipeIn);

            return NoContent();

        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var buscarEquipe = _equipeServices.Get(id);

            if (buscarEquipe == null)
                return BadRequest("Equipe não encontrada");

            _equipeServices.Delete(buscarEquipe);
            return NoContent();

        }

    }
}
