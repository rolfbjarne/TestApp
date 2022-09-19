using System;
using System.Diagnostics;
using System.Threading;
using System.Data;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;

class gdb {
	static void Main (string [] args)
	{
		Thread.CurrentThread.CurrentCulture = new CultureInfo ("es-ES");
		Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

		ResourceManager temp = new ResourceManager("ConsoleApplication1.Resources", typeof(gdb).Assembly);

		string res = temp.GetString ("String1");
		//string res = ConsoleApplication1.Properties.Resources.String1;
		Console.WriteLine (res);
	}
}
