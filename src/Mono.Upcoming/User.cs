using System.Xml.Serialization;

namespace Mono.Upcoming
{
	public class User
	{
		[XmlAttribute ("id")]
		public string UserId;

		[XmlAttribute ("name")]
		public string Name;

		[XmlAttribute ("username")]
		public string Username;

		[XmlAttribute ("zip")]
		public string Zip;

		[XmlAttribute ("photourl")]
		public string photourl;

		public System.Uri PhotoUrl {
			get { 
			 	try {
					return new System.Uri (photourl); 
				}
				catch {
					return null; 
				}
			}
		}

		[XmlAttribute ("url")]
		public string url;

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
	}
}
