namespace PlanetAPI.Repositories{
    using System.Threading.Tasks;
    using PlanetAPI.Models;
    using System.Linq;
    using System.Collections.Generic;

    public class PlanetRepository : IPlanetRepository
    {
        private List<Planet> _planets;

        public PlanetRepository(){
            _planets = new List<Planet>();
        }

        public async Task<Planet> GetAsync(int id){
            return await Task.Run(() => _planets.FirstOrDefault(x => x.PlanetId == id));
        }

        public List<Planet> GetPlanets(){
            return _planets;
        }

        public async Task AddPlanet(Planet planet){
            await Task.Run(() => _planets.Add(planet));
        }

        public async Task AddAdjacentPlanet(Planet planet, AdjacentPlanet adjacent){
            var p = await Task.Run(() => _planets.FirstOrDefault(x => x.PlanetId == planet.PlanetId));
            if(p != null){
                await Task.Run(() => p.AdjacentPlanets.Add(adjacent));
            }
        }

        public async Task<bool> UpdatePlanet(Planet planet){
            var p = await Task.Run(() => _planets.FirstOrDefault(x => x.PlanetId == planet.PlanetId));
            if(p != null){
                p.AdjacentPlanets = planet.AdjacentPlanets;
                p.Market = planet.Market;
                p.PlanetName = p.PlanetName;
                return true;
            }
            return false;
        }
        
    }
}