using System;
using System.IO;
using SampleSQLite.Droid;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteConnector))]

namespace SampleSQLite.Droid
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
				string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
				var path = Path.Combine(documentsPath, Constants.csDataBaseName);
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
