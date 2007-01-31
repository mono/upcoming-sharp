using System.Xml.Serialization;

namespace Mono.Upcoming
{
	public class Event : ConnectionWrapper
	{
		[XmlAttribute ("id")]
		public int ID;

		[XmlAttribute ("name")]
		public string Name;

		[XmlAttribute ("tags")]
		public string Tags;

		[XmlAttribute ("description")]
		public string Description;

		[XmlAttribute ("start_date")]
		public string start_date;

		[XmlIgnore ()]
		public System.DateTime StartDate
		{
			get {
				try
				{
					return System.DateTime.Parse (start_date);
				}
				catch
				{
					return new System.DateTime (0);
				}
			}
		}

		[XmlAttribute ("end_date")]
		public string end_date;

		[XmlIgnore ()]
		public System.DateTime EndDate
		{
			get
			{
				try
				{
					return System.DateTime.Parse (end_date);
				}
				catch
				{
					return new System.DateTime (0);
				}
			}
		}

		[XmlAttribute ("personal")]
		public int personal;

		public bool Personal {
			get { return personal == 1; }
		}

		[XmlAttribute ("self_promotion")]
		public int self_promotion;

		public bool SelfPromotion {
			get { return self_promotion == 1; }
		}

		[XmlAttribute ("metro_id")]
		public int MetroID;
		
		[XmlAttribute ("venue_id")]
		public int VenueID;

		[XmlAttribute ("user_id")]
		public int UserID;

		[XmlAttribute ("category_id")]
		public int CategoryID;

		[XmlAttribute ("date_posted")]
		public string date_posted;

		[XmlAttribute ("latitude")]
		public string latitude;

		public float Latitude {
			get {
				try {
					return float.Parse (latitude);
				}
				catch {
					return 0.0F;
				}
			}
		}

		[XmlAttribute ("longitude")]
		public string longitude;

		public float Longitude {
			get {
				try {
					return float.Parse (longitude);
				}
				catch {
					return 0.0F;
				}
			}
		}

		[XmlAttribute ("geocoding_precision")]
		public string GeocodingPrecision;

		[XmlAttribute ("geocoding_ambiguous")]
		public string geocoding_ambiguous;

		public bool GeocodingAmbiguous {
			get {
				if (geocoding_ambiguous == "1")
					return true;
				else
					return false;
			}
		}

		public Metro GetMetro ()
		{
			return connection.GetMetro (MetroID);
		}

		public Venue GetVenue ()
		{
			return connection.GetVenue (VenueID);
		}

		public User GetUser ()
		{
			return connection.GetUserInfo (UserID);
		}

		public Group[] GetGroups ()
		{
			return connection.GetGroups (ID);
		}
	}
}
