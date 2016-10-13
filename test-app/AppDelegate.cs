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

	public void TickOnce ()
	{
		var sftp = new Renci.SshNet.SftpClient ("servername", 0, "{ username }", "{ password}");
	}

	void Tapped ()
	{
		TickOnce ();
	}

	public override bool FinishedLaunching (UIApplication app, NSDictionary options)
	{
		window = new UIWindow (UIScreen.MainScreen.Bounds);

		NSTimer.CreateScheduledTimer (0.1, (v) => TickOnce ());

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
