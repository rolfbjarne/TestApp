using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

using Mono.Cecil;

namespace TestBlock {
	class MainClass {
		static void Main (string [] args)
		{
			var rootdir = Path.GetDirectoryName (Path.GetDirectoryName (Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location)));
#if NET
			rootdir = Path.GetDirectoryName (rootdir);
#endif
			var dir = Path.Combine (rootdir, "asm");

			var xi = Path.Combine (dir, "Xamarin.iOS.dll");

			// Load using MetadataLoadContext
			var references = new string []
			{
				xi,
				Path.Combine (dir, "System.Drawing.Common.dll"),
				Path.Combine (dir, "System.dll"),
				Path.Combine (dir, "mscorlib.dll"),
				Path.Combine (dir, "System.Core.dll"),
			};
			var universe = new MetadataLoadContext (new PathAssemblyResolver (references));
			PrintAttributeCount (universe.LoadFromAssemblyPath (xi), "MetadataLoadContext");

			// Load using old-style reflection
			PrintAttributeCount (Assembly.LoadFile (xi), "Old style reflection");

			var ad = AssemblyDefinition.ReadAssembly (xi);
			PrintAttributeCount (ad, "Mono.Cecil");
		}

		static void PrintAttributeCount (Assembly asm, string message)
		{
			var method = asm.GetType ("UserNotificationsUI.IUNNotificationContentExtension").GetMethod ("DidReceiveNotification");
			var attribs = method.GetCustomAttributesData ();
			Console.WriteLine ("{2} {1}: Attrib count: {0}", attribs.Count, message, attribs.Count == 4 ? "✅" : "❌");
		}

		static void PrintAttributeCount (AssemblyDefinition asm, string message)
		{
			var method = asm.MainModule.Types.First (v => v.FullName == "UserNotificationsUI.IUNNotificationContentExtension").Methods.First (v => v.Name == "DidReceiveNotification");
			var attribs = method.CustomAttributes;
			Console.WriteLine ("{2} {1}: Attrib count: {0}", attribs.Count, message, attribs.Count == 4 ? "✅" : "❌");
		}
	}
}