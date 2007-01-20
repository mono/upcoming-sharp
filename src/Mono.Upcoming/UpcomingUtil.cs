using System;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Security;

namespace Mono.Upcoming
{
	internal class UpcomingUtil
	{
		private static readonly string URL = "http://upcoming.org/services/rest/?";
		private static XmlSerializer response_serializer;
		private string api_key;

		public string APIKey {
			get { return api_key; }
		}

		static UpcomingUtil () {
			response_serializer = new XmlSerializer (typeof (Response));
		}

		public UpcomingUtil (string api_key)
		{
			this.api_key = api_key;
		}

		public Response Get (string method_name, params UpcomingParam[] parameters)
		{ 
			string parameter_string = FormatParameters (method_name, parameters);
			return GetParameters (parameter_string);
		}

		public Response Post (string method_name, params UpcomingParam[] parameters)
		{
			string parameter_string = FormatParameters (method_name, parameters);
			return PostParameters (parameter_string);
		}

		private string FormatParameters (string method_name, params UpcomingParam[] parameters)
		{
			StringBuilder builder = new StringBuilder (string.Format ("api_key={0}&method={1}", api_key, method_name));

			foreach (UpcomingParam param in parameters)
				builder.Append (param.ToString ());

			return builder.ToString ();
		}

		private static string GetString (string url)
		{
			HttpWebRequest request = HttpWebRequest.Create (url) as HttpWebRequest;
			request.Credentials = CredentialCache.DefaultCredentials;
			WebResponse response = request.GetResponse ();
			string result = new StreamReader (response.GetResponseStream ()).ReadToEnd ();
			response.Close ();
			return result;
		}

		private static Response GetParameters (string parameters)
		{
		 	string url = string.Format ("{0}{1}", URL, parameters);
			HttpWebRequest request = HttpWebRequest.Create (url) as HttpWebRequest;
			request.Credentials = CredentialCache.DefaultCredentials;

			WebResponse response = null;

			try {
				response = request.GetResponse ();
				// Console.WriteLine (GetString (url));
				return (Response)response_serializer.Deserialize (response.GetResponseStream ());
			}
			catch (WebException ex) {
				throw new UpcomingException (ex.Message);
			}
			finally {
				if (response != null)
					response.Close ();
			}
		}

		private static Response PostParameters (string parameter_string)
		{
			WebRequest conn;

			try {
				conn = WebRequest.Create (URL);
			}
			catch (SecurityException) {
				throw;
			}

			conn.Method = "POST";
			conn.ContentType = "application/x-www-form-urlencoded";

			byte[] buf = Encoding.UTF8.GetBytes (parameter_string);
			conn.ContentLength = buf.Length;

			Stream stream = conn.GetRequestStream ();
			try {
				stream.Write (buf, 0, buf.Length);
			}
			catch (IOException) {
				throw;
			}
			finally {
				stream.Close ();
			}

			WebResponse response = null;

			// FIXME ugh... we shouldn't need to trap any exceptions
			// but apparently upcoming.org returns 404 Not found exception
			// on e.g. "watchlist.remove", so we don't want to die on the response giving errors.
			// There ain't much we can do about it for now though.
			try {
				response = conn.GetResponse ();
				return (Response)response_serializer.Deserialize (response.GetResponseStream ());
			}
			catch {
				return null;
			}
			finally {
				if (response != null)
					response.Close ();
			}
		}
	}

	public class UpcomingException : Exception
	{
		public UpcomingException (string msg)
			: base (msg)
		{ }
	}
}
