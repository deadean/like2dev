using System;
using SQLite;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SampleSQLite
{
	[Table("Info")]
	public class InfoItem:INotifyPropertyChanged
	{
		public event EventHandler Deleted;

		public InfoItem()
		{
			DeleteCommand = new Command(()=>{
				DependencyService.Get<ISQLiteConnector>().GetConnection().Delete(this);
				Deleted?.Invoke(this, null);
			});
		}

		private int _id;
		[PrimaryKey, AutoIncrement]
		public int Id
		{
			get
			{
				return _id;
			}
			set
			{
				this._id = value;
				OnPropertyChanged(nameof(Id));
			}
		}

		private string _name;
		[NotNull]
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				this._name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		[Ignore]
		public ICommand DeleteCommand { get; private set;}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			this.PropertyChanged?.Invoke(this,
				new PropertyChangedEventArgs(propertyName));
		}

		public override string ToString()
		{
			return string.Format("[InfoItem: Id={0}, Name={1}]", Id, Name);
		}
	}
}
