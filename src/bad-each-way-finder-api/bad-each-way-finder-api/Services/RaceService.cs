using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.DomainModel;
using bad_each_way_finder_api_domain.Exchange;
using bad_each_way_finder_api_domain.Extensions;
using bad_each_way_finder_api_domain.Sportsbook;
using bad_each_way_finder_api_exchange.Interfaces;
using bad_each_way_finder_api_sportsbook.Interfaces;

namespace bad_each_way_finder_api.Services
{
    public class RaceService : IRaceService
    {
        private readonly IExchangeHandler _exchangeHandler;
        private readonly ISportsbookHandler _sportsbookHandler;
        private readonly ILogger<RaceService> _logger;
        private readonly IPropositionDatabaseService _propositionDatabaseService;
        private readonly IRaceDatabaseService _raceDatabaseService;
        private readonly ISportsbookDatabaseService _sportsbookDatabaseService;

        public RaceService(IExchangeHandler exchangeHandler, ISportsbookHandler sportsbookHandler,
            ILogger<RaceService> logger, IPropositionDatabaseService propositionDatabaseService,
            IRaceDatabaseService raceDatabaseService, ISportsbookDatabaseService sportsbookDatabaseService)
        {
            _exchangeHandler = exchangeHandler;
            _sportsbookHandler = sportsbookHandler;
            _logger = logger;
            _propositionDatabaseService = propositionDatabaseService;
            _raceDatabaseService = raceDatabaseService;
            _sportsbookDatabaseService = sportsbookDatabaseService;
        }

