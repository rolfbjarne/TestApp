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
		}

		public override void Awake (NSObject context)
		{
			base.Awake (context);

			// Configure interface objects here.
			Console.WriteLine ("{0} awake with context", this);

			NSTimer.CreateRepeatingScheduledTimer (1, (obj) =>
			{
				try {
					var asm = System.Reflection.Assembly.ReflectionOnlyLoad (GetType ().Assembly.FullName);
					Console.WriteLine ("Reflection only: {0}", asm);
				} catch (Exception e) {
					Console.WriteLine ("FAIL: {0}", e);
				}
			});
		}

		class C
		{
			static int Counter;
			public static int Allocated;
			public static int Collected;
			public int ID;

			public C ()
			{
				ID = ++Counter;
				Allocated++;
			}
			~C ()
			{
				Collected++;
			}
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
