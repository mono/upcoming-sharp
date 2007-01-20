using System;
using System.Text;
using System.Collections;

namespace Mono.Upcoming
{
	public class AuthenticatedConnection : UpcomingConnection
	{
		private Token token;

		public Token Token {
			get { return token; }
		}

		public AuthenticatedConnection (string api_key)
			: this (null, api_key)
		{ }

		public AuthenticatedConnection (string token, string api_key)
			: base (api_key)
		{
			if (token == null)
				return;

			CheckToken (token);
		}

		public void CreateToken (string frob)
		{
			Response rsp = Util.Get ("auth.getToken", new UpcomingParam ("frob", frob));
			token = rsp.Token;
		}

		private void CheckToken (string token)
		{
			Response rsp = Util.Get ("auth.checkToken", new UpcomingParam ("token", token));
			this.token = rsp.Token;
		}

		public Metro[] GetMetroList ()
		{
			Response rsp = Util.Get ("user.getMetroList", new UpcomingParam ("token", token.TokenString));

			foreach (Metro m in rsp.Metros)
				m.Connection = this;

			return rsp.Metros;
		}

		public User GetUserInfo ()
		{
			return base.GetUserInfo (token.UserId);
		}

		public Group[] GetMyGroups ()
		{
			Response rsp = Util.Get ("group.getMyGroups", new UpcomingParam ("token", Token.TokenString));
			SetConnection (rsp.Groups);

			return rsp.Groups;
		}

		public Watch[] GetWatchList ()
		{
			Response rsp = Util.Get ("watchlist.getList", new UpcomingParam ("token", token.TokenString));
			SetConnection (rsp.Watches);

			return rsp.Watches;
		}

		public Event AddEvent (string name, Venue venue, Category category, DateTime start_date)
		{
			return AddEvent (name, venue, category, start_date, null, false, false);
		}

		// Fixme: currently no end date support
		public Event AddEvent (string name, Venue venue, Category category, DateTime start_date, 
			string description, bool personal, bool selfpromotion)
		{
			UpcomingParam[] param_array = GetEventParams (name, description, venue.ID, category.ID, start_date, 
				personal, selfpromotion);

			Response rsp = Util.Post ("event.add", param_array);

			return rsp.Events[0];
		}

		public Event EditEvent (Event event_to_edit)
		{
			if (event_to_edit.UserID != Token.UserId)
				throw new UpcomingException ("User Id must match Owner Id to be able to edit events");

			UpcomingParam[] param_array = GetEventParams (event_to_edit.Name, event_to_edit.Description, event_to_edit.VenueID, 
				event_to_edit.CategoryID, event_to_edit.StartDate, event_to_edit.Personal, event_to_edit.SelfPromotion);

			Response rsp = Util.Post ("event.edit", param_array);

			return rsp.Events[0];
		}

		private UpcomingParam[] GetEventParams (string name, string description, int venue_id, int category_id, DateTime start_date, 
			bool personal, bool self_promotion)
		{
		 	System.Collections.ArrayList param_list = new System.Collections.ArrayList ();
			param_list.Add (new UpcomingParam ("token", Token.TokenString));
			param_list.Add (new UpcomingParam ("name", name));
			param_list.Add (new UpcomingParam ("venue_id", venue_id));
			param_list.Add (new UpcomingParam ("category_id", category_id));
			param_list.Add (new UpcomingParam ("start_date", start_date.ToString ("YYYY-MM-DD")));
			param_list.Add (new UpcomingParam ("start_time", start_date.ToString ("HH:MM:SS")));

			if (description != null && description != string.Empty)
				param_list.Add (new UpcomingParam ("description", description));

			if (personal)
				param_list.Add (new UpcomingParam ("personal", 1));

			if (self_promotion)
				param_list.Add (new UpcomingParam ("selfpromotion", 1));

			return (UpcomingParam[]) param_list.ToArray (typeof (UpcomingParam));
		}

		public void AddTagsToEvent (Event event_to_edit, string tags)
		{
			Util.Post ("event.addTags", new UpcomingParam ("token", Token.TokenString),
				new UpcomingParam ("event_id", event_to_edit.ID),
				new UpcomingParam ("tags", tags));
		}

		public void RemoveTagFromEvent (Event event_to_edit, string tag)
		{
			Util.Post ("event.removeTag", new UpcomingParam ("token", Token.TokenString),
				new UpcomingParam ("event_id", event_to_edit.ID),
				new UpcomingParam ("tag", tag));
		}

		public Group AddGroup (string name, string description, ModerationLevel moderation_level, bool is_private)
		{
			UpcomingParam[] param_array = GetGroupParams (name, description, moderation_level, is_private);

			Response rsp = Util.Post ("group.add", param_array);

			return rsp.Groups [0];
		}

		public Group EditGroup (Group group)
		{
			if (group.OwnerUserId != Token.UserId)
				throw new UpcomingException ("User Id must match Owner Id to be able to edit group.");

			UpcomingParam[] param_array = GetGroupParams (group.Name, group.Description, group.ModerationLevel, group.IsPrivate);
		 
		 	Response rsp = Util.Post ("group.edit", param_array);

			return rsp.Groups [0];
		}

		public void JoinGroup (Group group)
		{
			Util.Post ("group.join", new UpcomingParam ("token", Token.TokenString), new UpcomingParam ("group_id", group.ID));
		}

		public void LeaveGroup (Group group)
		{
			Util.Post ("group.leave", new UpcomingParam ("token", Token.TokenString), new UpcomingParam ("group_id", group.ID));
		}

