using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CryptoTickerBot.Data.Enums;
using CryptoTickerBot.Exchanges.Core;
using CryptoTickerBot.Helpers;
using Flurl.Http;
using Newtonsoft.Json;

namespace CryptoTickerBot.Exchanges
{
    public class HitbtcExchange : CryptoExchangeBase
    {
        public HitbtcExchange() : base(CryptoExchangeId.Hitbtc)
        {
        }

        public override async Task GetExchangeData(CancellationToken ct)
        {
            ExchangeData = new ConcurrentDictionary<CryptoCoinId, CryptoCoin>();

            while (!ct.IsCancellationRequested)
            {
                var data = await TickerUrl.GetJsonAsync<List<HitbtcCoin>>(ct).ConfigureAwait(false);

                var hitbtcObject = data;

                foreach (var datum in data)
                {
                    var s = datum.Symbol;
                    if (!s.EndsWith("USD"))
                        continue;
                    var symbol = s.Replace("USD", "");
                    if (symbol == "BCC") symbol = "BCH";
                    //if ( !AllowedSymbols.Contains ( symbol ) ) continue;
                    Update(datum, symbol);
                }

                await Task.Delay(60000).ConfigureAwait(false);



                //foreach (PropertyInfo propertyInfo in bitbnsObject.GetType().GetProperties())
                //{

                //    var propertyName = propertyInfo.Name;
                //    var temp = propertyInfo.GetValue(bitbnsObject);
                //    Update(temp, propertyName.ToUpper(), "Bitbns");

                //}

                //await Task.Delay(60000, ct).ConfigureAwait(false);
            }
        }

        protected override void DeserializeData(dynamic datum, CryptoCoinId id)
        {
            var d = (HitbtcCoin)datum;

            string UsdToInr(decimal amount) => FiatConverter.Convert(amount, FiatCurrency.USD, FiatCurrency.INR).ToString("0.##");

            ExchangeData[id].LowestAskINR = Convert.ToDecimal( UsdToInr(Convert.ToDecimal(d.Ask)));
            ExchangeData[id].HighestBidINR = Convert.ToDecimal(UsdToInr(Convert.ToDecimal(d.Bid)));

            ExchangeData[id].LowestAsk = Convert.ToDecimal(d.Ask);
            ExchangeData[id].HighestBid = Convert.ToDecimal(d.Bid);
            ExchangeData[id].Rate = Convert.ToDecimal(d.Last);
            //Console.WriteLine(ExchangeData);


        }

        #region JSON Structure
        public partial class HitbtcCoin
        {
            [JsonProperty("ask")]
            public string Ask { get; set; }

            [JsonProperty("bid")]
            public string Bid { get; set; }

            [JsonProperty("last")]
            public string Last { get; set; }

            [JsonProperty("open")]
            public string Open { get; set; }

            [JsonProperty("low")]
            public string Low { get; set; }

            [JsonProperty("high")]
            public string High { get; set; }

            [JsonProperty("volume")]
            public string Volume { get; set; }

            [JsonProperty("volumeQuote")]
            public string VolumeQuote { get; set; }

            [JsonProperty("timestamp")]
            public DateTimeOffset Timestamp { get; set; }

            [JsonProperty("symbol")]
            public string Symbol { get; set; }
        }

       

        #endregion
    }
}