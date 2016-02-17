#region Imports
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
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

[Register ("AppDelegate")]
public partial class AppDelegate : UIApplicationDelegate
{
	UIWindow window;
	UIViewController dvc;
	UIButton button;

	public void TickOnce ()
	{
	}

	void Tapped ()
	{
		TickOnce ();
	}

	public override bool FinishedLaunching (UIApplication app, NSDictionary options)
	{
		window = new UIWindow (UIScreen.MainScreen.Bounds);

		NSTimer.CreateScheduledTimer (0.1, (v) => TickOnce ());

		dvc = new C ();
		dvc.View.BackgroundColor = UIColor.Green;

		window.RootViewController = dvc;
		window.MakeKeyAndVisible ();

		return true;
	}

	static void Main (string[] args)
	{
		UIApplication.Main (args, null, "AppDelegate");
	}
}

class C : UIViewController
{
	public override void ViewDidLoad ()
	{
		base.ViewDidLoad ();

		UIScrollView scrollView = new UIScrollView(new CGRect(0, 0, this.View.Bounds.Width, this.View.Bounds.Height));

		var pageWidth = scrollView.Frame.Size.Width;
		float pageHeight = 0;

		scrollView.BackgroundColor = UIColor.White;
		scrollView.Frame = new CGRect(0,0, pageWidth, pageHeight);
		scrollView.ClipsToBounds = false;
		scrollView.Scrolled += HandleScrolled;
		scrollView.DecelerationEnded += HandleDecelerationEnded;

		int count = 3;
		var scrollFrame = scrollView.Frame;
		scrollFrame.Width = scrollFrame.Width * count;
		scrollView.ContentSize = scrollFrame.Size;
		scrollView.PagingEnabled = true;
		scrollView.ShowsHorizontalScrollIndicator = false;

		List<UIViewController> controllers = new List<UIViewController>();

		controllers.Add(new UIViewController());
		controllers.Add(new UIViewController());
		controllers.Add(new UIViewController());

		UIPageControl pageControl = new UIPageControl();

		pageControl.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
		pageControl.Pages = count;
		pageControl.UserInteractionEnabled = false;
		pageControl.BackgroundColor = UIColor.Clear;

		pageControl.CurrentPage = 0;

		this.View = scrollView;
	}

	void HandleDecelerationEnded (object sender, EventArgs e)
	{
		Console.WriteLine ("Ended");
	}

	void HandleScrolled (object sender, EventArgs e)
	{
		Console.WriteLine ("Scrolled");
	}
}
