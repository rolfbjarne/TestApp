using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

using Foundation;
using UIKit;
using ObjCRuntime;

public class AppDelegate : UIApplicationDelegate
{
	UIWindow window;
	UIViewController dvc;

	static long totalMS;
	static long iterations;

	void Run ()
	{
		IntPtr array;
		const int counter = 10000;
		GC.Collect ();
		createArguments (out array, counter);
		try {
			var watch = Stopwatch.StartNew ();
			callSelector (Handle, array, counter);
			watch.Stop ();
			var lastAvg = totalMS / (double) iterations;
			totalMS += watch.ElapsedMilliseconds;
			iterations++;
			var avg = totalMS / (double) iterations;
			Console.WriteLine ("Did {0} iterations in {1}ms (average: {2:0.00}ms of {3} iterations, diff since last: {4:0.00})",
			                   counter, watch.ElapsedMilliseconds, avg, iterations, avg - lastAvg);
		} finally {
			freeArguments (array, counter);
		}
		GC.Collect ();
		if (iterations == 100)
			_exit (0);
		NSTimer.CreateScheduledTimer (0.1, (v) => Run ());
	}

	[DllImport ("libc")]
	extern static void _exit (int code);

	public override bool FinishedLaunching (UIApplication app, NSDictionary options)
	{
		window = new UIWindow (UIScreen.MainScreen.Bounds);
		window.RootViewController = dvc = new UIViewController ();
		dvc.View.BackgroundColor = UIColor.White;
		window.MakeKeyAndVisible ();

		NSTimer.CreateScheduledTimer (0.1, (v) => Run ());

		return true;
	}

	static void Main (string[] args)
	{
		UIApplication.Main (args, null, typeof (AppDelegate));
	}

	[Export ("callMe:")]
	void CallMe (UIView view)
	{
	}

	[DllImport ("__Internal")]
	extern static void callSelector (IntPtr handle, IntPtr args, int counter);

	[DllImport ("__Internal")]
	extern static void createArguments (out IntPtr array, int counter);

	[DllImport ("__Internal")]
	extern static void freeArguments (IntPtr array, int counter);
}
