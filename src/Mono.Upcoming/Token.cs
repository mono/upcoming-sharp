using System.Xml.Serialization;

namespace Mono.Upcoming
{
	public class Token
	{
		[XmlAttribute ("token")]
		public string TokenString;

		[XmlAttribute ("user_id")]
		public int UserId;

		[XmlAttribute ("user_name")]
		public string UserFullName;

		[XmlAttribute ("user_username")]
		public string UserName;
	}
}
