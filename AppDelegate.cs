#region Imports
using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Collections;
//using System.Diagnostics;
//using System.Drawing;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;
//using System.Runtime.Serialization;
//using System.Security.Permissions;
//using System.Security;
//using System.Text;
//using System.Threading.Tasks;
//using System.Threading;

//using AddressBook;
//using AudioUnit;
//using AVFoundation;
//using CoreAnimation;
//using CoreFoundation;
//using CoreGraphics;
//using CoreImage;
//using CoreMedia;
//using CoreText;
//using EventKit;
//using ExternalAccessory;
using Foundation;
//using JavaScriptCore;
//using MapKit;
//using MediaPlayer;
//using MessageUI;
//using ObjCRuntime;
//using OpenTK;
//using ReplayKit;
//using Social;
//using SpriteKit;
//using StoreKit;
//using SystemConfiguration;
using UIKit;
#endregion

using AVFoundation;

[Register ("AppDelegate")]
public partial class AppDelegate : UIApplicationDelegate, IAVCapturePhotoCaptureDelegate
{
	UIWindow window;
	UIViewController dvc;
	UIButton button;

	public void TickOnce ()
	{
		var session = new AVCaptureSession ();
		session.SessionPreset = AVCaptureSession.PresetPhoto;

		var output = new AVCapturePhotoOutput ();
		var settings = AVCapturePhotoSettings.Create ();

		var device = AVCaptureDevice.GetDefaultDevice (AVMediaTypes.Video);
		NSError err;
		var input = new AVCaptureDeviceInput (device, out err);
		session.AddInput (input);
		session.AddOutput (output);
		session.StartRunning ();

		output.CapturePhoto (settings, this);
	}

	[Export ("captureOutput:didFinishProcessingPhoto:error:")]
	public void DidFinishCapture (AVCapturePhotoOutput captureOutput, AVCapturePhoto resolvedSettings, NSError error)
	{
		Console.WriteLine ("DidFinishCapture");
		var data = resolvedSettings.DepthData;
		if (data != null) {
			string auxDataType;
			var dict = data.GetDictionaryRepresentation (out auxDataType);
			Console.WriteLine (dict);
		}
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
		Console.WriteLine (typeof (System.FileStyleUriParser)); // System.dll
		Console.WriteLine (typeof (Microsoft.Win32.SafeHandles.SafeMemoryMappedFileHandle)); // System.Core.dll
		Console.WriteLine (typeof (System.Numerics.BigInteger)); // System.Numerics.dll
		Console.WriteLine (typeof (System.Security.Cryptography.CryptographicAttributeObject)); // System.Security.dll
		Console.WriteLine (typeof (System.Xml.XmlDocument)); // System.Xml.dll
		UIApplication.Main (args, null, "AppDelegate");
	}
}
