using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace RedCarpet.MWS.Feeds
{

	[XmlRoot(ElementName = "Header")]
	public class Header
	{
		[XmlElement(ElementName = "DocumentVersion")]
		public string DocumentVersion { get; set; }
		[XmlElement(ElementName = "MerchantIdentifier")]
		public string MerchantIdentifier { get; set; }
	}

	[XmlRoot(ElementName = "StandardPrice")]
	public class StandardPrice
	{
		[XmlAttribute(AttributeName = "currency")]
		public string Currency { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "Price")]
	public class Price
	{
		[XmlElement(ElementName = "SKU")]
		public string SKU { get; set; }
		[XmlElement(ElementName = "StandardPrice")]
		public StandardPrice StandardPrice { get; set; }
	}

	[XmlRoot(ElementName = "Message")]
	public class Message
	{
		[XmlElement(ElementName = "MessageID")]
		public string MessageID { get; set; }
		[XmlElement(ElementName = "Price")]
		public Price Price { get; set; }
	}

	[XmlRoot(ElementName = "AmazonEnvelope")]
	public class PriceFeed
	{
		[XmlElement(ElementName = "Header")]
		public Header Header { get; set; }
		[XmlElement(ElementName = "MessageType")]
		public string MessageType { get; set; }
		[XmlElement(ElementName = "Message")]
		public List<Message> Messages { get; set; }
		[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Xsi { get; set; }
		[XmlAttribute(AttributeName = "noNamespaceSchemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
		public string NoNamespaceSchemaLocation { get; set; }
	}

}
