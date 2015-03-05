//
// AppDelegate.cs: test app for various stuff.
//
// Authors:
//    Rolf Bjarne Kvinge <rolf@xamarin.com>
//
// Copyright 2013 Xamarin Inc.
//

using System;
using System.Drawing;
using System.Runtime.InteropServices;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace TestApp
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;
		UIViewController dvc;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			dvc = new UIViewController ();
			dvc.View.BackgroundColor = UIColor.Blue;
		
			window.RootViewController = dvc;
			window.MakeKeyAndVisible ();


			NSNotificationCenter.DefaultCenter.AddObserver (null, (v) => {
				Console.WriteLine ("Notification: {0}", v);
			});

			NSTimer.CreateRepeatingScheduledTimer (1, () => {
				new NSThread ().Start ();
			});

			return true;
		}

		static void Main (string[] args)
		{
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}



