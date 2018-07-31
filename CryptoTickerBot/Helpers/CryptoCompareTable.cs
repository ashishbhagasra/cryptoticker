using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using CryptoTickerBot.Data.Enums;
using CryptoTickerBot.Exchanges.Core;
using MoreLinq;

namespace CryptoTickerBot.Helpers
{
    using CoinMapCompare = Dictionary<CryptoCoinId, CoinMapModel>;
    using ExchangeToCoinMapCompare = Dictionary<CryptoExchangeId, Dictionary<CryptoCoinId, CoinMapModel>>;


    using CoinMap = Dictionary<CryptoCoinId, decimal>;
    using ExchangeToCoinMap = Dictionary<CryptoExchangeId, Dictionary<CryptoCoinId, decimal>>;

    public class BestCoinModel
    {
        public CryptoExchangeId from { get; set; }
        public CryptoExchangeId to { get; set; }
        public CryptoCoinId first { get; set; }
        public CryptoCoinId second { get; set; }
        public decimal profit { get; set; }
        public decimal fees { get; set; }
        
    }

    public class CoinMapModel
    {
        public CryptoCoinId Coin { get; set; }
        public string From { get; set; }
        public decimal Buy { get; set; } // Ask price at exchange
        public string To { get; set; }
        public decimal Sell { get; set; } // Bid Price at exchange
        public decimal WithdrawFee { get; set; }
        public decimal Profit { get; set; }
        public decimal ProfitINR { get; set; }
        public decimal ProfitPercentage { get; set; }
        public decimal minInvestment { get; set; }
    }

    public class CryptoCompareTable
    {
        public Dictionary<CryptoExchangeId, CryptoExchangeBase> Exchanges { get; } =
            new Dictionary<CryptoExchangeId, CryptoExchangeBase>();

        public CryptoCompareTable(params CryptoExchangeBase[] exchanges)
        {
            foreach (var exchange in exchanges)
                AddExchange(exchange);
        }

        [DebuggerStepThrough]
        public void AddExchange(CryptoExchangeBase exchange) =>
            Exchanges[exchange.Id] = exchange;

        [Pure]
        public CoinMap GetPair(CryptoExchangeId from, CryptoExchangeId to)
        {
            if (!Exchanges.ContainsKey(from) || !Exchanges.ContainsKey(to))
                return null;

            var result = new CoinMap();
            var symbols =
                Exchanges[from].ExchangeData.Keys
                    .Intersect(Exchanges[to].ExchangeData.Keys)
                    .ToList();

            foreach (var symbol in symbols)
            {
                var buy = Exchanges[from].ExchangeData[symbol].BuyPrice;
                var sell = Exchanges[to].ExchangeData[symbol].SellPrice;

                if (buy == 0 || sell == 0 || symbol == CryptoCoinId.NULL)
                    continue;

                result[symbol] = (sell - buy) / buy;
            }

            return result;
        }


        [Pure]
        public CoinMapCompare GetPairCompare(CryptoExchangeId from, CryptoExchangeId to)
        {
            if (!Exchanges.ContainsKey(from) || !Exchanges.ContainsKey(to))
                return null;

            var result = new CoinMapCompare();
            var symbols =
                Exchanges[from].ExchangeData.Keys
                    .Intersect(Exchanges[to].ExchangeData.Keys)
                    .ToList();

            foreach (var symbol in symbols)
            {
                var buy = Exchanges[from].ExchangeData[symbol].BuyPrice;
                var sell = Exchanges[to].ExchangeData[symbol].SellPrice;

                if (buy == 0 || sell == 0 || symbol == CryptoCoinId.NULL)
                    continue;

                CoinMapModel c = new CoinMapModel();

                c.Coin = symbol;
                c.From = from.ToString();
                c.To = to.ToString();
                c.Buy = buy;
                c.Sell = sell;

               // var profit = (1m + compare[from][to][best])
               //* (1m + compare[to][from][leastWorst])
               //- 1m;

                c.Profit = sell - buy;
                c.ProfitINR = FiatConverter.Convert(c.Profit, FiatCurrency.USD, FiatCurrency.INR); // 0; // TODO
                c.ProfitPercentage = (sell - buy) / buy;

                if (c.Profit <= 0)
                    continue;

                var fromExchange = Exchanges[from];
                decimal withdrawalFee = 0;
                try
                {
                    withdrawalFee = fromExchange.WithdrawalFees[symbol];
                }
                catch { }
                c.WithdrawFee = withdrawalFee;

                var fees = fromExchange[symbol].Buy(fromExchange.DepositFees[CryptoCoinId.BTC]) +
                           fromExchange[symbol].Sell(withdrawalFee);

                c.minInvestment = fees / c.Profit;


                result[symbol] = c;

            }

            return result;
        }


