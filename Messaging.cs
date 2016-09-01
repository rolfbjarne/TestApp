using System;
using System.Runtime.InteropServices;

using CoreGraphics;
using Foundation;
using ObjCRuntime;

class Messaging
{
	internal const string LIBOBJC_DYLIB = "/usr/lib/libobjc.dylib";

	//	[DllImport ("__Internal", EntryPoint = "xamarin_vector_float3_to_Vector4f_objc_msgSend")]
	//	public extern static void xamarin_vector_float3_to_Vector4f_objc_msgSend (IntPtr receiver, IntPtr selector, out OpenTK.Vector4 v4);
	//
	//	[DllImport ("__Internal", EntryPoint = "xamarin_void_objc_msgSend_Vector4f_to_vector_float3")]
	//	public extern static void xamarin_void_objc_msgSend_Vector4f_to_vector_float3 (IntPtr receiver, IntPtr selector, ref OpenTK.Vector4 v4);
	//
	[DllImport (LIBOBJC_DYLIB, EntryPoint = "objc_msgSend")]
	public extern static OpenTK.Vector4 V4_objc_msgSend (IntPtr receiver, IntPtr selector);

	[DllImport (LIBOBJC_DYLIB, EntryPoint = "objc_msgSend")]
	public extern static void void_objc_msgSend_IntPtr_CGPoint_ref_CGPoint (IntPtr receiver, IntPtr selector, IntPtr scrollView, CGPoint velocity, ref CGPoint targetContentOffset);

	[DllImport (LIBOBJC_DYLIB, EntryPoint = "objc_msgSend")]
	public extern static void void_objc_msgSend_IntPtr_IntPtr (IntPtr receiver, IntPtr selector, IntPtr p1, IntPtr p2);

	[DllImport (LIBOBJC_DYLIB, EntryPoint = "objc_msgSend")]
	public extern static IntPtr IntPtr_objc_msgSend (IntPtr receiver, IntPtr selector);

	[DllImport (LIBOBJC_DYLIB, EntryPoint = "objc_msgSend")]
	public extern static int int_objc_msgSend_int (IntPtr receiver, IntPtr selector, int p0);

	[DllImport (LIBOBJC_DYLIB, EntryPoint = "objc_msgSend")]
	public extern static IntPtr IntPtr_objc_msgSend_IntPtr (IntPtr receiver, IntPtr selector, IntPtr p1);

	[DllImport (LIBOBJC_DYLIB, EntryPoint = "objc_msgSend")]
	public extern static IntPtr IntPtr_objc_msgSend_IntPtr_nfloat_Int64 (IntPtr r, IntPtr selector, IntPtr p0, nfloat p1, long p2);

	[DllImport (LIBOBJC_DYLIB, EntryPoint = "objc_msgSend")]
	public extern static IntPtr IntPtr_objc_msgSend_IntPtr_nfloat_int (IntPtr r, IntPtr s, IntPtr p0, nfloat p1, int p2);

	[DllImport (LIBOBJC_DYLIB, EntryPoint = "objc_msgSend")]
	public extern static IntPtr IntPtr_objc_msgSend_int (IntPtr r, IntPtr s, int p0);

	[DllImport (LIBOBJC_DYLIB, EntryPoint = "objc_msgSend")]
	public extern static IntPtr IntPtr_objc_msgSend_long (IntPtr r, IntPtr s, long p0);

	[DllImport (LIBOBJC_DYLIB, EntryPoint = "objc_msgSend")]
	public extern static void void_objc_msgSend (IntPtr r, IntPtr s);

	[DllImport (LIBOBJC_DYLIB, EntryPoint = "objc_msgSend")]
	public extern static void void_objc_msgSend_IntPtr_ref_BlockLiteral (IntPtr receiver, IntPtr selector, IntPtr p1, ref BlockLiteral p2);

}

class AssertionException : Exception {
	public AssertionException () { }
	public AssertionException (string msg) : base (msg) { }
}

class Assert {
	public static void AreEqual (string a, string b, string msg)
	{
		if (a != b) {
			Console.WriteLine ("Not equal:");
			Console.WriteLine ("    {0}", a);
			Console.WriteLine ("    {0}", b);
			throw new AssertionException (msg);
		}
	}
	public static void AreEqual (Type a, Type b, string msg)
	{
		if (a != b)
			throw new AssertionException (msg);
	}
}