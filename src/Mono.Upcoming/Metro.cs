using System.Xml.Serialization;

namespace Mono.Upcoming
{
	public class Metro : ConnectionWrapper
	{
		[XmlAttribute ("id")]
		public int ID;

		[XmlAttribute ("name")]
		public string Name;

		[XmlAttribute ("code")]
		public string Code;

		[XmlAttribute ("state_id")]
		public int StateId;

		public State GetState ()
		{
			return connection.GetState (StateId);
		}

		public Venue[] GetVenues ()
		{
			return connection.GetVenues (ID);
		}
	}
}
