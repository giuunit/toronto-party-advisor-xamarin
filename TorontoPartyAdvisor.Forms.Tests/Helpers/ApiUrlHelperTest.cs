using System;
using NUnit.Framework;

namespace TorontoPartyAdvisor.Forms.Tests
{
	[TestFixture]
	public class ApiUrlHelperTest
	{
		[Test]
		public void GetUrlTest()
		{
			var tokenMock = "qsdqsDfqfsqfzz54fqsdq";
			string url = ApiUrlHelper.GetPlacesGetUrl (tokenMock);
		}

		[Test]
		public void GetPlacesPingUrl()
		{
			string url = ApiUrlHelper.GetPlacesPingUrl ();
		}
	}
}

