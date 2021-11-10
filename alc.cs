using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace TestBlock {
	class MainClass {
		static void Main (string [] args)
		{
			var libraryPaths = new string []
			{
				"/Users/rolf/work/maccore/main/xamarin-macios/_ios-build/Library/Frameworks/Xamarin.iOS.framework/Versions/Current/lib/mono/Xamarin.iOS",
			};
			var references = new string []
			{
				"/Users/rolf/work/maccore/main/xamarin-macios/_ios-build/Library/Frameworks/Xamarin.iOS.framework/Versions/Current/lib/mono/Xamarin.iOS/Facades/System.Drawing.Common.dll",
				"/Users/rolf/work/maccore/main/xamarin-macios/_ios-build/Library/Frameworks/Xamarin.iOS.framework/Versions/Current/lib/mono/Xamarin.iOS/System.dll",
				"/Users/rolf/work/maccore/main/xamarin-macios/_ios-build/Library/Frameworks/Xamarin.iOS.framework/Versions/Current/lib/mono/Xamarin.iOS/mscorlib.dll",
				"/Users/rolf/work/maccore/main/xamarin-macios/_ios-build/Library/Frameworks/Xamarin.iOS.framework/Versions/Current/lib/mono/Xamarin.iOS/System.Core.dll",
			};

			var universe = new MetadataLoadContext (
				new SearchPathsAssemblyResolver (
					libraryPaths,
					references),
				"mscorlib"
			);

			var baselib = universe.LoadFromAssemblyPath ("/Users/rolf/work/maccore/main/xamarin-macios/_ios-build/Library/Frameworks/Xamarin.iOS.framework/Versions/Current/lib/mono/Xamarin.iOS/Xamarin.iOS.dll");
			var attribs = baselib.GetType ("UserNotificationsUI.IUNNotificationContentExtension").GetMethod ("DidReceiveNotification").GetCustomAttributesData ();
			Console.WriteLine ("Attrib count: {0}", attribs.Count);
		}
	}


class SearchPathsAssemblyResolver : MetadataAssemblyResolver
{
	readonly string[] libraryPaths;
	readonly string[] references;

	public SearchPathsAssemblyResolver (string[] libraryPaths, string[] references)
	{
		this.libraryPaths = libraryPaths;
		this.references = references;
	}

	public override Assembly Resolve (MetadataLoadContext context, AssemblyName assemblyName)
	{
		string name = assemblyName.Name;
		if (name != null) {
			foreach (var asm in context.GetAssemblies ()) {
				if (asm.GetName ().Name == name)
					return asm;
			}

			string dllName = name + ".dll";
			foreach (var libraryPath in libraryPaths) {
				string path = Path.Combine (libraryPath, dllName);
				if (File.Exists (path)) {
					return context.LoadFromAssemblyPath (path);
				}
			}
			foreach (var reference in references) {
				if (Path.GetFileName (reference).Equals (dllName, StringComparison.OrdinalIgnoreCase)) {
					return context.LoadFromAssemblyPath (reference);
				}
			}
		}
		return null;
	}
}

}