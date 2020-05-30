using System;

namespace WebMvc.Models.ViewModel
{
	public class ErrorViewModel
	{
		public string RequestId { get; set; }

		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
	}
}