using Xamarin.Forms;
using SQLite;
using System.Linq;

namespace SampleSQLite
{
	public partial class SampleSQLitePage : ContentPage
	{
		SQLiteConnection _connection;
		public SampleSQLitePage()
		{
			try
			{
				InitializeComponent();

				_connection = DependencyService.Get<ISQLiteConnector>().GetConnection();
				UpdateSource();
			}
			catch (System.Exception ex)
			{
			}
			finally
			{
			}
		}

		void UpdateSource()
		{
			var items = _connection.Table<InfoItem>().AsEnumerable().ToList();
			foreach (var item in items)
			{
				item.Deleted += Item_Deleted;
			}
			ctrListView.ItemsSource = items;
		}

		void Handle_Clicked_Create(object sender, System.EventArgs e)
		{
			ctrAddContainer.IsVisible = !ctrAddContainer.IsVisible;
		}

		void Handle_Clicked_Save(object sender, System.EventArgs e)
		{
			_connection.Insert(new InfoItem() { Name = ctrNewItem.Text });
			ctrNewItem.Text = null;
			ctrAddContainer.IsVisible = false;
			UpdateSource();
		}

		void Item_Deleted(object sender, System.EventArgs e)
		{
			UpdateSource();
		}
	}
}
