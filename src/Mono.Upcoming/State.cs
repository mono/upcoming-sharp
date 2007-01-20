using System.Xml.Serialization;

namespace Mono.Upcoming
{
	public class State : ConnectionWrapper
	{
		[XmlAttribute ("id")]
		public int ID;

		[XmlAttribute ("name")]
		public string Name;

		[XmlAttribute ("code")]
		public string Code;

		[XmlAttribute ("country_id")]
		public int CountryID;

		public Metro[] GetMetroList ()
		{
			Response rsp = connection.Util.Get ("metro.getList", new UpcomingParam ("state_id", ID));
			ApplyConnection (rsp.Metros);

			return rsp.Metros;
		}

		public Country GetCountry ()
		{
			Response rsp = connection.Util.Get ("country.getInfo", new UpcomingParam ("country_id", CountryID));
			ApplyConnection (rsp.Countries);

			return rsp.Countries[0];
		}
	}
}
