using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedCarpet.MWS.Feeds.Feed
{
	public static class PriceFeedBuilder
	{
		public static PriceFeed Build()
		{

			var priceFeed = new PriceFeed();
			priceFeed.MessageType = "Price";
			priceFeed.Header = new Header();
			priceFeed.Messages = new List<Message>();
			return priceFeed;
		}

		public static Message BuildMessage()
		{
			Message message = new Message();
			message.Price = new Price();
			message.Price.StandardPrice = new StandardPrice();
			message.Price.StandardPrice.Currency = "USD";

			return message;
		}
	}
}
