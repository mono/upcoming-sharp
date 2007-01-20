using System.Xml.Serialization;

namespace Mono.Upcoming
{
	[XmlRoot ("rsp")]
	public class Response
	{
		[XmlAttribute ("stat")]
		public Status Status;

		[XmlElement ("category")]
		public Category[] Categories;

		[XmlElement ("country")]
		public Country[] Countries;

		[XmlElement ("event")]
		public Event[] Events;

		[XmlElement ("group")]
		public Group[] Groups;

		[XmlElement ("metro")]
		public Metro[] Metros;

		[XmlElement ("state")]
		public State[] States;

		[XmlElement ("token")]
		public Token Token;

		[XmlElement ("user")]
		public User[] Users;

		[XmlElement ("venue")]
		public Venue[] Venues;

		[XmlElement ("watchlist")]
		public Watch[] Watches;
	}

	public enum Status
	{ 
		[XmlEnum ("ok")]
		OK,
		[XmlEnum ("failed")]
		Failed
	}
}
