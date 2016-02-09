namespace TorontoPartyAdvisor
{
	public class User
	{
		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public readonly string Token;

		public User()
		{
			
		}

		public User(string token)
		{
			Token = token;
		}

		public User(string token, string firstname, string lastname)
		{
			Token = token;
			FirstName = firstname;
			LastName = lastname;
		}

		public void FillName(string firstname, string lastname)
		{
			FirstName = firstname;
			LastName = lastname;
		}
	}
}