using System;
using System.Diagnostics.Contracts;
using System.IO;
using SampleSQLite.iOS;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteConnector))]

namespace SampleSQLite.iOS
{
	public class SQLiteConnector:ISQLiteConnector
	{
		public SQLiteConnector()
		{
		}

		public SQLiteConnection GetConnection()
		{
			try
			{
				string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
				var path = Path.Combine(libraryPath, Constants.csDataBaseName);

				var isShouldGenerateTables = false;
				if (!System.IO.File.Exists(path))
					isShouldGenerateTables = true;
				
				var conn = new SQLite.SQLiteConnection(path);

				if (isShouldGenerateTables)
					conn.CreateTable<InfoItem>();

				return conn;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			finally
			{
			}
			return null;
		}
	}
}
