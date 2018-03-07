using System;

using Foundation;
using UIKit;
using ObjCRuntime;

public class AppDelegate : UIApplicationDelegate
{
	UIWindow window;
	UIViewController dvc;

	public override bool FinishedLaunching (UIApplication app, NSDictionary options)
	{
		window = new UIWindow (UIScreen.MainScreen.Bounds);
		window.RootViewController = dvc = new UIViewController ();
		dvc.View.BackgroundColor = UIColor.White;
		window.MakeKeyAndVisible ();

		return true;
	}

	static void Main (string[] args)
	{
		UIApplication.Main (args, null, typeof (AppDelegate));
	}
}
