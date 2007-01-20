using System;
using Mono.Upcoming;

public class Authenticated 
{
	static string API_KEY = "0a49a0f577";

	public static void Main ()
	{
		AuthenticatedConnection connection = new AuthenticatedConnection (API_KEY);

		Console.WriteLine ("Goto {0}", connection.FrobUrl);
		Console.WriteLine ("Authenticate and paste the frob");
		Console.Write("Here: ");
		string frob = Console.ReadLine ();
		Console.WriteLine ();

		connection.CreateToken (frob);

		Watch[] watches = connection.GetWatchList ();

		foreach (Watch w in watches) {
		 	Event e = w.GetEvent ();
			Console.WriteLine ("Name: {0} - Id: {1}", e.Name, e.ID);
			Console.WriteLine ("StartDate : {0}", e.StartDate);

			Venue v = e.GetVenue ();
			Console.WriteLine ("Venue: {0} - City: {1}", v.Name, v.City);
			Console.WriteLine ();
		}
	}
}
