namespace PlanetAPI.Models{
    public class ProductionItem{
        public int ItemId {get; set;}
        public string ItemName {get; set;}
        public int ProducedPerHour {get; set;}
        public double BaseValue {get; set;}
        public int Amount {get; set;}
        public int MaxAmount {get; set;}
    }
}