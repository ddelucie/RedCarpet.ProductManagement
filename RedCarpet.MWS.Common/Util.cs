using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace RedCarpet.MWS.Common
{
	public static class Util
	{
		public static Stream GenerateStreamFromString(string s)
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}

		public static Stream GenerateStreamFromXml<T>(T item)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			var stream = new MemoryStream();
			serializer.Serialize(stream, item); stream.Position = 0;
			return stream;
		}

		public static void GenerateFromXml<T>(T item)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			using (FileStream fs = new FileStream(@"sertest.xml", FileMode.Create))
			{
				serializer.Serialize(fs, item);
			}
		}

		public static string CalculateContentMD5(Stream content)
		{
			MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
			byte[] hash = provider.ComputeHash(content);
			return Convert.ToBase64String(hash);
		}
	}
}
