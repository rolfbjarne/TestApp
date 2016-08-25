using System;

using WatchKit;
using Foundation;

namespace unifiedtestapp.srtfhExtension
{
	public partial class InterfaceController : WKInterfaceController
	{
		protected InterfaceController (IntPtr handle) : base (handle)
		{
			// Note: this .ctor should not contain any initialization logic.
			Console.WriteLine ("ctor");

		}

		public override void Awake (NSObject context)
		{
			base.Awake (context);

			// Configure interface objects here.
			Console.WriteLine ("{0} awake with context", this);

			ExtensionDelegate.ScheduleNextBackgroundUpdate ();
		}

		public override void WillActivate ()
		{
			// This method is called when the watch view controller is about to be visible to the user.
			Console.WriteLine ("{0} will activate", this);
		}

		public override void DidDeactivate ()
		{
			// This method is called when the watch view controller is no longer visible to the user.
			Console.WriteLine ("{0} did deactivate", this);
		}
	}
}
