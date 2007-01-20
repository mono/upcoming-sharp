using System;
using System.Text;

namespace Mono.Upcoming
{
	public class UpcomingConnection
	{
		private UpcomingUtil util;

		internal UpcomingUtil Util {
			get { return util; }
		}

		public string FrobUrl {
			get { 
			 	return string.Format ("http://upcoming.org/services/auth/?api_key={0}", Util.APIKey); 
			}
		}

		public UpcomingConnection (string api_key)
		{
			this.util = new UpcomingUtil (api_key);
		}

		public Category[] GetCategoryList ()
		{
			Response rsp = Util.Get ("category.getList");

			return rsp.Categories;
		}

		public Event[] SearchEvents (string search_text)
		{
			Response rsp = Util.Get ("event.search", new UpcomingParam ("search_text", search_text));
			SetConnection (rsp.Events);

			return rsp.Events;
		}

		public Country[] GetCountryList ()
		{
			Response rsp = Util.Get ("metro.getCountryList");
			SetConnection (rsp.Countries);

			return rsp.Countries;
		}

		public Metro[] GetByLatLon (float latitude, float longitude)
		{
			Response rsp = Util.Get ("metro.getForLatLon", new UpcomingParam ("latitude", latitude),
				new UpcomingParam ("longitude", longitude));
			SetConnection (rsp.Metros);

			return rsp.Metros;
		}

		public User GetUserInfo (int user_id)
		{
			Response rsp = Util.Get ("user.getInfo", new UpcomingParam ("user_id", user_id));

			return rsp.Users[0];
		}

		public User GetUserInfoByUsername (string username)
		{
			Response rsp = Util.Get ("user.getInfoByUsername", new UpcomingParam ("username", username));

			return rsp.Users[0];
		}

		public Metro[] SearchMetro (string search_text)
		{
			return SearchMetro (search_text, null, null);
		}

		public Metro[] SearchMetro (string search_text, Country country)
		{
			return SearchMetro (search_text, country, null);
		}

		public Metro[] SearchMetro (string search_text, State state)
		{
			return SearchMetro (search_text, null, state);
		}

		public Metro[] SearchMetro (string search_text, Country country, State state)
		{
			System.Collections.ArrayList list = new System.Collections.ArrayList ();
			list.Add (new UpcomingParam ("search_text", search_text));

			if (country != null)
				list.Add (new UpcomingParam ("country_id", country.ID));

			if (state != null)
				list.Add (new UpcomingParam ("state_id", state.ID));

			Response rsp = Util.Get ("metro.search", (UpcomingParam[])list.ToArray (typeof (UpcomingParam)));

			SetConnection (rsp.Metros);

			return rsp.Metros;
		}


		internal Country GetCountry (int country_id)
		{
			Response rsp = Util.Get ("country.getInfo", new UpcomingParam ("country_id", country_id));
			SetConnection (rsp.Countries);

			return rsp.Countries[0];
		}

		internal State GetState (int state_id)
		{
			Response rsp = Util.Get ("state.getInfo", new UpcomingParam ("state_id", state_id));
			SetConnection (rsp.States);

			return rsp.States[0];
		}

		internal Metro GetMetro (int metro_id)
		{
			Response rsp = Util.Get ("metro.getInfo", new UpcomingParam ("metro_id", metro_id));
			SetConnection (rsp.Metros);

			return rsp.Metros[0];
		}

		internal Venue GetVenue (int venue_id)
		{
			Response rsp = Util.Get ("venue.getInfo", new UpcomingParam ("venue_id", venue_id));
			SetConnection (rsp.Venues);

			return rsp.Venues[0];
		}

		internal Event GetEvent (int event_id)
		{
			Response rsp = Util.Get ("event.getInfo", new UpcomingParam ("event_id", event_id));
			SetConnection (rsp.Events);

			return rsp.Events[0];
		}

		internal virtual Group[] GetGroups (int event_id)
		{
			Response rsp = Util.Get ("event.getGroups", new UpcomingParam ("event_id", event_id));
			SetConnection (rsp.Groups);

			return rsp.Groups;
		}

		internal virtual Venue[] GetVenues (int metro_id)
		{
			Response rsp = Util.Get ("venue.getList", new UpcomingParam ("metro_id", metro_id));
			SetConnection (rsp.Venues);

			return rsp.Venues;
		}

		//FIXME: we shouldn't need to pass the page_nr and members_per_page parameters, but otherwise the call fails
		internal virtual User[] GetGroupMembers (int group_id, int page_nr, int members_per_page)
		{
			Response rsp = Util.Get ("group.getMembers", new UpcomingParam ("group_id", group_id),
				new UpcomingParam ("page", page_nr),
				new UpcomingParam ("membersPerPage", members_per_page));

			return rsp.Users;
		}

		//FIXME: we shouldn't need to pass the page_nr and events_per_page parameters, but otherwise the call fails
		internal virtual Event[] GetGroupEvents (int group_id, int page_nr, int events_per_page)
		{
			Response rsp = Util.Get ("group.getEvents", new UpcomingParam ("group_id", group_id),
				new UpcomingParam ("page", page_nr),
				new UpcomingParam ("eventsPerPage", events_per_page));
			SetConnection (rsp.Events);

			return rsp.Events;
		}


		protected void SetConnection (ConnectionWrapper[] wrappers)
		{
			if (wrappers == null)
				return;

			foreach (ConnectionWrapper w in wrappers)
				w.Connection = this;
		}
	}
}
