using System;

using Foundation;
using WatchKit;

namespace unifiedtestapp.srtfhExtension
{
	[Register ("ExtensionDelegate")]
	public class ExtensionDelegate : WKExtensionDelegate
	{
		public ExtensionDelegate ()
		{
		}

		public override void ApplicationDidFinishLaunching ()
		{
			Console.WriteLine ("****** LAUNCH ******");
		}
	}
}

