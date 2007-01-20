using System.Xml.Serialization;

namespace Mono.Upcoming
{
	public class Venue : ConnectionWrapper
	{
		[XmlAttribute ("id")]
		public int ID;

		[XmlAttribute ("name")]
		public string Name;

		[XmlAttribute ("city")]
		public string City;

		[XmlAttribute ("address")]
		public string Address;

		[XmlAttribute ("zip")]
		public string Zip;

		[XmlAttribute ("description")]
		public string Description;

		[XmlAttribute ("phone")]
		public string Phone;

		[XmlAttribute ("url")]
		public string url;

		[XmlIgnore ()]
		public System.Uri Url {
			get {
				try {
					return new System.Uri (url);
				}
				catch {
					return null;
				}
			}
		}

		[XmlAttribute ("user_id")]
		public int UserID;

		[XmlAttribute ("private")]
		public int is_private;

		[XmlIgnore ()]
		public bool IsPrivate {
			get { return is_private == 1 ? true : false; }
		}

		[XmlAttribute ("metro_id")]
		public int MetroId;

		[XmlAttribute ("latitude")]
		public string latitude;

		[XmlIgnore ()]
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

		[XmlIgnore ()]
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
		public string is_geocoding_ambiguous;

		[XmlIgnore ()]
		public bool IsGeocodingAmbiguous {
			get { return is_geocoding_ambiguous == "1" ? true : false; }
		}

	}
}
