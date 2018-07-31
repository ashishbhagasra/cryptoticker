using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoTickerBot.Data.Enums;
using CryptoTickerBot.Exchanges.Core;
using CryptoTickerBot.Helpers;
using Newtonsoft.Json;
using NLog;
using static Newtonsoft.Json.JsonConvert;
using static CryptoTickerBot.Helpers.Utility;
using System.Reflection;

namespace CryptoTickerBot.Exchanges
{
	public class KoinexExchange : CryptoExchangeBase
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( );

		public KoinexExchange ( ) : base ( CryptoExchangeId.Koinex )
		{
		}

		public override async Task GetExchangeData ( CancellationToken ct )
		{
			ExchangeData = new ConcurrentDictionary<CryptoCoinId, CryptoCoin> ( );

			while ( !ct.IsCancellationRequested )
			{
				try
				{
					var json = DownloadWebPage ( TickerUrl );
					var data = DeserializeObject<Root> ( json );

                    var koinexObject = data.Stats.Inr;

                    foreach (PropertyInfo propertyInfo in koinexObject.GetType().GetProperties())
                    {
                       
                        var propertyName = propertyInfo.Name;
                        var temp = propertyInfo.GetValue(koinexObject);
                        Update(temp, propertyName.ToUpper(), "Koinex");

                    }

                    Console.WriteLine(ExchangeData);
                    await Task.Delay(60000, ct).ConfigureAwait(false);
                   
                }
				catch ( Exception e )
				{
					Logger.Error ( e );
				}

				await Task.Delay ( 2000, ct ).ConfigureAwait ( false );
			}
		}

		protected override void DeserializeData ( dynamic data, CryptoCoinId id )
		{
			var stats = (CoinStats) data;
			decimal InrToUsd ( decimal amount ) => FiatConverter.Convert ( amount, FiatCurrency.INR, FiatCurrency.USD );

			ExchangeData[id].LowestAsk  = InrToUsd ( stats.LowestAsk ?? 0 );
			ExchangeData[id].HighestBid = InrToUsd ( stats.HighestBid ?? 0 );
			ExchangeData[id].Rate       = InrToUsd ( stats.LastTradedPrice ?? 0 );

            ExchangeData[id].LowestAskINR = Convert.ToDecimal( stats.LowestAsk);
            ExchangeData[id].HighestBidINR = Convert.ToDecimal( stats.HighestBid);

            Console.WriteLine(ExchangeData);
		}

		#region JSON Structure

		public class Root
		{
			[JsonProperty ( "prices" )]
			public Prices Prices { get; set; }

			[JsonProperty ( "stats" )]
			public Stats Stats { get; set; }
		}

		public class Prices
		{
			[JsonProperty ( "inr" )]
			public Dictionary<string, decimal> Inr { get; set; }

			[JsonProperty ( "bitcoin" )]
			public Dictionary<string, decimal> Bitcoin { get; set; }

			[JsonProperty ( "ether" )]
			public Dictionary<string, decimal> Ether { get; set; }

			[JsonProperty ( "ripple" )]
			public Dictionary<string, decimal> Ripple { get; set; }
		}

		public class Stats
		{
			[JsonProperty ( "inr" )]
			public AllCoinStats Inr { get; set; }

			[JsonProperty ( "bitcoin" )]
			public AllCoinStats Bitcoin { get; set; }

			[JsonProperty ( "ether" )]
			public AllCoinStats Ether { get; set; }

			[JsonProperty ( "ripple" )]
			public AllCoinStats Ripple { get; set; }
		}

		public class CoinStats
		{
			[JsonProperty ( "currency_full_form" )]
			public string CurrencyFullForm { get; set; }

			[JsonProperty ( "currency_short_form" )]
			public string CurrencyShortForm { get; set; }

			[JsonProperty ( "per_change" )]
			public string PerChange { get; set; }

			[JsonProperty ( "last_traded_price" )]
			public decimal? LastTradedPrice { get; set; }

			[JsonProperty ( "lowest_ask" )]
			public decimal? LowestAsk { get; set; }

			[JsonProperty ( "highest_bid" )]
			public decimal? HighestBid { get; set; }

			[JsonProperty ( "min_24hrs" )]
			public decimal? Min24Hrs { get; set; }

			[JsonProperty ( "max_24hrs" )]
			public decimal? Max24Hrs { get; set; }

			[JsonProperty ( "vol_24hrs" )]
			public decimal? Vol24Hrs { get; set; }
		}

		public class AllCoinStats
		{
            [JsonProperty("ETH")]
            public CoinStats ETH { get; set; }
            [JsonProperty("BTC")]
            public CoinStats BTC { get; set; }
            [JsonProperty("LTC")]
            public CoinStats LTC { get; set; }
            [JsonProperty("XRP")]
            public CoinStats XRP { get; set; }
            [JsonProperty("BCH")]
            public CoinStats BCH { get; set; }
            [JsonProperty("OMG")]
            public CoinStats OMG { get; set; }
            [JsonProperty("REQ")]
            public CoinStats REQ { get; set; }
            [JsonProperty("ZRX")]
            public CoinStats ZRX { get; set; }
            [JsonProperty("GNT")]
            public CoinStats GNT { get; set; }
            [JsonProperty("BAT")]
            public CoinStats BAT { get; set; }
            [JsonProperty("AE")]
            public CoinStats AE { get; set; }
            [JsonProperty("TRX")]
            public CoinStats TRX { get; set; }
            [JsonProperty("XLM")]
            public CoinStats XLM { get; set; }
            [JsonProperty("NEO")]
            public CoinStats NEO { get; set; }
            [JsonProperty("GAS")]
            public CoinStats GAS { get; set; }
            [JsonProperty("XRB")]
            public CoinStats XRB { get; set; }
            [JsonProperty("NCASH")]
            public CoinStats NCASH { get; set; }
            [JsonProperty("AION")]
            public CoinStats AION { get; set; }
            [JsonProperty("EOS")]
            public CoinStats EOS { get; set; }
            [JsonProperty("CMT")]
            public CoinStats CMT { get; set; }
            [JsonProperty("ONT")]
            public CoinStats ONT { get; set; }
            [JsonProperty("ZIL")]
            public CoinStats ZIL { get; set; }
            [JsonProperty("IOST")]
            public CoinStats IOST { get; set; }
            [JsonProperty("ACT")]
            public CoinStats ACT { get; set; }
            [JsonProperty("ZCO")]
            public CoinStats ZCO { get; set; }
            [JsonProperty("SNT")]
            public CoinStats SNT { get; set; }
            [JsonProperty("POLY")]
            public CoinStats POLY { get; set; }
            [JsonProperty("ELF")]
            public CoinStats ELF { get; set; }
            [JsonProperty("REP")]
            public CoinStats REP { get; set; }
            [JsonProperty("QKC")]
            public CoinStats QKC { get; set; }
            [JsonProperty("XZC")]
            public CoinStats XZC { get; set; }
            [JsonProperty("TUSD")]
            public CoinStats TUSD { get; set; }

        }

		#endregion
	}
}