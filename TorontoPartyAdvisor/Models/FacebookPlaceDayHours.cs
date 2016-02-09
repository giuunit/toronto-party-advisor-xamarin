using System;
using Newtonsoft.Json;

namespace TorontoPartyAdvisor
{
	public class FacebookPlaceDayHours
	{
		[JsonProperty(PropertyName = "day")]
		public DayOfWeek Day { get; set; }
		[JsonProperty(PropertyName="start_time")]
		public string StartTime { get; set; }
		[JsonProperty(PropertyName = "end_time")]
		public string EndTime { get; set; }
	}
}

