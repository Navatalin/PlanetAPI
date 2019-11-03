using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlanetAPI.Models;
using PlanetAPI.Services;

namespace PlanetAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]

    public class PlanetController : ControllerBase
    {
        private readonly ILogger<PlanetController> _logger;
        private readonly IPlanetService _planetService;

        public PlanetController(ILogger<PlanetController> logger, IPlanetService planetService){
            _logger = logger;
            _planetService = planetService;
        }

        [Route("{planetId:int}")]
        [HttpGet]
        public async Task<ActionResult<Planet>> GetPlanet(int planetId){
            return await _planetService.GetPlanetById(planetId);
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult> AddPlanet([FromBody] Planet planet){
            var result = await _planetService.AddPlanet(planet);

            if(!result){
                return BadRequest();
            }
            return Ok();
        }
        [Route("{planetId:int}/AdjacentPlanet")]
        [HttpPost]
        public async Task<ActionResult> AddAdjacentPlanet([FromBody] AdjacentPlanet adjacent, int planetId){
            var planet = await _planetService.GetPlanetById(planetId);
            var result = await _planetService.AddAdjacentPlanet(planet,adjacent);

            if(!result){
                return BadRequest();
            }
            return Ok();
        }
        [Route("{planetId:int}/Update")]
        [HttpPost]
        public async Task<ActionResult> UpdatePlanet([FromBody] Planet planet, int planetId){
            var result = await _planetService.UpdatePlanet(planet);

            if(!result){
                return BadRequest();
            }
            return Ok();
        }
        [Route("{planetId:int}/Market/Transaction")]
        [HttpPost]
        public async Task<ActionResult> MakeMarketTransaction([FromBody] MarketTransaction marketTransaction, int planetId){
            var planet = await _planetService.GetPlanetById(planetId);
            var result = await _planetService.MakeMarketTransaction(planet,marketTransaction);

            if(result == null){
                return BadRequest();
            }
            return Ok(result);
        }
        [Route("Markets/Update")]
        [HttpGet]
        public async Task<ActionResult> UpdateMarkets(){
            await _planetService.UpdateMarkets();
            return Ok();
        }
    }
}