using System.Xml.Serialization;

namespace Mono.Upcoming
{
	public enum ModerationLevel
	{
	 	[XmlEnum ("moderated")]
		Moderated,
	 	[XmlEnum ("unmoderated")]
		Unmoderated 
	}

	public class Group : ConnectionWrapper
	{
		[XmlAttribute ("id")]
		public int ID;

		[XmlAttribute ("name")]
		public string Name;

		[XmlAttribute ("description")]
		public string Description;

		[XmlAttribute ("moderation_level")]
		public ModerationLevel ModerationLevel;

		[XmlAttribute ("is_private")]
		public int is_private;

		public bool IsPrivate {
			get { return is_private == 1; }
		}

		[XmlAttribute ("timestamp")]
		public string timestamp;

		public System.DateTime Timestamp {
			get { return System.DateTime.Parse (timestamp); }
		}

		[XmlAttribute ("owner_user_id")]
		public int OwnerUserId;

		[XmlAttribute ("commercial_account_id")]
		public int CommercialAccountId;

		[XmlAttribute ("num_members")]
		public int NumberOfMembers;


		public User GetOwner ()
		{
			return connection.GetUserInfo (OwnerUserId);
		}

		public User[] GetMembers (int page_nr, int members_per_page)
		{
			return connection.GetGroupMembers (ID, page_nr, members_per_page);
		}

		public Event[] GetEvents (int page_nr, int events_per_page)
		{
			return connection.GetGroupEvents (ID, page_nr, events_per_page);
		}
	}
}
