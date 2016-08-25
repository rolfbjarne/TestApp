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
			Console.Write ("****** LAUNCH ******");
		}

		public override void ApplicationWillEnterForeground ()
		{
			Console.Write ("****** FOREGROUND ******");
		}

		public static void ScheduleNextBackgroundUpdate ()
		{
			// Create a fire date 30 minutes into the future
			var fireDate = NSDate.FromTimeIntervalSinceNow (30 * 60);

			// Create 
			var userInfo = new NSMutableDictionary ();
			userInfo.Add (new NSString ("LastActiveDate"), NSDate.FromTimeIntervalSinceNow (0));
			userInfo.Add (new NSString ("Reason"), new NSString ("UpdateScore"));

			// Schedule for update
			Console.WriteLine ("Scheduling...");
			WKExtension.SharedExtension.ScheduleBackgroundRefresh (fireDate, userInfo, (error) =>
			{
				Console.WriteLine ("Scheduled: {0}", error);
				// Was the Task successfully scheduled?
				if (error == null) {
					// Yes, handle if needed
				} else {
					// No, report error
				}
			});
			Console.WriteLine ("Scheduling requested.");
		}

		public override void HandleBackgroundTasks (NSSet<WKRefreshBackgroundTask> backgroundTasks)
		{
			Console.WriteLine ("HandleBackgroundTasks");
		}

	}
}