		public void AddEventToGroup (Event event_to_add, Group group)
		{
			Util.Post ("group.addEventTo", new UpcomingParam ("token", Token.TokenString), new UpcomingParam ("event_id", event_to_add.ID),
				new UpcomingParam ("group_id", group.ID));
		}

		public UpcomingParam[] GetGroupParams (string name, string description, ModerationLevel moderation_level, bool is_private)
		{
		 	System.Collections.ArrayList param_list = new System.Collections.ArrayList ();
			param_list.Add (new UpcomingParam ("token", Token.TokenString));
			param_list.Add (new UpcomingParam ("name", name));

			if (description != null && description != string.Empty)
			 	param_list.Add (new UpcomingParam ("description", description));

			if (moderation_level == ModerationLevel.Moderated)
			 	param_list.Add (new UpcomingParam ("moderation_level", moderation_level));

			if (is_private)
			 	param_list.Add (new UpcomingParam ("is_private", 1));

			return (UpcomingParam[]) param_list.ToArray (typeof (UpcomingParam));
		}


		public Venue AddVenue (string venuename, string venueaddress, string venuecity, Metro metro, string venuezip, string venuephone, 
				Uri venueurl, string venuedescription, bool is_private)
		{
			ArrayList param_list = CreateVenueParams (venuename, venueaddress, venuecity, metro.ID, venuezip, venuephone, venueurl.ToString (), venuedescription, is_private);
			param_list.Add (new UpcomingParam ("token", Token.TokenString));

			UpcomingParam[] param_array = (UpcomingParam[])param_list.ToArray (typeof (UpcomingParam));

			Response rsp = Util.Post ("venue.add", param_array);

			return rsp.Venues[0];
		}

		public void EditVenue (Venue venue)
		{
			ArrayList param_list = CreateVenueParams (venue.Name, venue.Address, venue.City, venue.MetroId, venue.Zip, 
					venue.Phone, venue.url, venue.Description, venue.IsPrivate);
			param_list.Add (new UpcomingParam ("venue_id", venue.ID));
			param_list.Add (new UpcomingParam ("token", Token.TokenString));

			UpcomingParam[] param_array = (UpcomingParam[])param_list.ToArray (typeof (UpcomingParam));

			Util.Post ("venue.edit", param_array);
		}

		private static ArrayList CreateVenueParams (string venuename, string venueaddress, string venuecity, int metro_id, string venuezip, string venuephone,
				string venueurl, string venuedescription, bool is_private)
		{
			System.Collections.ArrayList param_list = new System.Collections.ArrayList ();
			param_list.Add (new UpcomingParam ("venuename", venuename));
			param_list.Add (new UpcomingParam ("venueaddress", venueaddress));
			param_list.Add (new UpcomingParam ("venuecity", venuecity));
			param_list.Add (new UpcomingParam ("metro_id", metro_id));

			if (venuezip != null && venuezip != string.Empty)
				param_list.Add (new UpcomingParam ("venuezip", venuename));

			if (venuephone != null && venuephone != string.Empty)
				param_list.Add (new UpcomingParam ("venuephone", venuephone));

			if (venueurl != null)
				param_list.Add (new UpcomingParam ("venueurl", venueurl));

			if (venuedescription != null && venuedescription != string.Empty)
				param_list.Add (new UpcomingParam ("venuedescription", venuedescription));

			if (is_private)
				param_list.Add (new UpcomingParam ("private", 1));

			return param_list;
		}

		public int AddEventToWatchList (Event event_to_add)
		{
			return this.AddEventToWatchList (event_to_add, AttendStatus.Watch);
		}

		public int AddEventToWatchList (Event event_to_add, AttendStatus status)
		{
			Response rsp = Util.Post ("watchlist.add", new UpcomingParam ("token", Token.TokenString), 
				new UpcomingParam ("event_id", event_to_add.ID),
				new UpcomingParam ("status", status.ToString ().ToLower ())
				);

			return rsp.Watches[0].ID;
		}

		public void RemoveWatch (Watch watch)
		{
			Util.Post ("watchlist.remove", new UpcomingParam ("token", Token.TokenString),
				new UpcomingParam ("watchlist_id", watch.ID));
		}


		internal override Group[] GetGroups (int event_id)
		{
			Response rsp = Util.Get ("event.getGroups",
				new UpcomingParam ("event_id", event_id),
				new UpcomingParam ("token", Token.TokenString));

			foreach (Group group in rsp.Groups)
				group.Connection = this;

			return rsp.Groups;
		}

		internal override Venue[] GetVenues (int metro_id)
		{
			Response rsp = Util.Get ("venue.getList", new UpcomingParam ("metro_id", metro_id), 
				new UpcomingParam ("token", Token.TokenString));

			foreach (Venue venue in rsp.Venues)
				venue.Connection = this;

			return rsp.Venues;
		}

		internal override User[] GetGroupMembers (int group_id, int page_nr, int members_per_page)
		{
			Response rsp = Util.Get ("group.getMembers", new UpcomingParam ("group_id", group_id), 
				new UpcomingParam ("page", page_nr),
				new UpcomingParam ("membersPerPage", members_per_page),
				new UpcomingParam ("token", Token.TokenString));

			return rsp.Users;
		}

		internal override Event[] GetGroupEvents (int group_id, int page_nr, int events_per_page)
		{
			Response rsp = Util.Get ("group.getEvents", new UpcomingParam ("group_id", group_id),
				new UpcomingParam ("page", page_nr),
				new UpcomingParam ("eventsPerPage", events_per_page),
				new UpcomingParam ("token", Token.TokenString));

			foreach (Event evt in rsp.Events)
				evt.Connection = this;

			return rsp.Events;
		}
	}
}
