namespace PlanetAPI.Models{
    public class MarketTransaction{
        public int TransactionId {get; set;}
        public string TransactionType {get; set;}
        public int ItemId {get; set;}
        public int Amount {get; set;}
    }
}