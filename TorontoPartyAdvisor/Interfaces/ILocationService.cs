namespace TorontoPartyAdvisor
{
	public interface ILocationService
	{
		void StartService ();
		void OnPause();
		void OnResume ();
		Position GetPosition ();
	}
}

