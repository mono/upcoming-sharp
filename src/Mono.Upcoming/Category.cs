using System.Xml.Serialization;

namespace Mono.Upcoming
{
	public class Category
	{
		[XmlAttribute ("id")]
		public string ID;

		[XmlAttribute ("name")]
		public string Name;

		[XmlAttribute ("description")]
		public string Description;
	}
}