        [Pure]
        public ExchangeToCoinMap GetAll(CryptoExchangeId from)
        {
            if (!Exchanges.ContainsKey(from))
                return null;

            var result = new ExchangeToCoinMap();
            foreach (var to in Exchanges.Keys)
            {
                if (from == to)
                    continue;

                result[to] = GetPair(from, to);
            }

            return result;
        }

        [Pure]
        public Dictionary<CryptoExchangeId, ExchangeToCoinMap> GetAll()
        {
            var result = new Dictionary<CryptoExchangeId, ExchangeToCoinMap>();
            foreach (var exchange in Exchanges.Keys)
                result[exchange] = GetAll(exchange);

            return result;
        }

        [Pure]
        public Dictionary<CryptoExchangeId, ExchangeToCoinMap> Get(params CryptoExchangeId[] exchanges)
        {
            var result = new Dictionary<CryptoExchangeId, ExchangeToCoinMap>();
            foreach (var exchange in exchanges.Intersect(Exchanges.Keys))
                result[exchange] = GetAll(exchange)
                    .Where(pair => exchanges.Contains(pair.Key))
                    .ToDictionary();

            return result;
        }

        [Pure]
        public ExchangeToCoinMapCompare GetAllCompare(CryptoExchangeId from)
        {
            if (!Exchanges.ContainsKey(from))
                return null;

            var result = new ExchangeToCoinMapCompare();
            foreach (var to in Exchanges.Keys)
            {
                if (from == to)
                    continue;

                result[to] = GetPairCompare(from, to);
            }

            return result;
        }

        [Pure]
        public Dictionary<CryptoExchangeId, ExchangeToCoinMapCompare> GetAllCompare()
        {
            var result = new Dictionary<CryptoExchangeId, ExchangeToCoinMapCompare>();
            foreach (var exchange in Exchanges.Keys)
                result[exchange] = GetAllCompare(exchange);

            return result;
        }


        [Pure]
        public Dictionary<CryptoExchangeId, ExchangeToCoinMapCompare> GetCompare(params CryptoExchangeId[] exchanges)
        {
            var result = new Dictionary<CryptoExchangeId, ExchangeToCoinMapCompare>();
            foreach (var exchange in exchanges.Intersect(Exchanges.Keys))
                result[exchange] = GetAllCompare(exchange)
                    .Where(pair => exchanges.Contains(pair.Key))
                    .ToDictionary();

            return result;
        }

        public static void RemoveExchange(
            ExchangeToCoinMap compare,
            params CryptoExchangeId[] cryptoExchanges
        )
        {
            foreach (var exchange in cryptoExchanges)
                compare.Remove(exchange);
        }

        public static void RemoveExchange(
            Dictionary<CryptoExchangeId, ExchangeToCoinMap> compare,
            params CryptoExchangeId[] cryptoExchanges
        )
        {
            foreach (var exchange in cryptoExchanges)
                compare.Remove(exchange);

            foreach (var compareValue in compare.Values)
                RemoveExchange(compareValue, cryptoExchanges);
        }

        [Pure]
        public static (CryptoCoinId best, CryptoCoinId leastWorst, decimal profit) GetBestPair(
            Dictionary<CryptoExchangeId, ExchangeToCoinMap> compare,
            CryptoExchangeId from,
            CryptoExchangeId to
        )
        {
            var best = compare[from][to].MaxBy(x => x.Value).Key;
            var leastWorst = compare[to][from].MaxBy(x => x.Value).Key;
            var profit = (1m + compare[from][to][best])
                * (1m + compare[to][from][leastWorst])
                - 1m;

            return (best, leastWorst, profit);
        }

