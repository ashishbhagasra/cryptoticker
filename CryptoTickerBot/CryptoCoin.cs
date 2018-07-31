﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using CryptoTickerBot.Data.Domain;
using CryptoTickerBot.Data.Enums;
using CryptoTickerBot.Data.Extensions;
using CryptoTickerBot.Helpers;

namespace CryptoTickerBot
{
	public class CryptoCoin : IEquatable<CryptoCoin>
	{
		public CryptoCoinId Id { get; }
        public string ExchangeName { get; set; }
		public string Symbol { get; }
		public decimal HighestBid { get; set; }
		public decimal LowestAsk { get; set; }

        public decimal HighestBidINR { get; set; }
        public decimal LowestAskINR { get; set; }


        public decimal SellPrice => HighestBid;
		public decimal BuyPrice => LowestAsk;
		public decimal Average => ( BuyPrice + SellPrice ) / 2;
		public decimal Rate { get; set; }
		public decimal Spread => BuyPrice - SellPrice;
		public decimal SpreadPercentange => Average != 0 ? Spread / Average : 0;
		public DateTime Time { get; set; }

		public CryptoCoin (
			string symbol,
			decimal highestBid = 0m, decimal lowestAsk = 0m,
			decimal rate = 0m,
			DateTime? time = null
		)
		{
			Id         = symbol.ToEnum ( CryptoCoinId.NULL );
            ExchangeName = "";
			Symbol     = symbol;
			HighestBid = highestBid;
			LowestAsk  = lowestAsk;

            Rate = rate;
			Time       = time ?? DateTime.Now;
            

        }

        public CryptoCoin(
            string symbol, string exchangeName,
            decimal highestBid = 0m, decimal lowestAsk = 0m,
            decimal rate = 0m,
            DateTime? time = null
        )
        {
            Id = symbol.ToEnum(CryptoCoinId.NULL);
            ExchangeName = exchangeName;
            Symbol = symbol;
            HighestBid = highestBid;
            LowestAsk = lowestAsk;
            Rate = rate;
            Time = time ?? DateTime.Now;
        }

        public CryptoCoin ( CryptoCoinValue coinValue )
		{
			Id         = coinValue.CoinId;
			Symbol     = coinValue.CoinId.ToString ( );
			HighestBid = coinValue.HighestBid;
			LowestAsk  = coinValue.LowestAsk;
			Time       = coinValue.Time;
			Rate       = ( HighestBid + LowestAsk ) / 2m;
		}

		public bool Equals ( CryptoCoin other ) =>
			other != null && Symbol == other.Symbol &&
			HighestBid == other.HighestBid && LowestAsk == other.LowestAsk;

		public CryptoCoinValue ToCryptoCoinValue ( CryptoExchangeId exchangeId ) =>
			new CryptoCoinValue ( Id, exchangeId, LowestAsk, HighestBid, Time );

		[DebuggerStepThrough]
		[Pure]
		public virtual decimal Buy ( decimal amountInUsd ) => amountInUsd / BuyPrice;

		[DebuggerStepThrough]
		[Pure]
		public virtual decimal Sell ( decimal quantity ) => SellPrice * quantity;

		public override bool Equals ( object obj ) => Equals ( obj as CryptoCoin );

		public override int GetHashCode ( ) =>
			-1758840423 + EqualityComparer<string>.Default.GetHashCode ( Symbol );

		[DebuggerStepThrough]
		[Pure]
		public CryptoCoin Clone ( ) =>
			new CryptoCoin ( Symbol, HighestBid, LowestAsk, Rate, Time );

		[DebuggerStepThrough]
		public static bool operator == ( CryptoCoin coin1, CryptoCoin coin2 ) =>
			EqualityComparer<CryptoCoin>.Default.Equals ( coin1, coin2 );

		[DebuggerStepThrough]
		public static bool operator != ( CryptoCoin coin1, CryptoCoin coin2 ) =>
			!( coin1 == coin2 );

		public static PriceChange operator - ( CryptoCoin coin1, CryptoCoin coin2 ) =>
			PriceChange.From ( coin1, coin2 );

		[Pure]
		public override string ToString ( ) =>
			$"{Symbol}: Highest Bid = {HighestBid,-10:C} Lowest Ask = {LowestAsk,-10:C}";

		[Pure]
		public IList<object> ToSheetsRow ( string Exchange) =>
			new List<object> { Exchange, Symbol, LowestAskINR, HighestBidINR,  $"{Time:G}", LowestAsk, HighestBid};
	}
}