        public async Task<List<Race>> BuildRaces()
        {
            var races = new List<Race>();

            try
            {
                //win
                var SportsbookMarketDetails = GetMarketDetails();

                var linkedWinMarketIds = SportsbookMarketDetails.marketDetails
                    .Select(l => l.linkedMarketId);

                if(linkedWinMarketIds == null || !linkedWinMarketIds.Any())
                {
                    _logger.LogWarning("NO_LINKED_WIN_MARKET_IDS; " +
                        "Source= PropositionService; " +
                        "Action= BuildRaces; " +
                        "Msg= No linked Win Market Ids present, cannot continue; ");

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
                    _logger.LogWarning("NO_EX_PLACE_MARKET_CATALOGUES; " +
                        "Source=PropositionService; " +
                        "Action=BuildRaces; " +
                        "Msg=No Exchange Place Market Catalgues present, cannot continue; ");

                    return races;
                }

                var exchangePlaceMarketIds = exchangePlaceMarketCatalogues!
                    .Select(m => m.MarketId);

                var exchangePlaceMarketBooks = GetMarketBooks(exchangePlaceMarketIds.ToList());

                foreach (var sportsbookMarketDetail in SportsbookMarketDetails.marketDetails
                    .Where(m => m.marketStatus == "OPEN"))
                {
                    var mappedExchangeWinMarketCatalogue = exchangeMarketCatalogues
                         .FirstOrDefault(m => m.MarketId == sportsbookMarketDetail.linkedMarketId);

                    if (mappedExchangeWinMarketCatalogue == null)
                    {
                        _logger.LogWarning($"NO_MAPPED_EXCHANGE_WIN_MARKET_CATALOGU; " +
                            $"Source=PropositionService; " +
                            $"Action=BuildRaces; " +
                            $"LinkedMarketId={sportsbookMarketDetail.linkedMarketId}; " +
                            $"MarketId={sportsbookMarketDetail.marketId}; " +
                            $"MarketStartTime={sportsbookMarketDetail.marketStartTime}; " +
                            $"Msg=No Mapped Exchange Win Market Catalogue; ");

                        continue;
                    }

                    var mappedEvent = mappedExchangeWinMarketCatalogue.Event;

                    if(mappedEvent.Venue == "Southwell")
                    {
                        Console.WriteLine("");
                    }

                    if (mappedEvent == null)
                    {
                        _logger.LogWarning("NO_MAPPED_EVENT; " +
                            "Source=PropositionService; " +
                            "Action=BuildRaces; " +
                            $"LinkedMarketId={sportsbookMarketDetail.linkedMarketId}; " +
                            $"MarketStartTime={sportsbookMarketDetail.marketStartTime}; " +
                            "Msg=No Mapped Event present, cannot continue; ");

                        continue;
                    }

                    var mappedExchangeWinMarketBook = exchangeWinMarketBooks
                        .FirstOrDefault(m => m.MarketId == sportsbookMarketDetail.linkedMarketId);

                    if (mappedExchangeWinMarketBook == null)
                    {
                        _logger.LogWarning($"NO_MAPPED_EXCHANGE_WIN_MARKET_BOOK; " +
                            $"Source=PropositionService; " +
                            $"Action=BuildRaces; " +
                            $"LinkedMarketId={sportsbookMarketDetail.linkedMarketId}; " +
                            $"EventName={mappedEvent.Name}; " +
                            $"MarketStartTime={sportsbookMarketDetail.marketStartTime}; " +
                            $"Msg=No Mapped Exchange Win Market Book; ");

                        continue;
                    }

                    var raceExchangePlaceMarketCatalogues = exchangePlaceMarketCatalogues
                        .Where(m => m.Description?.MarketTime == sportsbookMarketDetail.marketStartTime);

                    if (raceExchangePlaceMarketCatalogues == null || !raceExchangePlaceMarketCatalogues.Any())
                    {
                        _logger.LogWarning($"NO_RACE_EXCHANGE_PLACE_MARKET_CATALOGUES; " +
                            $"Source=PropositionService; " +
                            $"Action=BuildRaces; " +
                            $"LinkedMarketId={sportsbookMarketDetail.linkedMarketId}; " +
                            $"EventName={mappedEvent.Name}; " +
                            $"MarketStartTime={sportsbookMarketDetail.marketStartTime}; " +
                            $"Msg=No Mapped Exchange Win Market Book; ");

                        continue;
                    }

                    var racePlaceMarketIds = raceExchangePlaceMarketCatalogues
                        .Select(m => m.MarketId);

                    var raceExhangePlaceMarketBooks = exchangePlaceMarketBooks
                        .Where(m => racePlaceMarketIds.Contains(m.MarketId));

                    var mappedExchangePlaceMartketBook = raceExhangePlaceMarketBooks
                        .FirstOrDefault(m => m.NumberOfWinners == sportsbookMarketDetail.numberOfPlaces);

                    var mappedExchangePlaceMarketId = "0";
                    if (mappedExchangePlaceMartketBook == null)
                    {
                        _logger.LogWarning("NO_MAPPED_EXCHANGE_PLACE_MARKET_BOOK; " +
                            "Source=PropositionService; " +
                            "Action=BuildRaces; " +
                            $"LinkedMarketId={sportsbookMarketDetail.linkedMarketId}; " +
                            $"EventName={mappedEvent.Name}; " +
                            $"MarketStartTime={sportsbookMarketDetail.marketStartTime}; " +
                            "Msg=No Mapped Exchange Place Market Book present, cannot continue; ");

                        //continue;
                    }
                    else
                    {
                        mappedExchangePlaceMarketId = mappedExchangePlaceMartketBook.MarketId;
                    }

                    var runnerList = new List<RunnerInfo>();

                    foreach (var runner in sportsbookMarketDetail.runnerDetails
                        .Where(r => r.runnerOrder < 50 && r.runnerStatus == "ACTIVE"))
                    {
                        var RunnerInfo = new RunnerInfo();

                        var mappedRunner = mappedExchangeWinMarketCatalogue.Runners
                            .FirstOrDefault(r => r.RunnerName == runner.selectionName);

                        if(mappedRunner == null)
                        {
                            _logger.LogWarning("NO_MAPPED_RUNNER; " +
                                "Source=PropositionService; " +
                                "Action=BuildRaces; " +
                                $"LinkedMarketId={sportsbookMarketDetail.linkedMarketId}; " +
                                $"EventName={mappedEvent.Name}; " +
                                $"MarketStartTime={sportsbookMarketDetail.marketStartTime}; " +
                                $"Name={runner.selectionName}; " +
                                "Msg=No Mapped Runner present, cannot continue; ");

                            continue;
                        }

                        RunnerInfo.Id = $"{mappedEvent.Id}{runner.selectionId}";
                        RunnerInfo.RunnerSelectionId = runner.selectionId;
                        RunnerInfo.RunnerName = runner.selectionName;
                        RunnerInfo.RunnerOrder = runner.runnerOrder;
                        RunnerInfo.RunnerStatus = runner.runnerStatus;

                        if(runner.winRunnerOdds == null)
                        {
                            _logger.LogWarning($"RUNNER_ODDS_INVALID; " +
                                $"Source=PropositionService; " +
                                $"Action=BuildRaces; " +
                                $"LinkedMarketId={sportsbookMarketDetail.linkedMarketId}; " +
                                $"EventName={mappedEvent.Name}; " +
                                $"MarketStartTime={sportsbookMarketDetail.marketStartTime}; " +
                                $"Name={runner.selectionName}; " +
                                $"Msg=No valid odds for runner, cannot continue; ");

                            continue;
                        }

                        RunnerInfo.WinRunnerOddsDecimal = runner.winRunnerOdds.@decimal;
                        RunnerInfo.WinRunnerOddsNumerator = runner.winRunnerOdds.numerator;
                        RunnerInfo.WinRunnerOddsDenominator = runner.winRunnerOdds.denominator;

                        var mappedExchangeWinRunner = mappedExchangeWinMarketBook.Runners
                            .FirstOrDefault(r => r.SelectionId == mappedRunner.SelectionId);

                        if (mappedExchangeWinRunner == null)
                        {
                            _logger.LogWarning($"NO_MAPPED_EXCHANGE_WIN_RUNNER; " +
                                $"Source=PropositionService; " +
                                $"Action=BuildRaces; " +
                                $"EventName{mappedEvent.Name}; " +
                                $"MarketStartTime={sportsbookMarketDetail.marketStartTime}; " +
                                $"RunnerName={runner.selectionName}; " +
                                $"Msg=No Mapped Exchange Win Runner; ");

                            RunnerInfo.EachWayExpectedValue = -100;
                            RunnerInfo.WinExpectedValue = -100;
                            RunnerInfo.PlaceExpectedValue = -100;

                            runnerList.Add(RunnerInfo);
                            continue;
                        }
                        if (mappedExchangeWinRunner.ExchangePrices == null)
                        {
                            _logger.LogWarning($"NO_EXCHANGE_PRICES; " +
                                $"Source=PropositionService; " +
                                $"Action=BuildRaces; " +
                                $"EventName{mappedEvent.Name}; " +
                                $"MarketStartTime={sportsbookMarketDetail.marketStartTime}; " +
                                $"RunnerName={runner.selectionName}; " +
                                $"Msg=No Mapped Exchange Win Runner Exchange Prices; ");

                            RunnerInfo.EachWayExpectedValue = -100;
                            RunnerInfo.WinExpectedValue = -100;
                            RunnerInfo.PlaceExpectedValue = -100;

                            runnerList.Add(RunnerInfo);
                            continue;
                        }
                        if (!mappedExchangeWinRunner.ExchangePrices.AvailableToLay.Any())
                        {
                            _logger.LogWarning($"NO_LAY_PRICE_AVAILABLE; " +
                                $"Source=PropositionService; " +
                                $"Action=BuildRaces; " +
                                $"EventName{mappedEvent.Name}; " +
                                $"MarketStartTime={sportsbookMarketDetail.marketStartTime}; " +
                                $"RunnerName={runner.selectionName}; " +
                                $"Msg=No Lay price available in mapped exchange win runner; ");

                            RunnerInfo.EachWayExpectedValue = -100;
                            RunnerInfo.WinExpectedValue = -100;
                            RunnerInfo.PlaceExpectedValue = -100;

                            runnerList.Add(RunnerInfo);
                            continue;
                        }
                        var exchangeWinPrice = mappedExchangeWinRunner.ExchangePrices.AvailableToLay[0].Price;
                        var exchangeWinSize = mappedExchangeWinRunner.ExchangePrices.AvailableToLay[0].Size;

                        var winExpectedValue = runner.winRunnerOdds.@decimal.ExpectedValue(exchangeWinPrice);

                        RunnerInfo.ExchangeWinPrice = exchangeWinPrice;
                        RunnerInfo.ExchangeWinSize = exchangeWinSize;
                        RunnerInfo.WinExpectedValue = winExpectedValue;

                        var mappedExchangePlaceRunner = mappedExchangePlaceMartketBook?.Runners
                            .FirstOrDefault(r => r.SelectionId == runner.selectionId);

                        if (mappedExchangePlaceRunner == null)
                        {
                            _logger.LogWarning("NO_MAPPED_EXCHANGE_PLACE_RUNNER; " +
                                "Source=PropositionService; " +
                                "Action=BuildRaces; " +
                                $"LinkedMarketId={sportsbookMarketDetail.linkedMarketId}; " +
                                $"EventName={mappedEvent.Name}; " +
                                $"MarketStartTime={sportsbookMarketDetail.marketStartTime}; " +
                                $"Name={runner.selectionName}; " +
                                "Msg=No Mapped Exchange Place Runner present, cannot continue; ");

                            RunnerInfo.EachWayExpectedValue = -100;
                            RunnerInfo.PlaceExpectedValue = -100;

                            runnerList.Add(RunnerInfo);
                            continue;
                        }
                        if (mappedExchangePlaceRunner.ExchangePrices == null)
                        {
                            _logger.LogWarning("NO_EXCHANGE_PRICES_AVAILABLE; " +
                                "Source=PropositionService; " +
                                "Action=BuildRaces; " +
                                $"LinkedMarketId={sportsbookMarketDetail.linkedMarketId}; " +
                                $"EventName={mappedEvent.Name}; " +
                                $"MarketStartTime={sportsbookMarketDetail.marketStartTime}; " +
                                $"Name={runner.selectionName}; " +
                                "Msg=No Exchange Prices present in Mapped Exchange Place Runner, cannot continue; ");

                            RunnerInfo.EachWayExpectedValue = -100;
                            RunnerInfo.PlaceExpectedValue = -100;

                            runnerList.Add(RunnerInfo);
                            continue;
                        }
                        if (!mappedExchangePlaceRunner.ExchangePrices.AvailableToLay.Any())
                        {
                            _logger.LogWarning("NO_LAY_PRICES_AVAILABLE; " +
                                "Source=PropositionService; " +
                                "Action=BuildRaces; " +
                                $"LinkedMarketId={sportsbookMarketDetail.linkedMarketId}; " +
                                $"EventName={mappedEvent.Name}; " +
                                $"MarketStartTime={sportsbookMarketDetail.marketStartTime}; " +
                                $"Name={runner.selectionName}; " +
                                "Msg=No Lay Prices present in Mapped Exchange Place Runner, cannot continue; ");

                            RunnerInfo.EachWayExpectedValue = -100;
                            RunnerInfo.PlaceExpectedValue = -100;

                            runnerList.Add(RunnerInfo);
                            continue;
                        }

                        var exchangePlacePrice = mappedExchangePlaceRunner.ExchangePrices.AvailableToLay[0].Price;
                        var exchangePlaceSize = mappedExchangePlaceRunner.ExchangePrices.AvailableToLay[0].Size;

                        var placeExpectedValue = runner.winRunnerOdds.@decimal
                            .PlacePart(sportsbookMarketDetail.placeFractionDenominator)
                            .ExpectedValue(exchangePlacePrice);

                        var eachWayExpectedValue = (winExpectedValue + placeExpectedValue) / 2;

                        RunnerInfo.ExchangePlacePrice = exchangePlacePrice;
                        RunnerInfo.ExchangePlaceSize = exchangePlaceSize;
                        RunnerInfo.PlaceExpectedValue = placeExpectedValue;
                        RunnerInfo.EachWayExpectedValue = eachWayExpectedValue;
                        RunnerInfo.EachWayPlacePart = runner.winRunnerOdds.@decimal
                            .EachWayPlacePart(sportsbookMarketDetail.placeFractionDenominator);

                        runnerList.Add(RunnerInfo);
                    }

                    var Race = new Race()
                    {
                        EventId = sportsbookMarketDetail.eventId,
                        EventName = mappedEvent.Name,
                        EventDateTime = sportsbookMarketDetail.marketStartTime,
                        ExchangeWinMarketId = mappedExchangeWinMarketBook.MarketId,
                        ExchangePlaceMarketId = mappedExchangePlaceMarketId,
                        SportsbookWinMarketId = sportsbookMarketDetail.marketId,
                        SportsbookEachwayAvailable = sportsbookMarketDetail.eachwayAvailable,
                        SportsbookNumberOfPlaces = sportsbookMarketDetail.numberOfPlaces,
                        SportsbookPlaceFractionDenominator = sportsbookMarketDetail.placeFractionDenominator,
                        LastUpdated = DateTime.UtcNow,
                        Runners = runnerList,
                    };

                    _raceDatabaseService.AddOrUpdateRace(Race);

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

        public List<Proposition> DetermineLivePropositions(List<Race> races)
        {
            var result = new List<Proposition>();

            foreach (var race in races)
            {
                try 
                { 
                    foreach (var runner in race.Runners)
                    {
                        try
                        {
                            if (runner.EachWayExpectedValue >= -0.10)
                            {
                                var proposition = new Proposition(race, runner);

                                result.Add(proposition);
                                _propositionDatabaseService.AddProposition(proposition);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("");
                        }
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine("");
                }
            }
            return result;
        }

        public List<Proposition> GetRaisedPropositionsForTimeRange(TimeRange timeRange)
        {
            var todaysSavedPropositions = _propositionDatabaseService.GetRaisedPropositionsForTimeRange(timeRange);
            
            foreach (var proposition in todaysSavedPropositions)
            {
                var race = _raceDatabaseService.GetRace(proposition.SportsbookWinMarketId);
                var runnerInfo = _raceDatabaseService.GetRunnerInfo($"{proposition.EventId}{proposition.RunnerSelectionId}");
                var raceRule4Deductions = _sportsbookDatabaseService.RaceRule4Deductions(proposition.SportsbookWinMarketId);

                proposition.FinalAdjustedOddsDecimal = proposition.WinRunnerOddsDecimal;
                proposition.SportsbookNumberOfPlaces = race.SportsbookNumberOfPlaces;
                proposition.SportsbookPlaceFractionDenominator = race.SportsbookPlaceFractionDenominator;

                if(raceRule4Deductions.Any())
                {
                    foreach(var deduction in raceRule4Deductions)
                    {
                        if(proposition.RecordedAt >= deduction.timeFrom && proposition.RecordedAt <= deduction.timeTo)
                        {
                            proposition.Rule4Deduction = deduction.deduction;
                            proposition.FinalAdjustedOddsDecimal = ((proposition.WinRunnerOddsDecimal - 1) * ((100 - proposition.Rule4Deduction) / 100)) + 1;
                            break;
                        }
                    }
                }

                proposition.LatestWinPrice = runnerInfo.ExchangeWinPrice;
                proposition.LatestPlacePrice = runnerInfo.ExchangePlacePrice;

                if(runnerInfo.ExchangeWinPrice > 0)
                {
                    proposition.LatestWinExpectedValue = proposition.WinRunnerOddsDecimal.ExpectedValue(
                        proposition.LatestWinPrice);

                }

                if(runnerInfo.ExchangePlacePrice > 0)
                {
                    var placeExpectedValue = proposition.EachWayPlacePart.ExpectedValue(proposition.LatestPlacePrice);

                    proposition.LatestEachWayExpectedValue = (proposition.LatestWinExpectedValue + placeExpectedValue) / 2;
                }

                //Update Latest values to proposition
                _propositionDatabaseService.AddProposition(proposition);
            }
            return todaysSavedPropositions;
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

                _sportsbookDatabaseService.AddOrUpdateMarketDetails(result);

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
