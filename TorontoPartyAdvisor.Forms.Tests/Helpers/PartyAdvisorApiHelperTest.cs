using NUnit.Framework;

namespace TorontoPartyAdvisor.Forms.Tests
{
	[TestFixture]
	public class PartyAdvisorApiHelperTest
	{
		[Test]
		public void Login()
		{
			var mock = new FacebookUser () {
				Email = "test@gmail.com", 
				FacebookId = "3455433333",
				FirstName = "Ricco",
				LastName = "Don", 
				Gender = "male",
				Locale = "fr_FR",
				TimeZone = 2,
				AccessToken = "dsqdsqdtazFQDDQtzaSa"
			};

			var task = PartyAdvisorApiHelper.Login (mock);
			task.Wait ();
		}

		[Test]
		public void PlacesPing()
		{
			var task = PartyAdvisorApiHelper.GetPlacePing ();
			task.Wait ();

			Assert.IsNotNull (task.Result);
		}
	}
}

