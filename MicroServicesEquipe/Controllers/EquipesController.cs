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

        [HttpGet("{equipe}", Name = "GetEquipe")]
        public ActionResult<Equipe> GetNome(string equipe)
        {
            var buscarEquipe = _equipeServices.Get(equipe);

            if (buscarEquipe == null)
                return BadRequest("Equipe não cadastrada");

            return buscarEquipe;
        }

        [HttpPost]
        public async Task<ActionResult<Equipe>> Create(Equipe novaEquipe)
        {
            var buscarEquipe = _equipeServices.Get(novaEquipe.Nome);

            if (buscarEquipe != null)
                return BadRequest("Equipe já cadastrada");

            await _equipeServices.Create(novaEquipe);

            return CreatedAtRoute("GetEquipe", new { equipe = novaEquipe.Nome }, novaEquipe);
        }

        [HttpPut("{equipe}")]
        public IActionResult Update(string equipe, Equipe equipeIn)
        {
            var buscarTime = _equipeServices.Get(equipe);

            if (buscarTime == null)
                return BadRequest("Equipe não encontrada");

            _equipeServices.Update(equipe, equipeIn);

            return NoContent();

        }

        [HttpDelete("{equipe}")]
        public IActionResult Delete(string equipe)
        {
            var buscarEquipe = _equipeServices.Get(equipe);

            if (buscarEquipe == null)
                return BadRequest("Equipe não encontrada");

            return NoContent();

        }
    }
}
