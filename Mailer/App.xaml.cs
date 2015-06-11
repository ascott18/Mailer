using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Mailer
{
	/// <summary>
	///     Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			path = Path.Combine(path, "Mailer/mailer.db");

			var dir = (new FileInfo(path)).Directory;

			// This is used in the connection string.
			AppDomain.CurrentDomain.SetData("DataDirectory", dir.FullName);


			// If the database isn't already there, put it there.
			if (!File.Exists(path))
			{
				// Get the embedded prototype database from the manifest.
				var db = Mailer.Properties.Resources.databasePrototype;

				// Make the folder in %APPDATA% that the database lives in.
				dir.Create();

				File.WriteAllBytes(path, db);
			}
		}


		private void App_OnExit(object sender, ExitEventArgs e)
		{
			Mailer.Properties.Settings.Default.Save();
		}
	}
}
