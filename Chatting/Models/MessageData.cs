using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Chatting
{
	public static class MessageData
	{
		public static List<Message> ContactMessages { get; set; }

		static MessageData()
		{
			var temp = new List<Message>();
			ContactMessages = temp.OrderBy(i => i.Name).ToList();
		}
	}
}