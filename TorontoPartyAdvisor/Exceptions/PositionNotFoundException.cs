using System;

namespace TorontoPartyAdvisor
{
	public class PositionNotFoundException : Exception
	{

		public PositionNotFoundException()
		{}

		public PositionNotFoundException(string message) : base(message) {}
	}
}

