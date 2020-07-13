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

	[DllImport ("__Internal")]
	extern static string xamarin_timezone_get_local_name ();

	[DllImport ("__Internal")]
	extern static IntPtr mono_pmip (IntPtr value);

	[DllImport (Constants.libcLibrary)]
	internal static extern int dladdr (IntPtr addr, out Dl_info info);

	internal struct Dl_info {
		internal IntPtr dli_fname; /* Pathname of shared object */
		internal IntPtr dli_fbase; /* Base address of shared object */
		internal IntPtr dli_sname; /* Name of nearest symbol */
		internal IntPtr dli_saddr; /* Address of nearest symbol */
	}

	public void DoIt ()
	{
		Console.WriteLine ("DoIt!");
		const string name = "xamarin_timezone_get_local_name";
		Console.WriteLine ($"{name} returns: {xamarin_timezone_get_local_name ()}");
		LocateSymbol (name);

		const string name2 = "mono_pmip";
		Console.WriteLine ($"{name2} (IntPtr.Zero) returns: 0x{mono_pmip (IntPtr.Zero).ToString ("x")}");
		LocateSymbol ("mono_pmip");
	}

	void LocateSymbol (string name)
	{
		var symbol = ObjCRuntime.Dlfcn.dlsym (Dlfcn.RTLD.Default, name);
		Console.WriteLine ($"{name}: 0x{symbol.ToString ("x")}");
		var rv = dladdr (symbol, out var info);
		if (rv != 0) {
			Console.WriteLine ($"{name} was loaded from {Marshal.PtrToStringAuto (info.dli_fname)}");
		} else {
			Console.WriteLine ($"{name} was loaded from an unknown location (exit code {rv})");
		}
	}

	void Tapped ()
	{
		dladdr (IntPtr.Zero, out var x);
		DoIt ();
	}

	public override bool FinishedLaunching (UIApplication app, NSDictionary options)
	{
		window = new UIWindow (UIScreen.MainScreen.Bounds);

		NSTimer.CreateScheduledTimer (0.1, (v) => DoIt ());

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
			Console.Write ("A");
			Console.Write ("B");
			Console.WriteLine ("C");
		UIApplication.Main (args, null, "AppDelegate");
	}
}
