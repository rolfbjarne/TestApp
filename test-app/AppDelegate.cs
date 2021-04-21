#region Imports
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using AddressBook;
using AudioUnit;
using AVFoundation;
using CoreAnimation;
using CoreFoundation;
using CoreGraphics;
using CoreImage;
using CoreMedia;
using CoreText;
using EventKit;
using ExternalAccessory;
using Foundation;
using JavaScriptCore;
using MapKit;
using MediaPlayer;
using MessageUI;
using ObjCRuntime;
using OpenTK;
using ReplayKit;
using Social;
using SpriteKit;
using StoreKit;
using SystemConfiguration;
using UIKit;
#endregion

[Register ("AppDelegate")]
public partial class AppDelegate : UIApplicationDelegate
{
	UIWindow window;
	UIViewController dvc;
	UIButton button;

	void Tapped ()
	{
		Console.WriteLine (Constants.Version);
		T1 ();
		T2 ();
		T3 ();
		T4 ();
		T5 ();
	}

	void T1 ()
	{
		GC.Collect ();
		GC.Collect ();
		var iterations = 100000;
		var items = new NSObject [iterations];
		var watch = Stopwatch.StartNew ();
		for (var i = 0; i < iterations; i++) {
			items [i] = new NSObject ();
		}
		watch.Stop ();
		Console.WriteLine ($"{MethodBase.GetCurrentMethod ().Name}: {iterations} iterations in {watch.ElapsedMilliseconds,4} ms = {watch.ElapsedTicks / (double) iterations,10} ticks per iteration");
		for (var i = 0; i < iterations; i++)
			items [i].Dispose ();
	}

	void T2 ()
	{
		GC.Collect ();
		GC.Collect ();
		var iterations = 100000;
		var items = new NSObject [iterations];
		var watch = Stopwatch.StartNew ();
		for (var i = 0; i < iterations; i++) {
			items [i] = new CustomObject ();
		}
		watch.Stop ();
		Console.WriteLine ($"{MethodBase.GetCurrentMethod ().Name}: {iterations} iterations in {watch.ElapsedMilliseconds,4} ms = {watch.ElapsedTicks / (double) iterations,10} ticks per iteration");
		for (var i = 0; i < iterations; i++)
			items [i].Dispose ();
	}

	void T3 ()
	{
		GC.Collect ();
		GC.Collect ();
		var iterations = 100000;
		var items = new NSObject [iterations];
		var classHandle = Class.GetHandle (typeof (NSObject));
		var watch = Stopwatch.StartNew ();
		for (var i = 0; i < iterations; i++) {
			var obj = Messaging.IntPtr_objc_msgSend (Messaging.IntPtr_objc_msgSend (classHandle, Selector.GetHandle ("alloc")), Selector.GetHandle ("init"));
			items [i] = Runtime.GetNSObject (obj);
			Messaging.void_objc_msgSend (obj, Selector.GetHandle ("release"));
		}
		watch.Stop ();
		Console.WriteLine ($"{MethodBase.GetCurrentMethod ().Name}: {iterations} iterations in {watch.ElapsedMilliseconds,4} ms = {watch.ElapsedTicks / (double) iterations,10} ticks per iteration");
		for (var i = 0; i < iterations; i++)
			items [i].Dispose ();
	}

	void T4 ()
	{
		GC.Collect ();
		GC.Collect ();
		var iterations = 100000;
		var items = new NSObject [iterations];
		var classHandle = Class.GetHandle (typeof (NSObject));
		var watch = Stopwatch.StartNew ();
		for (var i = 0; i < iterations; i++) {
			var obj = Messaging.IntPtr_objc_msgSend (Messaging.IntPtr_objc_msgSend (classHandle, Selector.GetHandle ("alloc")), Selector.GetHandle ("init"));
			Messaging.void_objc_msgSend (obj, Selector.GetHandle ("retain"));
			items [i] = Runtime.GetNSObject (obj);
			Messaging.void_objc_msgSend (obj, Selector.GetHandle ("release"));
			Messaging.void_objc_msgSend (obj, Selector.GetHandle ("release"));
		}
		watch.Stop ();
		Console.WriteLine ($"{MethodBase.GetCurrentMethod ().Name}: {iterations} iterations in {watch.ElapsedMilliseconds,4} ms = {watch.ElapsedTicks / (double) iterations,10} ticks per iteration");
		for (var i = 0; i < iterations; i++)
			items [i].Dispose ();
	}

	void T5 ()
	{
		GC.Collect ();
		GC.Collect ();
		var iterations = 100000;
		var classHandle = Class.GetHandle (typeof (CustomObject));
		var watch = Stopwatch.StartNew ();
		for (var i = 0; i < iterations; i++) {
			var obj = Messaging.IntPtr_objc_msgSend (Messaging.IntPtr_objc_msgSend (classHandle, Selector.GetHandle ("alloc")), Selector.GetHandle ("init"));
			//GC.KeepAlive (Runtime.GetNSObject (obj));
			Messaging.void_objc_msgSend (obj, Selector.GetHandle ("release"));
		}
		watch.Stop ();
		Console.WriteLine ($"{MethodBase.GetCurrentMethod ().Name}: {iterations} iterations in {watch.ElapsedMilliseconds,4} ms = {watch.ElapsedTicks / (double) iterations,10} ticks per iteration");
	}

	public override bool FinishedLaunching (UIApplication app, NSDictionary options)
	{
		window = new UIWindow (UIScreen.MainScreen.Bounds);

		NSTimer.CreateScheduledTimer (0.1, (v) => Tapped ());

		dvc = new UIViewController ();
		dvc.View.BackgroundColor = UIColor.White;
		button = new UIButton (window.Bounds);
		button.TouchDown += (object sender, EventArgs e) => 
		{
			Tapped ();
		};
		button.SetTitleColor (UIColor.Blue, UIControlState.Normal);
		button.SetTitleColor (UIColor.Gray, UIControlState.Highlighted);
		button.SetTitle ("Click here!", UIControlState.Normal);
		dvc.Add (button);

		window.RootViewController = dvc;
		window.MakeKeyAndVisible ();

		// Launch ();

		return true;
	}

	static void Main (string[] args)
	{
		UIApplication.Main (args, null, "AppDelegate");
	}
}

[Register ("CustomObject")]
class CustomObject : NSObject {
}