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
		DialogViewController dvc;

		[DllImport ("libc")]
		static extern int strlen (IntPtr str);
		public void NativeCrash ()
		{
			strlen (IntPtr.Zero);
		}

		public void UnhandledException ()
		{
			throw new Exception ("Unhandled I am!");
		}

		public void DivisionByZero ()
		{
			int a = 1;
			int b = 0;
			int c = a / b;
		}

		Element CreateStyledStringElement (string caption, NSAction action)
		{
			var rv = new StyledStringElement (caption) {
				BackgroundColor = UIColor.LightGray,
				Alignment = UITextAlignment.Center,
			};
			rv.Tapped += action;
			return rv;
		}

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			dvc = new DialogViewController (
				new RootElement ("Test App")
				{
					new Section ("Actions") {
						CreateStyledStringElement ("Division by zero", () => DivisionByZero ()),
						CreateStyledStringElement ("Unhandled exception", () => UnhandledException ()),
						CreateStyledStringElement ("Native crash", () => NativeCrash ()),
					},
				}
			);
		
			dvc.Autorotate = true;

			window.RootViewController = dvc;
			window.MakeKeyAndVisible ();

			return true;
		}

		static void Main (string[] args)
		{
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}



