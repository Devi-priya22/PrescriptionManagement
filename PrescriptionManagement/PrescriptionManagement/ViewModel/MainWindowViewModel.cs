using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace PrescriptionManagement.ViewModel
{
    internal class MainWindowViewModel:ViewModelBase,IDataErrorInfo
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
		[Range(10,20)]
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
			set { _date = value;
				OnPropertyChanged();
			}
		}

		private string _search;

		public string Search
		{
			get { return _search; }
			set { _search = value;
				OnPropertyChanged();
			}
		}

        public string Error { get; }

        public string this[string columnName]
		{
            get
            {
                var context = new ValidationContext(this) { MemberName = columnName };
                var results = new List<ValidationResult>();
                var propInfo = this.GetType().GetProperty(columnName);
                var value = propInfo.GetValue(this);
                var isValid = Validator.TryValidateProperty(value, context, results);
                return isValid ? null : results.First().ErrorMessage;
            }
        }
	}
}
