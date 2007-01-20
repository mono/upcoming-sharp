namespace Mono.Upcoming
{
	public class UpcomingParam
	{
		private string name;
		private object value;

		public string Name {
			get { return name; }
		}

		public object Value {
			get { return value; }
		}

		public override string ToString ()
		{
			return string.Format ("&{0}={1}", System.Web.HttpUtility.UrlEncode (Name), System.Web.HttpUtility.UrlEncode (Value.ToString ()));
		}

		public UpcomingParam (string name, object value)
		{
			this.name = name;
			this.value = value;
		}
	}
}
