using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionManagement.ViewModel
{
    internal class MainWindowViewModel:ViewModelBase
    {
		private string _name;

		public string Name
		{
			get { return _name; }
			set { _name = value; 
				OnPropertyChanged();
			}
		}


		private int _age;

		public int age
		{
			get { return _age; }
			set { _age = value;
				OnPropertyChanged();
			}
		}

		private string _gender;

		public string Gender
		{
			get { return _gender; }
			set { _gender = value;
				OnPropertyChanged();
			}
		}

		private DateTime _date;

		public DateTime Date
		{
			get { return _date; }
			set { _date = value; }
		}



	}
}
