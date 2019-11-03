namespace PlanetAPI.Models{
    public class ConsumableItem{
        public int ItemId {get; set;}
        public string ItemName {get; set;}
        public int ConsumedPerHour {get; set;}
        public double BaseValue {get; set;}
        public int Amount {get; set;}
        public int MaxAmount {get; set;}
    }
}