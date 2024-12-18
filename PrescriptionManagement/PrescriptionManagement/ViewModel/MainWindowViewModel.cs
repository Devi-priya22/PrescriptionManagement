using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using PrescriptionManagement.Commands;
using PrescriptionManagement.Model;
using System.Collections.ObjectModel;
using PrescriptionManagement.Services;
using System.Windows;

namespace PrescriptionManagement.ViewModel
{
    internal class MainWindowViewModel:ViewModelBase, IDataErrorInfo
    {
        private readonly List<Patient> _patients;
        public ObservableCollection<Patient> Patients { get; set; }
        public RelayCommand AddCommand { get; set; }

		public RelayCommand DeleteCommand { get; set; }

        public RelayCommand SearchCommand { get; set; }

        private readonly DatabasesManager _dbManager;
		public MainWindowViewModel() 
		{
			AddCommand = new RelayCommand(AddFunction);
			DeleteCommand = new RelayCommand(DeleteFunction,CanDelete);
			SearchCommand = new RelayCommand(SearchFunction,CanSearchFuction);
			_dbManager = new DatabasesManager();
			Patients = new ObservableCollection<Patient>();
			LoadPatients();   
			
		}

        private bool CanSearchFuction()
        {
			return !string.IsNullOrWhiteSpace(Search);
		}


        private void SearchFunction()
		{
			Patients.Clear();
            if (!string.IsNullOrWhiteSpace(Search))
            {
                var filteredPatients = _dbManager.GetAllPatients()
                    .Where(p => p.Name.Contains(Search))
                    .ToList();

                
                foreach (var patient in filteredPatients)
                {
                    Patients.Add(patient);
                }
            }
            else
            {
                
                LoadPatients();
            }
        }

        private void DeleteFunction()
        {
            if (SelectedItem != null)
            {
                _dbManager.DeletePatient(SelectedItem);
                Patients.Remove(SelectedItem);
                SelectedItem = null;
            }

        }

		private bool CanDelete()
		{
            return SelectedItem != null;
        }
        private void AddFunction()
		{
			var patient = new Patient()
			{
				Name = Name,
				Age = Age,
				Gender = Gender,
				Date = Date
			};
            _dbManager.AddPatient(patient);
			Patients.Add(patient);

			Name=string.Empty;
			Age = default;
			Gender=string.Empty;
			IsMale = false;
			IsFemale = false;
			Date= DateTime.Now;
		}
		private void LoadPatients()
		{
			var patientFromDb = _dbManager.GetAllPatients();
			foreach (var patient in patientFromDb)
			{
				Patients.Add(patient);
			}
		}


		private Patient _selectedItem;

		public Patient SelectedItem
		{
			get { return _selectedItem; }
			set { _selectedItem = value; 
				OnPropertyChanged();
				DeleteCommand.OnCanExecute();
			}
		}


		private string _name;
		[Required(ErrorMessage ="Name cannot be null")]
		public string Name
		{
			get { return _name; }
			set { _name = value; 
				OnPropertyChanged();
			}
		}


		private int _age;

		[Required (ErrorMessage ="Age could not be null")]
        [Range(1, 18, ErrorMessage = "It's not a child. Prefer another doctor.")]
        public int Age
        {
            get { return _age; }
            set
            {
                if (value <=18 && value>0)
                {
                    _age = value;
                    OnPropertyChanged();
                }
				
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
		public bool IsMale
		{
			get => Gender == "Male";
			set
			{
				if (value) Gender = "Male";
				OnPropertyChanged();

			}
		}
		public bool IsFemale
		{
            get => Gender == "Female";
            set
            {
                if (value) Gender = "Female";
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
				SearchCommand.OnCanExecute();
			}
		}

		public string Error { get; } = "Errors";

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
