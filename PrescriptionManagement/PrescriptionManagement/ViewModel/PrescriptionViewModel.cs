using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrescriptionManagement.Commands;
using PrescriptionManagement.Model;
using PrescriptionManagement.Services;

namespace PrescriptionManagement.ViewModel
{
    internal class PrescriptionViewModel: ViewModelBase
    {

        private readonly List<Patient> _patients;
        public ObservableCollection<Patient> Patients { get; set; }

        public RelayCommand AddCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }

     

        public PrescriptionViewModel()
        {
            AddCommand = new RelayCommand(AddFunction);
            DeleteCommand = new RelayCommand(DeleteFunction, CanDelete);
            Patients = new ObservableCollection<Patient>();
        }
        private string _disease;

		public string Disease
		{
			get { return _disease; }
			set { _disease = value; }
		}

		private string _description;

		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		private string _dosage;

		public string Dosage
		{
			get { return _dosage; }
			set { _dosage = value; }
		}

		public bool IsThree
        {
			get => Dosage == "3 times";
            set
            {
                if (value) Dosage = "3 times";
                OnPropertyChanged();

            }
        }

        public bool IsTwo
        {
            get => Dosage == "2 times";
            set
            {
                if (value) Dosage = "2 times";
                OnPropertyChanged();

            }
        }

        public bool IsOne
        {
            get => Dosage == "1 time";
            set
            {
                if (value) Dosage = "1 time";
                OnPropertyChanged();

            }
        }

        private string _usage;

        public string Usage
        {
            get { return _usage; }
            set { _usage = value; }
        }
        public bool Before
        {
            get => Usage == "Before";
            set
            {
                if (value) Dosage = "Before";
                OnPropertyChanged();

            }
        }

        public bool After
        {
            get => Usage == "After";
            set
            {
                if (value) Dosage = "After";
                OnPropertyChanged();

            }
        }






    }
}
