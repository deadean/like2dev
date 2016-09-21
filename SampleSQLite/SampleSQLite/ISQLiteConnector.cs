using System;
using SQLite;

namespace SampleSQLite
{
	public interface ISQLiteConnector
	{
		SQLiteConnection GetConnection();
	}
}
