namespace PlanetAPI.Models{
    using System.Collections.Generic;
    using System;
    public class PlanetMarket{
        public List<ProductionItem> ProductionItems {get; set;}
        public List<ConsumableItem> ConsumableItems {get;set;}
        public DateTime LastUpdate{get; set;}
    }
}