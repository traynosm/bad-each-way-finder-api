using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.DomainModel;
using bad_each_way_finder_api_domain.Exchange;
using bad_each_way_finder_api_domain.Extensions;
using bad_each_way_finder_api_domain.Sportsbook;
using bad_each_way_finder_api_exchange.Interfaces;
using bad_each_way_finder_api_sportsbook.Interfaces;

namespace bad_each_way_finder_api.Services
{
    public class PropositionService : IPropositionService
    {
        private readonly IExchangeHandler _exchangeHandler;
        private readonly ISportsbookHandler _sportsbookHandler;

        public PropositionService(IExchangeHandler exchangeHandler, ISportsbookHandler sportsbookHandler)
        {
            _exchangeHandler = exchangeHandler;
            _sportsbookHandler = sportsbookHandler;
        }

        public List<Race> BuildRaces()
        {
            var races = new List<Race>();

            try
            {
                //win
                var SportsbookMarketDetails = GetMarketDetails();

                var linkedWinMarketIds = SportsbookMarketDetails.marketDetails
                    .Select(l => l.linkedMarketId);

                if(linkedWinMarketIds== null || !linkedWinMarketIds.Any())
                {
                    //log this
                    return races;
                }

                var exchangeWinMarketBooks = GetMarketBooks(linkedWinMarketIds.ToList()!);

                //place
                var exchangeMarketCatalogues = _exchangeHandler.ListMarketCatalogues();

                var events = exchangeMarketCatalogues
                    .Select(m => m.Event);

                var exchangePlaceMarketCatalogues = exchangeMarketCatalogues
                    .Where(m =>
                        m.Description?.MarketType == "PLACE" ||
                        m.Description?.MarketType == "OTHER_PLACE");

                if(exchangePlaceMarketCatalogues == null || !exchangePlaceMarketCatalogues.Any())
                {
                    //log this
                    return races;
                }

                var exchangePlaceMarketIds = exchangePlaceMarketCatalogues!
                    .Select(m => m.MarketId);

                var exchangePlaceMarketBooks = GetMarketBooks(exchangePlaceMarketIds.ToList());

                foreach (var sportsbookMarketDetail in SportsbookMarketDetails.marketDetails
                    .Where(m => m.marketStatus == "OPEN"))
                {
                    var mappedExchangeWinMarketBook = exchangeWinMarketBooks
                        .FirstOrDefault(m => m.MarketId == sportsbookMarketDetail.linkedMarketId);

                    if (mappedExchangeWinMarketBook == null)
                    {
                        //log this
                        continue;
                    }

                    var mappedExchangeWinMarketCatalogue = exchangeMarketCatalogues
                        .FirstOrDefault(m => m.MarketId == sportsbookMarketDetail.linkedMarketId);

                    if (mappedExchangeWinMarketCatalogue == null)
                    {
                        //log this
                        continue;
                    }

                    var raceExchangePlaceMarketCatalogues = exchangePlaceMarketCatalogues
                        .Where(m => m.Description?.MarketTime == sportsbookMarketDetail.marketStartTime);

                    if (raceExchangePlaceMarketCatalogues == null || !raceExchangePlaceMarketCatalogues.Any())
                    {
                        //log this
                        continue;
                    }

                    var mappedEvent = raceExchangePlaceMarketCatalogues
                        .First().Event;

                    if (mappedEvent == null)
                    {
                        //log this
                        continue;
                    }

                    var racePlaceMarketIds = raceExchangePlaceMarketCatalogues
                        .Select(m => m.MarketId);

                    var raceExhangePlaceMarketBooks = exchangePlaceMarketBooks
                        .Where(m => racePlaceMarketIds.Contains(m.MarketId));

                    var mappedExchangePlaceMartketBook = raceExhangePlaceMarketBooks
                        .FirstOrDefault(m => m.NumberOfWinners == sportsbookMarketDetail.numberOfPlaces);

                    if (mappedExchangePlaceMartketBook == null)
                    {
                        //log this
                        continue;
                    }
                    var runnerList = new List<bad_each_way_finder_api_domain.DomainModel.Runner>();

                    foreach (var runner in sportsbookMarketDetail.runnerDetails
                        .Where(r => r.runnerOrder < 50))
                    {
                        var Runner = new bad_each_way_finder_api_domain.DomainModel.Runner();

                        var mappedRunner = mappedExchangeWinMarketCatalogue.Runners
                            .FirstOrDefault(r => r.RunnerName == runner.selectionName);

                        if(mappedRunner == null)
                        {
                            //log this
                            continue;
                        }

                        Runner.RunnerSelectionId = runner.selectionId;
                        Runner.RunnerName = runner.selectionName;
                        Runner.WinRunnerOdds = runner.winRunnerOdds;

                        var mappedExchangeWinRunner = mappedExchangeWinMarketBook.Runners
                            .FirstOrDefault(r => r.SelectionId == mappedRunner.SelectionId);

                        if (mappedExchangeWinRunner == null)
                        {
                            //log this
                            runnerList.Add(Runner);
                            continue;
                        }
                        if (mappedExchangeWinRunner.ExchangePrices == null)
                        {
                            //log this
                            runnerList.Add(Runner);
                            continue;
                        }
                        if (!mappedExchangeWinRunner.ExchangePrices.AvailableToLay.Any())
                        {
                            //log this
                            runnerList.Add(Runner);
                            continue;
                        }
                        var exchangeWinPrice = mappedExchangeWinRunner.ExchangePrices.AvailableToLay[0].Price;

                        var winExpectedValue = runner.winRunnerOdds.@decimal.ExpectedValue(exchangeWinPrice);

                        var mappedExchangePlaceRunner = mappedExchangePlaceMartketBook.Runners
                            .FirstOrDefault(r => r.SelectionId == runner.selectionId);

                        if (mappedExchangePlaceRunner == null)
                        {
                            //log this
                            runnerList.Add(Runner);
                            continue;
                        }
                        if (mappedExchangePlaceRunner.ExchangePrices == null)
                        {
                            //log this
                            runnerList.Add(Runner);
                            continue;
                        }
                        if (!mappedExchangePlaceRunner.ExchangePrices.AvailableToLay.Any())
                        {
                            //log this
                            runnerList.Add(Runner);
                            continue;
                        }
                        var exchangePlacePrice = mappedExchangePlaceRunner.ExchangePrices.AvailableToLay[0].Price;

                        var placeExpectedValue = runner.winRunnerOdds.@decimal
                            .PlacePart(sportsbookMarketDetail.placeFractionDenominator)
                            .ExpectedValue(exchangePlacePrice);

                        var eachWayExpectedValue = (winExpectedValue + placeExpectedValue) / 2;

                        Runner.ExchangeWinPrice = exchangeWinPrice;
                        Runner.ExchangePlacePrice = exchangePlacePrice;
                        Runner.WinExpectedValue = winExpectedValue;
                        Runner.PlaceExpectedValue = placeExpectedValue;
                        Runner.EachWayExpectedValue = eachWayExpectedValue; 

                        runnerList.Add(Runner);
                    }

                    var Race = new Race()
                    {
                        EventId = sportsbookMarketDetail.eventId,
                        EventName = mappedEvent.Name,
                        EventDateTime = sportsbookMarketDetail.marketStartTime,
                        ExchangeWinMarketId = mappedExchangeWinMarketBook.MarketId,
                        ExchangePlaceMarketId = mappedExchangePlaceMartketBook.MarketId,
                        SportsbookWinMarketId = sportsbookMarketDetail.marketId,
                        SportsbookEachwayAvailable = sportsbookMarketDetail.eachwayAvailable,
                        SportsbookNumberOfPlaces = sportsbookMarketDetail.numberOfPlaces,
                        SportsbookPlaceFractionDenominator = sportsbookMarketDetail.placeFractionDenominator,
                        Runners = runnerList,
                    };

                    races.Add(Race);
                }
                return races;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return races;
            }
        }

        private MarketDetails GetMarketDetails() 
        {
            var loginSuccess = _sportsbookHandler.TryLogin();

            if (loginSuccess)
            {
                var eventIds = _sportsbookHandler.ListEventsByEventType("7");
                var eId = _sportsbookHandler.ListMarketCatalogues(eventIds.Select(
                    e => e.Id).ToHashSet());
                var result = _sportsbookHandler.ListPrices(eId.Select(
                    e => e.MarketId).ToList());
                return result;
            }
            else
            {
                throw new Exception("Login failed");
            }
        }

        private IList<MarketBook> GetMarketBooks(List<string> marketIds)
        {
            var loginSuccess = _exchangeHandler.TryLogin();

            if (loginSuccess)
            {
                var result = _exchangeHandler.ListMarketBooks(marketIds);

                return result;
            }
            else
            {
                throw new Exception("Login failed");
            }
        }
    }
}
