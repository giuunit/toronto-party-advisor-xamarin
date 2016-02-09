using Xamarin.Forms;
using System;

namespace TorontoPartyAdvisor
{
	public partial class FacebookEventPage : ContentPage
	{
		private readonly FacebookEventExtended _fbEvent;

		public FacebookEventPage(FacebookEventExtended fbEvent)
		{
			InitializeComponent ();
			_fbEvent = fbEvent;

			Title = string.Format ("Next Event @ {0}", _fbEvent.PlaceName);

			//binding of the data here
			lblEventName.Text = _fbEvent.Name;
			lblDescription.Text = _fbEvent.Description;
			lblStartTime.Text = string.Format("{0:hh:mm tt}", _fbEvent.StartTime);
			lblStartDay.Text = FormatHelper.GetFormattedDay (_fbEvent.StartTime);

			//if end date exists
			if(_fbEvent.EndTime != DateTimeOffset.MinValue)
			{
				lblEndTime.Text = string.Format("{0:hh:mm tt}", _fbEvent.EndTime);
				lblEndDay.Text = FormatHelper.GetFormattedDay (_fbEvent.EndTime);
			}

			lblAttendingCount.Text = _fbEvent.AttendingCount.ToString();
			lblMaybeCount.Text = _fbEvent.MaybeCount.ToString();

			imgEventPic.Source = _fbEvent.Picture != null 
				&& _fbEvent.Picture.Data != null 
				&& !string.IsNullOrWhiteSpace(_fbEvent.Picture.Data.Url) ? 
				_fbEvent.Picture.Data.Url : null;
		}
	}
}

