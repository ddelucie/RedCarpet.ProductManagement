using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedCarpet.MWS.Feeds.Model;

namespace RedCarpet.MWS.Feeds.Feed
{
	public static class PriceFeedBuilder
	{
		public static AmazonEnvelope Build()
		{

			var amazonEnvelope = new AmazonEnvelope();
			amazonEnvelope.Header = new Header();
			amazonEnvelope.Header.DocumentVersion = "1.01";
			amazonEnvelope.Message = new Message();
			amazonEnvelope.Message.Price = new Price();
			amazonEnvelope.Message.Price.StandardPrice = new OverrideCurrencyAmount() { currency = BaseCurrencyCodeWithDefault.USD };
			amazonEnvelope.MessageType = "Price";

			return amazonEnvelope;
		}


	}


}
