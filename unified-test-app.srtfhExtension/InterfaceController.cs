using System;

using System.Xml;
using System.IO;
using System.Runtime.Serialization;

using WatchKit;
using Foundation;

namespace unifiedtestapp.srtfhExtension
{
	public partial class InterfaceController : WKInterfaceController
	{
		delegate void foo ();

		void ffoo ()
		{
			throw new Exception ("bar");
		}

		public void TickOnce ()
		{
			try {
				new MonoTests.System.Runtime.Serialization.DataContractResolverTest ().UseCase1 ();
			} catch (Exception e) {
				Console.WriteLine (e);
			}
		}


		protected InterfaceController (IntPtr handle) : base (handle)
		{
			// Note: this .ctor should not contain any initialization logic.
			Console.WriteLine ("ctor");

		}

		public override void Awake (NSObject context)
		{
			base.Awake (context);

			// Configure interface objects here.
			Console.WriteLine ("{0} awake with context", this);

			TickOnce ();
		}

		public override void WillActivate ()
		{
			// This method is called when the watch view controller is about to be visible to the user.
			Console.WriteLine ("{0} will activate", this);
		}

		public override void DidDeactivate ()
		{
			// This method is called when the watch view controller is no longer visible to the user.
			Console.WriteLine ("{0} did deactivate", this);
		}
	}
}




namespace MonoTests.System.Runtime.Serialization
{

	public enum Colors
	{
		Red, Green, Blue
	}


	public class DataContractResolverTest
	{
		public void UseCase1 ()
		{
			var ds = new DataContractSerializer (typeof (Colors), null, 10000, false, false, null, new MyResolver ());
			var sw = new StringWriter ();
			using (var xw = XmlWriter.Create (sw))
				ds.WriteObject (xw, new ResolvedClass ());

			// xml and xml2 are equivalent in infoset, except for prefixes and position of namespace nodes. So the difference should not matter.
			string xml = @"<?xml version='1.0' encoding='utf-16'?><Colors xmlns:i='http://www.w3.org/2001/XMLSchema-instance' xmlns:d1p1='urn:dummy' i:type='d1p1:ResolvedClass' xmlns='http://schemas.datacontract.org/2004/07/MonoTests.System.Runtime.Serialization'><Baz xmlns='http://schemas.datacontract.org/2004/07/'>c74376f0-5517-4cb7-8a07-35026423f565</Baz></Colors>".Replace ('\'', '"');
			string xml2 = @"<?xml version='1.0' encoding='utf-16'?><Colors xmlns:i='http://www.w3.org/2001/XMLSchema-instance' xmlns:d1p1='urn:dummy' xmlns:d1p2='http://schemas.datacontract.org/2004/07/' i:type='d1p2:ResolvedClass' xmlns='http://schemas.datacontract.org/2004/07/MonoTests.System.Runtime.Serialization'><d1p2:Baz>c74376f0-5517-4cb7-8a07-35026423f565</d1p2:Baz></Colors>".Replace ('\'', '"');
			try {
				Assert.AreEqual (xml, sw.ToString (), "#1");
			} catch (AssertionException) {
				Assert.AreEqual (xml2, sw.ToString (), "#2");
			}
			using (var xr = XmlReader.Create (new StringReader (xml)))
				Assert.AreEqual (typeof (ResolvedClass), ds.ReadObject (xr).GetType (), "#3");
			using (var xr = XmlReader.Create (new StringReader (xml)))
				Assert.AreEqual (typeof (ResolvedClass), ds.ReadObject (xr).GetType (), "#4");
		}
	}

	public class MyResolver : DataContractResolver
	{
		public override bool TryResolveType (Type type, Type declaredType, DataContractResolver knownTypeResolver, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
		{
			Console.WriteLine ("TryResolveType: {0} {1}", type, declaredType);
			if (knownTypeResolver.TryResolveType (type, declaredType, null, out typeName, out typeNamespace))
				return true;
			return SafeResolveType (type, out typeName, out typeNamespace);
		}

		XmlDictionary dic = new XmlDictionary ();

		bool SafeResolveType (Type type, out XmlDictionaryString name, out XmlDictionaryString ns)
		{
			Console.WriteLine ("SafeResolveType: {0}", type);
			name = dic.Add (type.Name);
			ns = dic.Add (type.Namespace ?? "urn:dummy");
			return true;
		}

		public override Type ResolveName (string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
		{
			Console.WriteLine ("ResolveName: {0} {1} {2}", typeName, typeNamespace, declaredType);
			var v = knownTypeResolver.ResolveName (typeName, typeNamespace, declaredType, null) ?? RecoveringResolveName (typeName, typeNamespace);
			// uncomment the following line to fix:
			//Console.WriteLine ("ResolveName: {0} {1} {2} => {3}", typeName, typeNamespace, declaredType, v);
			return v;
		}

		Type RecoveringResolveName (string typeName, string typeNamespace)
		{
			if (typeNamespace == "urn:dummy")
				return Type.GetType (typeName);
			else
				return Type.GetType (typeNamespace + '.' + typeName);
		}
	}
}

[DataContract]
public class ResolvedClass
{
	[DataMember]
	public Guid Baz = Guid.Parse ("c74376f0-5517-4cb7-8a07-35026423f565");
}



class AssertionException : Exception
{
	public AssertionException () { }
	public AssertionException (string msg) : base (msg) { }
}

class Assert
{
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