using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PrescriptionManagement.Commands;
using System.Windows.Input;
using PrescriptionManagement.Model;
using PrescriptionManagement.Services;

namespace PrescriptionManagement.ViewModel
{
    internal class PrescriptionsViewModel:ViewModelBase
    {
        private readonly DatabasesManager _dbManager = new DatabasesManager();
        private Patient _searchPatient;

        public Patient SearchPatient
        {
            get { return _searchPatient; }
            set 
            {
                _searchPatient = value;
                OnPropertyChanged();
            }
        }

        private string _patientName;

        public string PatientName
        {
            get { return _patientName; }
            set
            {
                _patientName = value;
                OnPropertyChanged();

                if (!string.IsNullOrEmpty(PatientName))
                {
                    GetSearchPatient();
                }
            }
        }

        private void GetSearchPatient()
        {
            var searchPatient = _dbManager.GetPatient(PatientName);

            if (searchPatient != null)
            {
                PatientAge = _dbManager.GetPatient(PatientName).Age;
                PatientGender = searchPatient.Gender;
                PatientDate = searchPatient.Date;
            }
            else
            {
                PatientAge = 0;
                PatientGender = string.Empty;
                PatientDate = DateTime.Now;
            }
        }

        private int _patientAge;

        public int PatientAge
        {
            get { return _patientAge; }
            set { _patientAge = value;
                OnPropertyChanged();
            }
        }

        private string _patientGender;

        public string PatientGender
        {
            get { return _patientGender; }
            set { _patientGender = value;
                OnPropertyChanged();
            }
        }

        private DateTime _patientDate;

        public DateTime PatientDate
        {
            get { return _patientDate; }
            set { _patientDate = value;
                OnPropertyChanged();
            }
        }



        private string _disease;

        public string Disease
        {
            get { return _disease; }
            set { _disease = value;
                OnPropertyChanged();
            }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value;
                OnPropertyChanged();
            }
        }

        private string _medicineName;

        public string MedicineName
        {
            get { return _medicineName; }
            set { _medicineName = value;
                OnPropertyChanged();
            }
        }



        private string _dosage;

        public string Dosage
        {
            get { return _dosage; }
            set { _dosage = value;
                OnPropertyChanged();
            }
        }

        public bool Before
        {
            get => Dosage == "Before";
            set
            {
                if (value) Dosage = "Before";
                OnPropertyChanged();

            }
        }

        public bool After
        {
            get => Dosage == "After";
            set
            {
                if (value) Dosage = "After";
                OnPropertyChanged();

            }
        }

        private string _usage;

        public string Usage
        {
            get { return _usage; }
            set { _usage = value;
                OnPropertyChanged();
            }
        }
        public bool One
        {
            get => Usage == "1-1-1";
            set
            {
                if (value) Dosage = "1-1-1";
                OnPropertyChanged();

            }
        }

        public bool Two
        {
            get => Usage == "1-0-1";
            set
            {
                if (value) Dosage = "1-0-1";
                OnPropertyChanged();

            }
        }

        public bool Three
        {
            get => Usage == "1-1-1";
            set
            {
                if (value) Dosage = "1-1-1";
                OnPropertyChanged();

            }
        }

        private readonly List<Medicine> _medicines; 
        public ObservableCollection<Medicine> Prescriptions { get; set; }

        public RelayCommand AddPrescriptionCommand { get; set; }
        public PrescriptionsViewModel()
        {
            _medicines = new List<Medicine>();
            Prescriptions = new ObservableCollection<Medicine>(_medicines);
            AddPrescriptionCommand = new RelayCommand(AddPrescription);
        }

        private void AddPrescription()
        {
            var newPrescription = new Medicine
            {
                MedicineName = MedicineName,
                Dosage = Dosage,
                Usage = Usage
            };

            _medicines.Add(newPrescription);
            Prescriptions.Add(newPrescription);

            MedicineName = string.Empty;
            Dosage = string.Empty;
            Usage = string.Empty;
        }

        
    }
}
      