#region Imports
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml;

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
using Network;
using ObjCRuntime;
using OpenTK;
using ReplayKit;
using Social;
using SpriteKit;
using StoreKit;
using SystemConfiguration;
using UIKit;
using PassKit;
#endregion


[Category (typeof (NSString))]
public static class NSStringExtension {
	[Export ("uppercaseStringWithLocaleEx:")]
	public static string toUpperEx (this NSString s, NSLocale locale)
	{
		return "NSStringExtension.toUpper";
	}
}

[Register ("AppDelegate")]
public partial class AppDelegate : UIApplicationDelegate
{
	UIWindow window;
	UIViewController dvc;
	UIButton button;

	[DllImport ("libc", EntryPoint = "objc_msgSend")]
	static extern IntPtr IntPtr_objc_msgSend (IntPtr @class, IntPtr selector);

	[DllImport ("libc", EntryPoint = "objc_msgSend")]
	static extern void objc_msgSend (IntPtr @class, IntPtr selector);

	[DllImport ("libc")]
	static extern IntPtr class_copyProtocolList (IntPtr obj, out uint count);

	[DllImport ("libc")]
	static extern IntPtr object_getClass (IntPtr obj);

	[DllImport ("libc")]
	static extern IntPtr objc_getClass (string name);

	public static void Tapped ()
	{
		IntPtr hDesSelector = Selector.GetHandle ("uppercaseStringWithLocaleEx:");

		NSString s = new NSString ("abc");
		try {
			var rv = Messaging.IntPtr_objc_msgSend_IntPtr (s.Handle, hDesSelector, NSLocale.CurrentLocale.Handle);
			var outPut = Runtime.GetNSObject (rv);

			string outPut2 = s.ToUpper (NSLocale.CurrentLocale);

			Console.WriteLine ("NSString: {0}, toUpper: {1} toUpper2: {2}", s.ToString (), outPut, outPut2);
		} catch (Exception e) {
			Console.WriteLine (e);
		}
	}

	public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
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
		button.SetTitle ("Click here", UIControlState.Normal);
		dvc.Add (button);

		window.RootViewController = dvc;
		window.MakeKeyAndVisible ();

		return true;
	}

	static void Main (string[] args)
	{
		UIApplication.Main (args, null, "AppDelegate");
	}
}
