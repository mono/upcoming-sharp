using System;
using System.Text;

namespace Mono.Upcoming.Test
{
	class Program
	{
		static string API_KEY = "0a49a0f577";

		static void Main (string[] args)
		{
			
			UpcomingConnection connection = new UpcomingConnection (API_KEY);

			Event[] evts = connection.SearchEvents ("fosdem");

			foreach (Event e in evts) {
				Console.WriteLine ("Name: {0} - Id: {1}", e.Name, e.ID);
				Console.WriteLine ("StartDate : {0}", e.StartDate);

				Venue v = e.GetVenue ();
				Console.WriteLine ("Venue: {0} - City: {1}", v.Name, v.City);
				Console.WriteLine ();
			}
		}
	}
}
