using System;
using NUnit.Framework;

namespace TorontoPartyAdvisor.Forms.Tests
{
	[TestFixture]
	public class PlaceDetailsTest
	{
		[Test]
		public void ConstructorTest()
		{
			var placeDetails = new PlaceDetails () {
				FemaleUsers = 20,
				MaleUsers = 40,
				PlaceId = 1
			};

			var test = placeDetails;
		}
	}
}

