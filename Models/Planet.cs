namespace PlanetAPI.Models{
    using System.Collections.Generic;
    public class Planet {
        public int PlanetId {get; set;}
        public string PlanetName {get; set;}
        public List<AdjacentPlanet> AdjacentPlanets {get; set;}
        public PlanetMarket Market {get; set;}
    }
}