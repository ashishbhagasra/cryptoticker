using System.Collections.Generic;
using System.Data.Entity.Migrations;
using CryptoTickerBot.Data.Domain;
using CryptoTickerBot.Data.Enums;
using CryptoTickerBot.Data.Extensions;
using CryptoTickerBot.Data.Persistence;
using NLog;

namespace CryptoTickerBot.Data.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<CtbContext>
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( );

		public Configuration ( )
		{
			AutomaticMigrationsEnabled        = true;
			AutomaticMigrationDataLossAllowed = true;
		}

		protected override void Seed ( CtbContext context )
		{
			Logger.Info ( "Seeding Database" );

            context.Coins.AddOrUpdate(
                new CryptoCoin(CryptoCoinId.BTC, "Bitcoin"),
                new CryptoCoin(CryptoCoinId.ETH, "Ethereum"),
                new CryptoCoin(CryptoCoinId.BCH, "Bitcoin Cash"),
                new CryptoCoin(CryptoCoinId.LTC, "Litecoin"),
                new CryptoCoin(CryptoCoinId.XRP, "Ripple"),
                new CryptoCoin(CryptoCoinId.NEO, "NEO"),
                new CryptoCoin(CryptoCoinId.DASH, "Dash"),
                new CryptoCoin(CryptoCoinId.XMR, "Monero"),
                new CryptoCoin(CryptoCoinId.TRX, "Tron"),
                new CryptoCoin(CryptoCoinId.ETC, "Ethereum Classic"),
                new CryptoCoin(CryptoCoinId.OMG, "OmiseGo"),
                new CryptoCoin(CryptoCoinId.ZEC, "Zcash"),
                new CryptoCoin(CryptoCoinId.XLM, "Stellar"),
                new CryptoCoin(CryptoCoinId.BNB, "Binance Coin"),
                new CryptoCoin(CryptoCoinId.BTG, "Bitcoin Gold"),
                new CryptoCoin(CryptoCoinId.BCD, "Bitcoin Diamond"),
                new CryptoCoin(CryptoCoinId.IOT, "IOTA"),
                new CryptoCoin(CryptoCoinId.DOGE, "Dogecoin"),
                new CryptoCoin(CryptoCoinId.STEEM, "Steem"),
                new CryptoCoin(CryptoCoinId.QTUM, "Qtum"),
                new CryptoCoin(CryptoCoinId.REQ, "Request"),
                


                new CryptoCoin(CryptoCoinId.ZRX, "ZRX"),
        new CryptoCoin(CryptoCoinId.GNT, "GNT"),
        new CryptoCoin(CryptoCoinId.BAT, "BAT"),
        new CryptoCoin(CryptoCoinId.AE, "AE"),
        new CryptoCoin(CryptoCoinId.GAS, "GAS"),
        new CryptoCoin(CryptoCoinId.NANO, "NANO"),
        new CryptoCoin(CryptoCoinId.NCASH, "NCASH"),
        new CryptoCoin(CryptoCoinId.AION, "AION"),
        new CryptoCoin(CryptoCoinId.EOS, "EOS"),
        new CryptoCoin(CryptoCoinId.CMT, "CMT"),
        new CryptoCoin(CryptoCoinId.ONT, "ONT"),
        new CryptoCoin(CryptoCoinId.ZIL, "ZIL"),
        new CryptoCoin(CryptoCoinId.IOST, "IOST"),
        new CryptoCoin(CryptoCoinId.ACT, "ACT"),
        new CryptoCoin(CryptoCoinId.ZCO, "ZCO"),
        new CryptoCoin(CryptoCoinId.SNT, "SNT"),
        new CryptoCoin(CryptoCoinId.POLY, "POLY"),
        new CryptoCoin(CryptoCoinId.ELF, "ELF"),
        new CryptoCoin(CryptoCoinId.REP, "REP"),
        new CryptoCoin(CryptoCoinId.QKC, "QKC"),
        new CryptoCoin(CryptoCoinId.XZC, "XZC"),
        new CryptoCoin(CryptoCoinId.TUSD, "TUSD"),
        new CryptoCoin(CryptoCoinId.KNC, "KNC"),
        new CryptoCoin(CryptoCoinId.CVC, "CVC"),
        new CryptoCoin(CryptoCoinId.ENG, "ENG"),
        new CryptoCoin(CryptoCoinId.MANA, "MANA"),
        new CryptoCoin(CryptoCoinId.SPANK, "SPANK"),
        new CryptoCoin(CryptoCoinId.ICX, "ICX"),
        new CryptoCoin(CryptoCoinId.CND, "CND"),
        new CryptoCoin(CryptoCoinId.GRS, "GRS"),
        new CryptoCoin(CryptoCoinId.DAI, "DAI"),
        new CryptoCoin(CryptoCoinId.TEN, "TEN"),
        new CryptoCoin(CryptoCoinId.STX, "STX"),
        new CryptoCoin(CryptoCoinId.HST, "HST"),
        new CryptoCoin(CryptoCoinId.RAM, "RAM"),
        new CryptoCoin(CryptoCoinId.NPXS, "NPXS"),
        new CryptoCoin(CryptoCoinId.USDT, "USDT"),
        new CryptoCoin(CryptoCoinId.EOSD, "EOSD"),
        new CryptoCoin(CryptoCoinId.XVG, "XVG"),
        new CryptoCoin(CryptoCoinId.CPX, "CPX"),
        new CryptoCoin(CryptoCoinId.BLZ, "BLZ"),
        new CryptoCoin(CryptoCoinId.ADA, "ADA"),
        new CryptoCoin(CryptoCoinId.CLOAK, "CLOAK"),
        new CryptoCoin(CryptoCoinId.DBC, "DBC"),
        new CryptoCoin(CryptoCoinId.DENT, "DENT"),
        new CryptoCoin(CryptoCoinId.DGB, "DGB"),
        new CryptoCoin(CryptoCoinId.DGD, "DGD"),
        new CryptoCoin(CryptoCoinId.EFX, "EFX"),
        new CryptoCoin(CryptoCoinId.ETN, "ETN"),
        new CryptoCoin(CryptoCoinId.KMD, "KMD"),
        new CryptoCoin(CryptoCoinId.LET, "LET"),
        new CryptoCoin(CryptoCoinId.LSK, "LSK"),
        new CryptoCoin(CryptoCoinId.LOOM, "LOOM"),
        new CryptoCoin(CryptoCoinId.LRC, "LRC"),
        new CryptoCoin(CryptoCoinId.XEM, "XEM"),
        new CryptoCoin(CryptoCoinId.NEXO, "NEXO"),
        new CryptoCoin(CryptoCoinId.SOUL, "SOUL"),
        new CryptoCoin(CryptoCoinId.POWR, "POWR"),
        new CryptoCoin(CryptoCoinId.QLC, "QLC"),
        new CryptoCoin(CryptoCoinId.RPX, "RPX"),
        new CryptoCoin(CryptoCoinId.SIA, "SIA"),
        new CryptoCoin(CryptoCoinId.STORM, "STORM"),
        new CryptoCoin(CryptoCoinId.SUB, "SUB"),
        new CryptoCoin(CryptoCoinId.VEN, "VEN"),
        new CryptoCoin(CryptoCoinId.WPR, "WPR"),
        new CryptoCoin(CryptoCoinId.WAN, "WAN"),
        new CryptoCoin(CryptoCoinId.WAVES, "WAVES"),
        new CryptoCoin(CryptoCoinId.XRB, "XRB"),
        new CryptoCoin(CryptoCoinId.SC, "SC")
            );

            context.Exchanges.AddIfNotExists (
				new CryptoExchange
				(
					CryptoExchangeId.Binance,
					"Binance",
					"https://www.binance.com/",
					"wss://stream2.binance.com:9443/ws/!ticker@arr@3000ms",
					0.1m,
					0.1m,
					withdrawalFees: new Dictionary<CryptoCoinId, decimal>
					{
						[CryptoCoinId.BTC] = 0.001m,
						[CryptoCoinId.ETH] = 0.01m,
						[CryptoCoinId.BCH] = 0.001m,
						[CryptoCoinId.LTC] = 0.01m
					},
					depositFees: new Dictionary<CryptoCoinId, decimal>
					{
						[CryptoCoinId.BTC] = 0,
						[CryptoCoinId.ETH] = 0,
						[CryptoCoinId.BCH] = 0,
						[CryptoCoinId.LTC] = 0
					}
				),
				CryptoExchangeId.Binance
			);

			context.Exchanges.AddIfNotExists (
				new CryptoExchange
				(
					CryptoExchangeId.BitBay,
					"BitBay",
					"https://bitbay.net/en",
					"https://api.bitbay.net/rest/trading/ticker",
					0.3m,
					0.3m,
					withdrawalFees: new Dictionary<CryptoCoinId, decimal>
					{
						[CryptoCoinId.BTC] = 0.0009m,
						[CryptoCoinId.ETH] = 0.00126m,
						[CryptoCoinId.BCH] = 0.0006m,
						[CryptoCoinId.LTC] = 0.005m
					},
					depositFees: new Dictionary<CryptoCoinId, decimal>
					{
						[CryptoCoinId.BTC] = 0,
						[CryptoCoinId.ETH] = 0,
						[CryptoCoinId.BCH] = 0,
						[CryptoCoinId.LTC] = 0
					}
				),
				CryptoExchangeId.BitBay
			);

			context.Exchanges.AddIfNotExists (
				new CryptoExchange
				(
					CryptoExchangeId.Coinbase,
					"Coinbase",
					"https://www.coinbase.com/",
					"wss://ws-feed.gdax.com/",
					0.3m,
					0.3m,
					withdrawalFees: new Dictionary<CryptoCoinId, decimal>
					{
						[CryptoCoinId.BTC] = 0.001m,
						[CryptoCoinId.ETH] = 0.003m,
						[CryptoCoinId.BCH] = 0.001m,
						[CryptoCoinId.LTC] = 0.01m
					},
					depositFees: new Dictionary<CryptoCoinId, decimal>
					{
						[CryptoCoinId.BTC] = 0,
						[CryptoCoinId.ETH] = 0,
						[CryptoCoinId.BCH] = 0,
						[CryptoCoinId.LTC] = 0
					}
				),
				CryptoExchangeId.Coinbase
			);

			context.Exchanges.AddIfNotExists (
				new CryptoExchange
				(
					CryptoExchangeId.CoinDelta,
					"CoinDelta",
					"https://coindelta.com/",
					"https://coindelta.com/api/v1/public/getticker/",
					0.3m,
					0.3m,
					withdrawalFees: new Dictionary<CryptoCoinId, decimal>
					{
						[CryptoCoinId.BTC] = 0.001m,
						[CryptoCoinId.ETH] = 0.001m,
						[CryptoCoinId.BCH] = 0.001m,
						[CryptoCoinId.LTC] = 0.002m,
                        [CryptoCoinId.ZEC] = 0.005m,
                        [CryptoCoinId.XRP] = 0.01m,
                        [CryptoCoinId.QTUM] = 0.01m,
                        [CryptoCoinId.OMG] = 0.1m,
                        [CryptoCoinId.EOS] = 0.5m,
                        [CryptoCoinId.ICX] = 1,
                        [CryptoCoinId.AION] = 1,
                        [CryptoCoinId.ENG] = 15,
                        [CryptoCoinId.BAT] = 10,
                        [CryptoCoinId.CVC] = 10,
                        [CryptoCoinId.GNT] = 10,
                        [CryptoCoinId.KNC] = 2,
                        [CryptoCoinId.SPANK] = 20,
                        [CryptoCoinId.MANA] = 30,
                        [CryptoCoinId.CND] = 30,
                        [CryptoCoinId.ZRX] = 5,
                        [CryptoCoinId.ZIL] = 50,
                        [CryptoCoinId.TRX] = 60,
                        [CryptoCoinId.GAS] = 0,
                        [CryptoCoinId.NEO] = 0,
                        [CryptoCoinId.QKC] = 15,
                        [CryptoCoinId.AE] = 1.2m,
                        [CryptoCoinId.ACT] = 1,
                        [CryptoCoinId.GRS] = 1,
                        [CryptoCoinId.USDT] = 10
                    },
					depositFees: new Dictionary<CryptoCoinId, decimal>
					{
						[CryptoCoinId.BTC] = 0,
						[CryptoCoinId.ETH] = 0,
						[CryptoCoinId.BCH] = 0,
						[CryptoCoinId.LTC] = 0
					}
				),
				CryptoExchangeId.CoinDelta
			);

			context.Exchanges.AddIfNotExists (
				new CryptoExchange
				(
					CryptoExchangeId.Koinex,
					"Koinex",
					"https://koinex.in/",
					"https://koinex.in/api/ticker",
					0.25m,
					withdrawalFees: new Dictionary<CryptoCoinId, decimal>
					{
						[CryptoCoinId.BTC] = 0.005m,
						[CryptoCoinId.ETH] = 0.003m,
						[CryptoCoinId.BCH] = 0.001m,
						[CryptoCoinId.LTC] = 0.01m,
                        [CryptoCoinId.ZRX] = 5,
                        [CryptoCoinId.GNT] = 10,
                        [CryptoCoinId.BAT] = 10,
                        [CryptoCoinId.TRX] = 60,
                        [CryptoCoinId.XLM] = 0.02m,
                        [CryptoCoinId.NEO] = 0,
                        [CryptoCoinId.GAS] = 0,
                        [CryptoCoinId.XRB] = 0.05m,
                        [CryptoCoinId.EOS] = 0.4m,
                        [CryptoCoinId.ONT] = 0,
                        [CryptoCoinId.ZIL] = 40,
                        [CryptoCoinId.ZCO] = 50,
                        [CryptoCoinId.POLY] = 5,
                        [CryptoCoinId.BTG] = 0.001m,
                        [CryptoCoinId.IOST] = 60,
                        [CryptoCoinId.OMG] = 0.25m,
                        [CryptoCoinId.NCASH] = 100,
                        [CryptoCoinId.AION] = 1.5m,
                        [CryptoCoinId.AE] = 2,
                        [CryptoCoinId.XRP] = 0.25m,
                        [CryptoCoinId.REQ] = 15,
                        [CryptoCoinId.ELF] = 3.5m,
                        [CryptoCoinId.REP] = 0.1m,
                        [CryptoCoinId.ACT] = 0.2m,
                        [CryptoCoinId.QKC] = 15
                    },
					depositFees: new Dictionary<CryptoCoinId, decimal>
					{
						[CryptoCoinId.BTC] = 0,
						[CryptoCoinId.ETH] = 0,
						[CryptoCoinId.BCH] = 0,
						[CryptoCoinId.LTC] = 0
					}
				),
				CryptoExchangeId.Koinex
			);

            context.Exchanges.AddIfNotExists(
            new CryptoExchange
            (
                CryptoExchangeId.Bitbns,
                "Bitbns",
                "https://bitbns.com/",
                "https://bitbns.com/order/getTickerWithVolume",
                0.25m,
                withdrawalFees: new Dictionary<CryptoCoinId, decimal>
                {
                    [CryptoCoinId.BTC] = 0.0005m,
                    [CryptoCoinId.ETH] = 0.002m,
                    [CryptoCoinId.BCH] = 0.001m,
                    [CryptoCoinId.LTC] = 0.01m,
                    [CryptoCoinId.ZRX] = 3,
                    [CryptoCoinId.ACT] = 0.1m,
                    [CryptoCoinId.BLZ] = 3,
                    [CryptoCoinId.ADA] = 1,
                    [CryptoCoinId.DGD] = 1,
                    [CryptoCoinId.DOGE] = 2,
                    [CryptoCoinId.ETN] = 1,
                    [CryptoCoinId.EOS] = 0.5m,
                    [CryptoCoinId.GAS] = 0,
                    [CryptoCoinId.ICX] = 0.5m,
                    [CryptoCoinId.LRC] = 3,
                    [CryptoCoinId.XMR] = 0.02m,
                    [CryptoCoinId.XEM] = 1,
                    [CryptoCoinId.NEO] = 0,
                    [CryptoCoinId.NCASH] = 220,
                    [CryptoCoinId.OMG] = 0.2m,
                    [CryptoCoinId.ONT] = 0.1m,
                    [CryptoCoinId.POLY] = 8,
                    [CryptoCoinId.POWR] = 8,
                    [CryptoCoinId.QLC] = 0.1m,
                    [CryptoCoinId.RPX] = 0.1m,
                    [CryptoCoinId.REQ] = 10,
                    [CryptoCoinId.XRP] = 0.1m,
                    [CryptoCoinId.SC] = 1,
                    [CryptoCoinId.XLM] = 0.01m,
                    [CryptoCoinId.SUB] = 3,
                    [CryptoCoinId.TRX] = 50,
                    [CryptoCoinId.VEN] = 0.5m,
                    [CryptoCoinId.XVG] = 2,
                    [CryptoCoinId.WAN] = 0.01m,
                    [CryptoCoinId.WAVES] = 0.01m,
                    [CryptoCoinId.WPR] = 30,
                    [CryptoCoinId.ZIL] = 50,
                    [CryptoCoinId.REP] = 0.1m,
                    [CryptoCoinId.QKC] = 20,
                    [CryptoCoinId.REP] = 0.1m,
                    [CryptoCoinId.QTUM] = 0.01m,
                    [CryptoCoinId.GNT] = 8,
                    [CryptoCoinId.ACT] = 0.2m,
                    [CryptoCoinId.ETC] = 0.1m,
                    [CryptoCoinId.USDT] = 10,
                    [CryptoCoinId.GRS] = 1
                },
                depositFees: new Dictionary<CryptoCoinId, decimal>
                {
                    [CryptoCoinId.BTC] = 0,
                    [CryptoCoinId.ETH] = 0,
                    [CryptoCoinId.BCH] = 0,
                    [CryptoCoinId.LTC] = 0
                }
            ),
            CryptoExchangeId.Bitbns
        );

            context.Exchanges.AddIfNotExists (
				new CryptoExchange
				(
					CryptoExchangeId.Kraken,
					"Kraken",
					"https://www.kraken.com/",
					"https://api.kraken.com/0/public/Ticker?pair=XBTUSD,BCHUSD,ETHUSD,LTCUSD",
					0.26m,
					0.26m,
					withdrawalFees: new Dictionary<CryptoCoinId, decimal>
					{
						[CryptoCoinId.BTC] = 0.0025m,
						[CryptoCoinId.ETH] = 0.005m,
						[CryptoCoinId.BCH] = 0.001m,
						[CryptoCoinId.LTC] = 0.02m
					},
					depositFees: new Dictionary<CryptoCoinId, decimal>
					{
						[CryptoCoinId.BTC] = 0,
						[CryptoCoinId.ETH] = 0,
						[CryptoCoinId.BCH] = 0,
						[CryptoCoinId.LTC] = 0
					}
				),
				CryptoExchangeId.Kraken
			);

            context.Exchanges.AddIfNotExists(
               new CryptoExchange
               (
                   CryptoExchangeId.Hitbtc,
                   "Hitbtc",
                   "https://www.hitbtc.com/",
                   "https://api.hitbtc.com/api/2/public/ticker",
                   0.26m,
                   0.26m,
                   withdrawalFees: new Dictionary<CryptoCoinId, decimal>
                   {
                       [CryptoCoinId.BTC] = 0.0025m,
                       [CryptoCoinId.ETH] = 0.005m,
                       [CryptoCoinId.BCH] = 0.001m,
                       [CryptoCoinId.LTC] = 0.02m
                   },
                   depositFees: new Dictionary<CryptoCoinId, decimal>
                   {
                       [CryptoCoinId.BTC] = 0,
                       [CryptoCoinId.ETH] = 0,
                       [CryptoCoinId.BCH] = 0,
                       [CryptoCoinId.LTC] = 0
                   }
               ),
               CryptoExchangeId.Hitbtc
           );


            context.Exchanges.AddIfNotExists (
				new CryptoExchange
				(
					CryptoExchangeId.Bitstamp,
					"Bitstamp",
					"https://www.bitstamp.net/",
					"wss://ws.pusherapp.com/app/de504dc5763aeef9ff52?protocol=7&client=js&version=2.1.6&flash=false",
					0m,
					0m,
					withdrawalFees: new Dictionary<CryptoCoinId, decimal>
					{
						[CryptoCoinId.BTC] = 0m,
						[CryptoCoinId.ETH] = 0m,
						[CryptoCoinId.BCH] = 0m,
						[CryptoCoinId.LTC] = 0m
					},
					depositFees: new Dictionary<CryptoCoinId, decimal>
					{
						[CryptoCoinId.BTC] = 0,
						[CryptoCoinId.ETH] = 0,
						[CryptoCoinId.BCH] = 0,
						[CryptoCoinId.LTC] = 0
					}
				),
				CryptoExchangeId.Bitstamp
			);

			context.Exchanges.AddIfNotExists (
				new CryptoExchange
				(
					CryptoExchangeId.Zebpay,
					"Zebpay",
					"https://www.zebpay.com/",
					"https://www.zebapi.com/api/v1/market/ticker-new/",
					0,
					0,
					withdrawalFees: new Dictionary<CryptoCoinId, decimal>
					{
						[CryptoCoinId.BTC] = 0.0009m,
						[CryptoCoinId.ETH] = 0.015m,
						[CryptoCoinId.BCH] = 0.001m,
						[CryptoCoinId.LTC] = 0.01m,
                        [CryptoCoinId.OMG] = 0.2m,
                        [CryptoCoinId.EOS] = 0.2m,
                        [CryptoCoinId.XRP] = 0.25m,
                        [CryptoCoinId.GNT] = 10,
                        [CryptoCoinId.ZRX] = 5,
                        [CryptoCoinId.REP] = 0.1m,
                        [CryptoCoinId.KNC] = 2,
                        [CryptoCoinId.BAT] = 11,
                        [CryptoCoinId.VEN] = 1,
                        [CryptoCoinId.AE] = 2,
                        [CryptoCoinId.IOST] = 100,
                        [CryptoCoinId.ZIL] = 40,
                        [CryptoCoinId.CMT] = 16,
                        [CryptoCoinId.NCASH] = 200

                    },
					depositFees: new Dictionary<CryptoCoinId, decimal>
					{
						[CryptoCoinId.BTC] = 0,
						[CryptoCoinId.ETH] = 0,
						[CryptoCoinId.BCH] = 0,
						[CryptoCoinId.LTC] = 0
					}
				),
				CryptoExchangeId.Zebpay
			);

			context.TeleBotUsers.AddIfNotExists (
				new TeleBotUser ( 441308948, UserRole.Owner, "cryptotrady", "Ashish", "Bhagasra" ),
                441308948
            );
		}
	}
}