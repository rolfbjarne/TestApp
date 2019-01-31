using System;
using System.Runtime.InteropServices;

using Foundation;
using ObjCRuntime;
using WatchKit;

namespace testappwatch.wkExtension
{
	[Register ("ExtensionDelegate")]
	public class ExtensionDelegate : WKExtensionDelegate
	{
		public override void ApplicationDidFinishLaunching ()
		{
			NSDecimal number = get_number2 (12, 34, 56, 78);
			Console.WriteLine ($"returned NSDecimal: {number.fields} {number.m1} {number.m2} {number.m3} {number.m4} {number.m5} {number.m6} {number.m7} {number.m8}");
			//var n = new NSNumber (0.7f);
			//Console.WriteLine ($"number: {n} handle: 0x{n.Handle.ToString ("x")}");
			//number = get_NSNumber_decimalvalue (n.Handle);
			//Console.WriteLine ($"decimalnumber -1: {number}");
			//number = get_decimalvalue (n.Handle, Selector.GetHandle ("decimalValue"));
			//Console.WriteLine ($"decimalnumber 0: {number}");
			//my_test_objc_msgSend2 (ref number, n.Handle, Selector.GetHandle ("decimalValue"));
			//Console.WriteLine ($"decimalnumber 1: {number}");
			//number = my_test_objc_msgSend (n.Handle, Selector.GetHandle ("decimalValue"));
			//Console.WriteLine ($"decimalnumber 2: {number}");
			//number = n.NSDecimalValue;
			//Console.WriteLine ($"decimalnumber 3: {number}");
			//number = (NSDecimal) 0.7f;
			//Console.WriteLine ($"decimalnumber 4: {number}");
			//number = (NSDecimal) 0.7m;
			//Console.WriteLine ($"decimalnumber 5: {number}");
			//number = (NSDecimal) 0.7d;
			//Console.WriteLine ($"decimalnumber 6: {number}");
			//number = (NSDecimal) 42;
			//Console.WriteLine ($"decimalnumber 7: {number}");
		}
		//[DllImport (ObjCRuntime.Constants.ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
		//public extern static NSDecimal my_test_objc_msgSend (IntPtr receiver, IntPtr selector);

		//[DllImport (ObjCRuntime.Constants.ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
		//public extern static void my_test_objc_msgSend2 (ref NSDecimal retval, IntPtr receiver, IntPtr selector);

		[DllImport ("__Internal")]
		static extern NSDecimal get_number2 (int arg1, int arg2, int arg3, int arg4);

		//[DllImport ("__Internal")]
		//static extern NSDecimal get_NSNumber_decimalvalue (IntPtr handle);

		//[DllImport ("__Internal")]
		//static extern NSDecimal get_decimalvalue (IntPtr handle, IntPtr selector);
	}
}

