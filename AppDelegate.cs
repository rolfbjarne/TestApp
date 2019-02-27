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
using System.Reflection.Emit;
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


	struct StructWithManyMembers {
		public byte a;
		public sbyte b;
		public short c;
		public ushort d;
		public int e;
		public uint f;
		public long g;
		public ulong h;
		public float i;
		public double j;
		public decimal k;
	}

	public void MakeGenericType ()
	{
		var t = typeof (List<>).MakeGenericType (typeof (StructWithManyMembers));
		var obj = Activator.CreateInstance (t);
		Console.WriteLine (obj);
	}

	public void PropertySetInfoOnNullable ()
	{
		Nullable<bool> value = true;
		typeof (bool?).GetField ("value", BindingFlags.NonPublic | BindingFlags.Instance).SetValue (value, true);
	}

	public void ValueTypeAsDictionaryKey ()
	{
		var dic = new Dictionary<StructWithManyMembers, StructWithManyMembers> ();
		Console.WriteLine (dic.Comparer);
		dic [default (StructWithManyMembers)] = default (StructWithManyMembers);
		dic [default (StructWithManyMembers)] = default (StructWithManyMembers);
		dic [default (StructWithManyMembers)] = default (StructWithManyMembers);
		Console.WriteLine (dic);
	}

	static DispatchQueue queue;
	static bool static_with_attribute_called_back;
	[MonoPInvokeCallback (typeof (dispatch_callback_t))]
	static void StaticCallbackWithPInvokeCallbackAttribute (IntPtr context)
	{
		static_with_attribute_called_back = true;
	}
	public void DelegateToNativeFunction ()
	{
		static_with_attribute_called_back = false;
		dispatch_sync_t del = Marshal.GetDelegateForFunctionPointer<dispatch_sync_t> (Dlfcn.dlsym (Dlfcn.RTLD.Default, "dispatch_sync_f"));
		del (queue.Handle, IntPtr.Zero, StaticCallbackWithPInvokeCallbackAttribute);
		if (!static_with_attribute_called_back)
			throw new Exception ("Not called back!");
	}

	internal delegate void dispatch_callback_t (IntPtr context);

	internal delegate void dispatch_sync_t (IntPtr queue, IntPtr context, dispatch_callback_t dispatch);

	[DllImport (Constants.libcLibrary)]
	extern static void dispatch_sync_f (IntPtr queue, IntPtr context, dispatch_callback_t dispatch);

	static bool static_called_back;
	static void StaticCallback (IntPtr context)
	{
		static_called_back = true;
	}

	public void ReverseCallbackToStaticFunction ()
	{
		static_called_back = false;
		dispatch_sync_f (queue.Handle, IntPtr.Zero, StaticCallback);
		if (!static_called_back)
			throw new Exception ("Not called back!");
	}

	bool instance_called_back;
	void InstanceCallback (IntPtr context)
	{
		instance_called_back = true;
	}

	public void ReverseCallbackToInstanceFunction ()
	{
		instance_called_back = false;
		dispatch_sync_f (queue.Handle, IntPtr.Zero, InstanceCallback);
		if (!instance_called_back)
			throw new Exception ("Not called back!");
	}

	public void ReverseCallbackToAnonymousFunction ()
	{
		var called_back = false;
		dispatch_sync_f (queue.Handle, IntPtr.Zero, (ptr) => called_back = true);
		if (!called_back)
			throw new Exception ("Not called back!");
	}

	public void SystemReflectionEmit ()
	{

		var mod = AssemblyBuilder.DefineDynamicAssembly (new AssemblyName ("dda"), AssemblyBuilderAccess.Run);
		Console.WriteLine (mod);
	}

	public void TickOnce ()
	{
		var methods = new string [] {
			nameof (MakeGenericType),
			nameof (PropertySetInfoOnNullable),
			nameof (ValueTypeAsDictionaryKey),
			nameof (DelegateToNativeFunction),
			nameof (ReverseCallbackToStaticFunction),
			nameof (ReverseCallbackToInstanceFunction),
			nameof (ReverseCallbackToAnonymousFunction),
			nameof (SystemReflectionEmit),
		};

		queue = new DispatchQueue ("Queue!");
		foreach (string m in methods) {
			var mi = GetType ().GetMethod (m);
			try {
				Console.WriteLine (m);
				mi.Invoke (this, new object [] { });
				Console.WriteLine ($"✅ {m} PASS");
			} catch (Exception ex) {
				var tie = ex as TargetInvocationException;
				ex = tie.InnerException;
				Console.WriteLine ($"❌ {m} Exception: {ex.Message}");
			}
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
		UIApplication.Main (args, null, "AppDelegate");
	}
}
