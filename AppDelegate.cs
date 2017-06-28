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

[AttributeUsage (AttributeTargets.All, AllowMultiple = true)]
class TestCaseAttribute : Attribute
{
	public TestCaseAttribute (object type) {}
}

[Register ("AppDelegate")]
public class AppDelegate : UIApplicationDelegate
{
	UIWindow window;
	UIViewController dvc;

	[TestCase (typeof (System.Net.Http.HttpClientHandler))]
	public static void Main (string[] args)
	{
		Console.WriteLine (typeof (AppDelegate).GetMethod ("Main").GetCustomAttributes (typeof (TestCaseAttribute), false));
		//Console.WriteLine (new System.Net.Http.HttpClientHandler ()); // <-- this makes the app launch
		UIApplication.Main (args, null, "AppDelegate");
	}

	public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
	{
		window = new UIWindow (UIScreen.MainScreen.Bounds);
		dvc = new UIViewController ();
		window.RootViewController = dvc;
		window.MakeKeyAndVisible ();

		return true;
	}

}
