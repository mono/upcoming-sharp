using System.Xml.Serialization;

namespace Mono.Upcoming
{
	public enum AttendStatus
	{
		[XmlEnum ("attend")]
		Attend,
		[XmlEnum ("watch")]
		Watch
	}

	public class Watch : ConnectionWrapper
	{
		[XmlAttribute ("id")]
		public int ID;

		[XmlAttribute ("event_id")]
		public int EventID;

		[XmlAttribute ("status")]
		public AttendStatus Status;

		public Event GetEvent ()
		{
			return connection.GetEvent (EventID);
		}
	}
}
