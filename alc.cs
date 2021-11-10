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
			var dir = Path.Combine (Path.GetDirectoryName (Path.GetDirectoryName (Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location))), "asm");
			Console.WriteLine ("dir: {0}", dir);
			var libraryPaths = new string []
			{
				dir
			};
			var references = new string []
			{
				Path.Combine (dir, "System.Drawing.Common.dll"),
				Path.Combine (dir, "System.dll"),
				Path.Combine (dir, "mscorlib.dll"),
				Path.Combine (dir, "System.Core.dll"),
			};

			var universe = new MetadataLoadContext (
				new SearchPathsAssemblyResolver (
					libraryPaths,
					references),
				"mscorlib"
			);

			var baselib = universe.LoadFromAssemblyPath (Path.Combine (dir, "Xamarin.iOS.dll"));
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