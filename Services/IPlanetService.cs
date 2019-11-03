namespace PlanetAPI.Services
{
    using System.Threading.Tasks;
    using PlanetAPI.Models;

    public interface IPlanetService
    {
        Task<Planet> GetPlanetById(int id);
        Task<bool> AddPlanet(Planet planet);
        Task<bool> AddAdjacentPlanet(Planet planet, AdjacentPlanet adjacent);
        Task<bool> UpdatePlanet(Planet planet);
        Task<MarketTransactionResult> MakeMarketTransaction(Planet planet, MarketTransaction marketTransaction);
        Task UpdateMarkets();
    }
}