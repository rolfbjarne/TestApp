//
// AppDelegate.cs: test app for various stuff.
//
// Authors:
//    Rolf Bjarne Kvinge <rolf@xamarin.com>
//
// Copyright 2013 Xamarin Inc.
//

using System;
using System.Drawing;
using System.Runtime.InteropServices;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace TestApp
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;
		UIViewController dvc;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			dvc = new UIViewController ();
			dvc.View.BackgroundColor = UIColor.Blue;
		
			window.RootViewController = dvc;
			window.MakeKeyAndVisible ();

			Console.WriteLine ("a: {0}", strlen ("aToZ"));

			return true;
		}

		static void Main (string[] args)
		{
			UIApplication.Main (args, null, "AppDelegate");
		}


		[DllImport ("libc")]
		static extern int strlen ([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef=typeof(UTF8StringMarshaler), MarshalCookie = "ChocolateCookie")] string str);

		[Preserve(AllMembers = true)]
		class UTF8StringMarshaler : ICustomMarshaler
		{
			static UTF8StringMarshaler singleton;

			IntPtr ICustomMarshaler.MarshalManagedToNative(object managedObject)
			{
				return Marshal.StringToHGlobalAuto ((string) managedObject);
			}

			object ICustomMarshaler.MarshalNativeToManaged(IntPtr nativeDataPtr)
			{
				if (nativeDataPtr == IntPtr.Zero)
					return null;

				return Marshal.PtrToStringAuto (nativeDataPtr);
			}

			void ICustomMarshaler.CleanUpNativeData(IntPtr nativeDataPtr)
			{
			}

			void ICustomMarshaler.CleanUpManagedData(object managedObject)
			{
			}

			int ICustomMarshaler.GetNativeDataSize()
			{
				throw new NotImplementedException();
			}

			public static ICustomMarshaler GetInstance(string cookie)
			{
				Console.WriteLine ("UTF8StringMarshaler.GetInstance ({0} IsNull: {1} Length: {2})", cookie, cookie == null, cookie == null ? "N/A" : cookie.Length.ToString ());
				if (cookie != "ChocolateCookie")
					Console.WriteLine ("FAILED");
				if (singleton == null)
				{
					return singleton = new UTF8StringMarshaler();
				}
				return singleton;
			}
		}
	}
}



