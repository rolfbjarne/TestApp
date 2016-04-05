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

class GCMonitor {
	static int counter;
	int id = counter++;

	public GCMonitor ()
	{
		Console.WriteLine ("Created: {0}", id);
	}

	~GCMonitor ()
	{
		Console.WriteLine ("Collected: {0} FAILED", id);
	}
}

[Register ("AppDelegate")]
public partial class AppDelegate : UIApplicationDelegate
{
	UIWindow window;
	UIViewController dvc;
	UIButton button;

	ConditionalWeakTable<UIControl, GCMonitor> table = new ConditionalWeakTable<UIControl, GCMonitor> ();
//	List<UIControl> controls = new List<UIControl> ();

	public void TickOnce ()
	{
		var ctrl = new UIControl ();
		ctrl.GetType ().GetMethod ("MarkDirty", BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null).Invoke (ctrl, new Object [] {} ); // make toggle ref object
		table.Add (ctrl, new GCMonitor ());
		ctrl.DangerousRetain (); // this will cause the 'ctrl' instance to be marked as a strong toggle ref object, but that won't prevent the value in the CWT from being collected
//		controls.Add (ctrl); // this will prevent the value in the CWT from being collected
	}

	void Tapped ()
	{
		TickOnce ();
	}

	public override bool FinishedLaunching (UIApplication app, NSDictionary options)
	{
		window = new UIWindow (UIScreen.MainScreen.Bounds);

		NSTimer.CreateScheduledTimer (0.1, (v) => TickOnce ());
		NSTimer.CreateRepeatingScheduledTimer (1, (v) => GC.Collect ());

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