        [Pure]
        public (CryptoCoinId best, CryptoCoinId leastWorst, decimal profit, decimal fees) GetBestPair(
            CryptoExchangeId from, CryptoExchangeId to
        )
        {
            var compare = GetAll();
            var (best, leastWorst, profit) = GetBestPair(compare, from, to);
            var fromExchange = Exchanges[from];
            var toExchange = Exchanges[to];

            decimal wFeeFirst = 0;
            decimal wFeeSecond = 0;
            try
            {
                wFeeFirst = fromExchange.WithdrawalFees[best];
                wFeeSecond = toExchange.WithdrawalFees[leastWorst];
            }
            catch { }

            var fees =
                fromExchange[best].Buy(fromExchange.DepositFees[CryptoCoinId.BTC]) +
                fromExchange[best].Sell(wFeeFirst) +
                toExchange[leastWorst].Buy(toExchange.DepositFees[CryptoCoinId.BTC]) +
                toExchange[leastWorst].Sell(wFeeSecond);


            //var fees =
            //    fromExchange[best].Buy(fromExchange.DepositFees[best]) +
            //    fromExchange[best].Sell(fromExchange.WithdrawalFees[best]) +
            //    toExchange[leastWorst].Buy(toExchange.DepositFees[leastWorst]) +
            //    toExchange[leastWorst].Sell(toExchange.WithdrawalFees[leastWorst]);

            return (best, leastWorst, profit, fees);
        }

        [Pure]
        public
           List<BestCoinModel> GetBestAll()
        {
            var all = GetAll();
            var exchanges = Exchanges.Keys.ToList();
            List<BestCoinModel> result = new List<BestCoinModel>();

            foreach (var from in exchanges)
                foreach (var to in exchanges)
                {
                    if (from == to || all[from][to].Count == 0 || all[to][from].Count == 0)
                        continue;

                    var (best, leastWorst, profit) = GetBestPair(all, from, to);
                    BestCoinModel b = new BestCoinModel();
                    b.from = from;
                    b.to = to;
                    b.first = best;
                    b.second = leastWorst;
                    b.profit = profit;
                    b.fees = 0;

                    result.Add(b);
                }

            foreach(var item in result)
            {
                var fromExchange = Exchanges[item.from];
                var toExchange = Exchanges[item.to];

                decimal wFeeFirst = 0;
                decimal wFeeSecond = 0;
                try
                {
                    wFeeFirst = fromExchange.WithdrawalFees[item.first];
                    wFeeSecond = toExchange.WithdrawalFees[item.second];
                }
                catch { }

                item.fees =
                    fromExchange[item.first].Buy(fromExchange.DepositFees[CryptoCoinId.BTC]) +
                    fromExchange[item.first].Sell(wFeeFirst) +
                    toExchange[item.second].Buy(toExchange.DepositFees[CryptoCoinId.BTC]) +
                    toExchange[item.second].Sell(wFeeSecond);
            }

            return result;
        }


        [Pure]
        public
            (
            CryptoExchangeId from,
            CryptoExchangeId to,
            CryptoCoinId first,
            CryptoCoinId second,
            decimal profit,
            decimal fees
            )
            GetBest()
        {
            var all = GetAll();
            var exchanges = Exchanges.Keys.ToList();
            var bestGain = decimal.MinValue;
            (CryptoExchangeId from, CryptoExchangeId to, CryptoCoinId first, CryptoCoinId second, decimal profit, decimal fees)
                result = default;

            foreach (var from in exchanges)
                foreach (var to in exchanges)
                {
                    if (from == to || all[from][to].Count == 0 || all[to][from].Count == 0)
                        continue;

                    var (best, leastWorst, profit) = GetBestPair(all, from, to);
                    if (profit > bestGain)
                    {
                        bestGain = profit;
                        result = (from, to, best, leastWorst, profit, 0);
                    }
                }

            var fromExchange = Exchanges[result.from];
            var toExchange = Exchanges[result.to];

            decimal wFeeFirst = 0;
            decimal wFeeSecond = 0;
            try
            {
                wFeeFirst = fromExchange.WithdrawalFees[result.first];
                wFeeSecond = toExchange.WithdrawalFees[result.second];
            }
            catch { }

            result.fees =
                fromExchange[result.first].Buy(fromExchange.DepositFees[CryptoCoinId.BTC]) +
                fromExchange[result.first].Sell(wFeeFirst) +
                toExchange[result.second].Buy(toExchange.DepositFees[CryptoCoinId.BTC]) +
                toExchange[result.second].Sell(wFeeSecond);


            //result.fees =
            //	fromExchange[result.first].Buy ( fromExchange.DepositFees[result.first] ) +
            //	fromExchange[result.first].Sell ( fromExchange.WithdrawalFees[result.first] ) +
            //	toExchange[result.second].Buy ( toExchange.DepositFees[result.second] ) +
            //	toExchange[result.second].Sell ( toExchange.WithdrawalFees[result.second] );

            return result;
        }
    }
}