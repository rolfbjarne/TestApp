using System;

using Foundation;
using UIKit;

[Register ("AppDelegate")]
public partial class AppDelegate : UIApplicationDelegate
{
	UIWindow window;
	UIViewController dvc;
	UIButton button;

	public void TickOnce ()
	{
		Console.WriteLine (check ());
	}

	public bool check ()
	{
		bool valid = false;

		double d1 = 1.0;
		double d2 = 2.0;

		Decimal ammount = new Decimal (d1);
		Decimal avaliable = new Decimal (d2);

		valid = ammount <= avaliable;
		return valid;
	}

	void Tapped ()
	{
		TickOnce ();
	}

	public override bool FinishedLaunching (UIApplication app, NSDictionary options)
	{
		window = new UIWindow (UIScreen.MainScreen.Bounds);

		NSTimer.CreateScheduledTimer (0.1, (v) => Tapped ());

		dvc = new UIViewController ();
		dvc.View.BackgroundColor = UIColor.White;
		button = new UIButton (window.Bounds);
		button.TouchDown += (object sender, EventArgs e) => {
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

	static void Main (string [] args)
	{
		UIApplication.Main (args, null, "AppDelegate");
	}
}
