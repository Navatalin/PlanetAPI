namespace PlanetAPI.Repositories{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using PlanetAPI.Models;

    public interface IPlanetRepository
    {
        Task<Planet> GetAsync(int id);
        List<Planet> GetPlanets();
        Task AddPlanet(Planet planet);
        Task AddAdjacentPlanet(Planet planet, AdjacentPlanet adjacent);
        Task<bool> UpdatePlanet(Planet planet);
    }
}