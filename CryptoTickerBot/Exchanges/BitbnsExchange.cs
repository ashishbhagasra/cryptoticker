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
    public class BitbnsExchange : CryptoExchangeBase
    {
        public BitbnsExchange() : base(CryptoExchangeId.Bitbns)
        {
        }

        public override async Task GetExchangeData(CancellationToken ct)
        {
            ExchangeData = new ConcurrentDictionary<CryptoCoinId, CryptoCoin>();

            while (!ct.IsCancellationRequested)
            {
                var data = await TickerUrl.GetJsonAsync<BitbnsCoin>(ct).ConfigureAwait(false);

                var bitbnsObject = data;

                foreach (PropertyInfo propertyInfo in bitbnsObject.GetType().GetProperties())
                {

                    var propertyName = propertyInfo.Name;
                    var temp = propertyInfo.GetValue(bitbnsObject);
                    Update(temp, propertyName.ToUpper(), "Bitbns");

                }

                await Task.Delay(60000, ct).ConfigureAwait(false);
            }
        }

        protected override void DeserializeData(dynamic data, CryptoCoinId id)
        {
            var stats = (CoinStats)data;
            decimal InrToUsd(decimal amount) => FiatConverter.Convert(amount, FiatCurrency.INR, FiatCurrency.USD);

            ExchangeData[id].LowestAsk = InrToUsd(Convert.ToDecimal(stats.LowestSellBid) );
            ExchangeData[id].HighestBid = InrToUsd(Convert.ToDecimal(stats.HighestBuyBid ));
            ExchangeData[id].Rate = InrToUsd(Convert.ToDecimal(stats.LastTradedPrice ));

            ExchangeData[id].LowestAskINR = Convert.ToDecimal(stats.LowestSellBid);
            ExchangeData[id].HighestBidINR = Convert.ToDecimal(stats.HighestBuyBid);



        }

        #region JSON Structure
        public partial class BitbnsCoin
        {
            [JsonProperty("BTC")]
            public CoinStats Btc { get; set; }

            [JsonProperty("XRP")]
            public CoinStats Xrp { get; set; }

            [JsonProperty("NEO")]
            public CoinStats Neo { get; set; }

            [JsonProperty("GAS")]
            public CoinStats Gas { get; set; }

            [JsonProperty("ETH")]
            public CoinStats Eth { get; set; }

            [JsonProperty("XLM")]
            public CoinStats Xlm { get; set; }

            [JsonProperty("RPX")]
            public CoinStats Rpx { get; set; }

            [JsonProperty("DBC")]
            public CoinStats Dbc { get; set; }

            [JsonProperty("LTC")]
            public CoinStats Ltc { get; set; }

            [JsonProperty("XMR")]
            public CoinStats Xmr { get; set; }

            [JsonProperty("DASH")]
            public CoinStats Dash { get; set; }

            [JsonProperty("DOGE")]
            public CoinStats Doge { get; set; }

            [JsonProperty("BCH")]
            public CoinStats Bch { get; set; }

            [JsonProperty("SC")]
            public CoinStats Sc { get; set; }

            [JsonProperty("TRX")]
            public CoinStats Trx { get; set; }

            [JsonProperty("ETN")]
            public CoinStats Etn { get; set; }

            [JsonProperty("ONT")]
            public CoinStats Ont { get; set; }

            [JsonProperty("ZIL")]
            public CoinStats Zil { get; set; }

            [JsonProperty("EOS")]
            public CoinStats Eos { get; set; }

            [JsonProperty("POLY")]
            public CoinStats Poly { get; set; }

            [JsonProperty("DGB")]
            public CoinStats Dgb { get; set; }

            [JsonProperty("NCASH")]
            public CoinStats Ncash { get; set; }

            [JsonProperty("ADA")]
            public CoinStats Ada { get; set; }

            [JsonProperty("ICX")]
            public CoinStats Icx { get; set; }

            [JsonProperty("VEN")]
            public CoinStats Ven { get; set; }

            [JsonProperty("OMG")]
            public CoinStats Omg { get; set; }

            [JsonProperty("REQ")]
            public CoinStats Req { get; set; }

            [JsonProperty("DGD")]
            public CoinStats Dgd { get; set; }

            [JsonProperty("QLC")]
            public CoinStats Qlc { get; set; }

            [JsonProperty("POWR")]
            public CoinStats Powr { get; set; }

            [JsonProperty("WPR")]
            public CoinStats Wpr { get; set; }

            [JsonProperty("WAVES")]
            public CoinStats Waves { get; set; }

            [JsonProperty("WAN")]
            public CoinStats Wan { get; set; }

            [JsonProperty("ACT")]
            public CoinStats Act { get; set; }

            [JsonProperty("XEM")]
            public CoinStats Xem { get; set; }

            [JsonProperty("XVG")]
            public CoinStats Xvg { get; set; }

            [JsonProperty("BLZ")]
            public CoinStats Blz { get; set; }

            [JsonProperty("SUB")]
            public CoinStats Sub { get; set; }

            [JsonProperty("LRC")]
            public CoinStats Lrc { get; set; }

            [JsonProperty("NEXO")]
            public CoinStats Nexo { get; set; }

            [JsonProperty("EFX")]
            public CoinStats Efx { get; set; }

            [JsonProperty("CPX")]
            public CoinStats Cpx { get; set; }

            [JsonProperty("ZRX")]
            public CoinStats Zrx { get; set; }

            [JsonProperty("REP")]
            public CoinStats Rep { get; set; }

            [JsonProperty("LOOM")]
            public CoinStats Loom { get; set; }

            [JsonProperty("EOSD")]
            public CoinStats Eosd { get; set; }

            [JsonProperty("STORM")]
            public CoinStats Storm { get; set; }

            [JsonProperty("GNT")]
            public CoinStats Gnt { get; set; }

            [JsonProperty("QTUM")]
            public CoinStats Qtum { get; set; }

            [JsonProperty("QKC")]
            public CoinStats Qkc { get; set; }

            [JsonProperty("LSK")]
            public CoinStats Lsk { get; set; }

            [JsonProperty("NPXS")]
            public CoinStats Npxs { get; set; }

            [JsonProperty("USDT")]
            public CoinStats Usdt { get; set; }

            [JsonProperty("ETC")]
            public CoinStats Etc { get; set; }

            [JsonProperty("DENT")]
            public CoinStats Dent { get; set; }

            [JsonProperty("CLOAK")]
            public CoinStats Cloak { get; set; }

            [JsonProperty("KMD")]
            public CoinStats Kmd { get; set; }

            [JsonProperty("GRS")]
            public CoinStats Grs { get; set; }

            [JsonProperty("RAM")]
            public CoinStats Ram { get; set; }

            [JsonProperty("LET")]
            public CoinStats Let { get; set; }

            [JsonProperty("SOUL")]
            public CoinStats Soul { get; set; }

            [JsonProperty("TST")]
            public CoinStats Tst { get; set; }
        }

        public partial class CoinStats
        {
            [JsonProperty("highest_buy_bid")]
            public double HighestBuyBid { get; set; }

            [JsonProperty("lowest_sell_bid")]
            public double LowestSellBid { get; set; }

            [JsonProperty("last_traded_price")]
            public double LastTradedPrice { get; set; }

            [JsonProperty("yes_price")]
            public double YesPrice { get; set; }

            [JsonProperty("volume")]
            public CoinVolume Volume { get; set; }
        }

        public partial class CoinVolume
        {
            [JsonProperty("max")]
            public double Max { get; set; }

            [JsonProperty("min")]
            public double Min { get; set; }

            [JsonProperty("volume")]
            public double Volume { get; set; }

            [JsonProperty("rate", NullValueHandling = NullValueHandling.Ignore)]
            public string Rate { get; set; }
        }


        #endregion
    }
}