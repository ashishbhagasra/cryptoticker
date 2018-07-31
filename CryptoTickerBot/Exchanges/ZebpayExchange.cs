using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoTickerBot.Data.Enums;
using CryptoTickerBot.Exchanges.Core;
using CryptoTickerBot.Helpers;
using Flurl.Http;
using Newtonsoft.Json;

namespace CryptoTickerBot.Exchanges
{
	public class ZebpayExchange : CryptoExchangeBase
	{
		public ZebpayExchange ( ) : base ( CryptoExchangeId.Zebpay )
		{
		}

		public override async Task GetExchangeData ( CancellationToken ct )
		{
			ExchangeData = new ConcurrentDictionary<CryptoCoinId, CryptoCoin> ( );

			var tickers = new List<(string symbol, string url)>
			{
				( "BTC", "https://www.zebapi.com/api/v1/market/ticker-new/btc/inr" ),
				( "ETH", "https://www.zebapi.com/api/v1/market/ticker-new/eth/inr" ),
				( "LTC", "https://www.zebapi.com/api/v1/market/ticker-new/ltc/inr" ),
				( "BCH", "https://www.zebapi.com/api/v1/market/ticker-new/bch/inr" ),

                ( "TRX", "https://www.zebapi.com/api/v1/market/ticker-new/trx/inr" ),
                ( "NCASH", "https://www.zebapi.com/api/v1/market/ticker-new/ncash/inr" ),
                ( "VEN", "https://www.zebapi.com/api/v1/market/ticker-new/ven/inr" ),
                ( "XRP", "https://www.zebapi.com/api/v1/market/ticker-new/xrp/inr" ),
                ( "IOST", "https://www.zebapi.com/api/v1/market/ticker-new/iost/inr" ),
                ( "EOS", "https://www.zebapi.com/api/v1/market/ticker-new/eos/inr" ),
                ( "REP", "https://www.zebapi.com/api/v1/market/ticker-new/rep/inr" ),
                ( "ZIL", "https://www.zebapi.com/api/v1/market/ticker-new/zil/inr" ),
                ( "BAT", "https://www.zebapi.com/api/v1/market/ticker-new/bat/inr" ),
                ( "KNC", "https://www.zebapi.com/api/v1/market/ticker-new/knc/inr" ),
                ( "ZRX", "https://www.zebapi.com/api/v1/market/ticker-new/zrx/inr" ),
                ( "GNT", "https://www.zebapi.com/api/v1/market/ticker-new/gnt/inr" ),
                ( "AE", "https://www.zebapi.com/api/v1/market/ticker-new/ae/inr" ),
                ( "CMT", "https://www.zebapi.com/api/v1/market/ticker-new/cmt/inr" ),
                ( "OMG", "https://www.zebapi.com/api/v1/market/ticker-new/omg/inr" )


            };

			while ( !ct.IsCancellationRequested )
				foreach ( var ticker in tickers )
				{
					var symbol = ticker.symbol;
					try
					{
						var data = await ticker.url.GetJsonAsync<ZebpayCoin> ( ct ).ConfigureAwait ( false );

						Update ( data, symbol );
					}
					catch ( FlurlHttpException e )
					{
						if ( e.InnerException is TaskCanceledException )
							throw e.InnerException;
					}

					await Task.Delay ( 2000, ct ).ConfigureAwait ( false );
				}
		}

		protected override void DeserializeData ( dynamic data, CryptoCoinId id )
		{
			decimal InrToUsd ( dynamic amount ) =>
				FiatConverter.Convert ( (decimal) amount, FiatCurrency.INR, FiatCurrency.USD );

			ExchangeData[id].LowestAsk  = InrToUsd(Convert.ToDecimal(data.Buy));
			ExchangeData[id].HighestBid = InrToUsd (Convert.ToDecimal(data.Sell));
			ExchangeData[id].Rate       = InrToUsd ( Convert.ToDecimal( data.Market ));

            ExchangeData[id].LowestAskINR = Convert.ToDecimal(data.Buy);
            ExchangeData[id].HighestBidINR = Convert.ToDecimal(data.Sell);


        }

        public partial class ZebpayCoin  
        {
            [JsonProperty("volume")]
            public double Volume { get; set; }

            [JsonProperty("24hoursHigh")]
            public string The24HoursHigh { get; set; }

            [JsonProperty("24hoursLow")]
            public string The24HoursLow { get; set; }

            [JsonProperty("buy")]
            public string Buy { get; set; }

            [JsonProperty("sell")]
            public string Sell { get; set; }

            [JsonProperty("market")]
            public string Market { get; set; }

            [JsonProperty("pricechange")]
            public string Pricechange { get; set; }

            [JsonProperty("pair")]
            public string Pair { get; set; }

            [JsonProperty("virtualCurrency")]
            public string VirtualCurrency { get; set; }

            [JsonProperty("currency")]
            public string Currency { get; set; }
        }
    }
}