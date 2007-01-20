using System.Xml.Serialization;
using System.Xml.Schema;

namespace Mono.Upcoming
{
	public class Country : ConnectionWrapper
	{
		[XmlAttribute ("id")]
		public int ID;

		[XmlAttribute ("name")]
		public string Name;

		[XmlAttribute ("code")]
		public string Code;

		public State[] GetStateList ()
		{
			Response rsp = connection.Util.Get ("metro.getStateList", new UpcomingParam ("country_id", ID));
			ApplyConnection (rsp.States);

			return rsp.States;
		}
	}
}
