using System.Xml.Serialization;

namespace Mono.Upcoming
{
	public abstract class ConnectionWrapper
	{
		protected UpcomingConnection connection;

		[XmlIgnore ()]
		public UpcomingConnection Connection {
			get { return connection; }
			set { connection = value; }
		}

		public void ApplyConnection (ConnectionWrapper[] wrappers)
		{
			foreach (ConnectionWrapper w in wrappers)
				w.Connection = this.connection;
		}
	}
}
