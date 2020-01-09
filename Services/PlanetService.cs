namespace PlanetAPI.Services{
    using System.Threading.Tasks;
    using PlanetAPI.Models;
    using PlanetAPI.Repositories;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public class PlanetService : IPlanetService{
        private readonly IPlanetRepository _planetRepository;
        private readonly ILogger<PlanetService> _logger;

        public PlanetService(ILogger<PlanetService> logger, IPlanetRepository planetRepository){
            _logger = logger;
            _planetRepository = planetRepository;
            _planetRepository.AddPlanet(new Planet{
                PlanetId = 1,
                PlanetName = "Test Planet",
                AdjacentPlanets = new List<AdjacentPlanet>(){
                    new AdjacentPlanet(){
                        PlanetId = 2,
                        Distance = 10
                    }
                },
                Market = new PlanetMarket(){
                    ProductionItems = new List<ProductionItem>(){
                        new ProductionItem(){
                            ItemId = 1,
                            ItemName = "Box",
                            ProducedPerHour = 1000,
                            BaseValue = 1,
                            Amount = 0,
                            MaxAmount = 10000
                        }
                    },
                    ConsumableItems = new List<ConsumableItem>(){
                        new ConsumableItem(){
                            ItemId = 2,
                            ItemName = "Food",
                            ConsumedPerHour = 1000,
                            BaseValue = 2,
                            Amount = 0,
                            MaxAmount = 20000
                        },
                        new ConsumableItem(){
                            ItemId = 3,
                            ItemName = "Metal",
                            ConsumedPerHour = 2000,
                            BaseValue = 10,
                            Amount = 0,
                            MaxAmount = 5000
                        }
                    },
                    LastUpdate = DateTime.Now
                }
            });
        }

        public async Task<Planet> GetPlanetById(int id){
            return await _planetRepository.GetAsync(id);
        }
        public async Task<bool> AddPlanet(Planet planet){
            await _planetRepository.AddPlanet(planet);
            return true;
        }
        public async Task<bool> AddAdjacentPlanet(Planet planet, AdjacentPlanet adjacent){
            var p = await _planetRepository.GetAsync(planet.PlanetId);
            if(p != null){
                p.AdjacentPlanets.Add(adjacent);
                return await _planetRepository.UpdatePlanet(p);
            }
            return false;
        }

        public async Task<bool> UpdatePlanet(Planet planet){
            return await _planetRepository.UpdatePlanet(planet);
        }

        public async Task<MarketTransactionResult> MakeMarketTransaction(Planet planet, MarketTransaction marketTransaction){
            var marketTransactionResult = new MarketTransactionResult();
            var p = await _planetRepository.GetAsync(planet.PlanetId);
            if(p != null){
                var market = p.Market;

                if(marketTransaction.TransactionType == "sell"){
                    var item = market.ConsumableItems.FirstOrDefault(x => x.ItemId == marketTransaction.ItemId);
                    if(item.Amount + marketTransaction.Amount > item.MaxAmount)
                        marketTransactionResult.Result = false;
                    else{
                        item.Amount = item.Amount + marketTransaction.Amount;
                        marketTransactionResult.Result = true;
                        marketTransactionResult.TransactionType = marketTransaction.TransactionType;
                        marketTransactionResult.TransactionValue = marketTransaction.Amount * item.BaseValue;
                    }
                }else
                if(marketTransaction.TransactionType == "buy"){
                    var item = market.ProductionItems.FirstOrDefault(x => x.ItemId == marketTransaction.ItemId);
                    if(item.Amount - marketTransaction.Amount <= 0){
                        marketTransactionResult.Result = false;
                    }
                    else{
                        item.Amount = item.Amount - marketTransaction.Amount;
                        marketTransactionResult.Result = true;
                        marketTransactionResult.TransactionType = marketTransaction.TransactionType;
                        marketTransactionResult.TransactionValue = marketTransaction.Amount * item.BaseValue;
                    }
                }
            }
            else{
                marketTransactionResult.Result = false;
            }
            return marketTransactionResult;
        }

        public async Task UpdateMarkets(){
            var planets = await Task.Run(() => _planetRepository.GetPlanets());
            foreach(var p in planets){
                var market = p.Market;
                foreach(var produce in market.ProductionItems){
                    var timeSinceLastUpdate = (DateTime.Now - market.LastUpdate).TotalHours;
                    var amountProduced = produce.ProducedPerHour * timeSinceLastUpdate;
                    produce.Amount = produce.Amount + (int)Math.Round(amountProduced,0);
                    if(produce.Amount > produce.MaxAmount)
                        produce.Amount = produce.MaxAmount;
                }
                foreach(var consume in market.ConsumableItems){
                    var timeSinceLastUpdate = (DateTime.Now - market.LastUpdate).TotalHours;
                    var amountConsumed = consume.ConsumedPerHour * timeSinceLastUpdate;
                    consume.Amount = consume.Amount - (int)Math.Round(amountConsumed,0);
                    if(consume.Amount < 0){
                        consume.Amount = 0;
                    }
                }
                market.LastUpdate = DateTime.Now;
            }
        }


    }
}