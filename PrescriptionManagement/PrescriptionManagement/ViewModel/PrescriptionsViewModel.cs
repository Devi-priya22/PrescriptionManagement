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

namespace PrescriptionManagement.ViewModel
{
    internal class PrescriptionsViewModel:ViewModelBase
    {
        private string _medicineName;
        private string _dosage;
        private string _usage;

        public ObservableCollection<Medicine> Prescriptions { get; set; }

        public string MedicineName
        {
            get => _medicineName;
            set
            {
                _medicineName = value;
                OnPropertyChanged();
            }
        }

        public string Dosage
        {
            get => _dosage;
            set
            {
                _dosage = value;
                OnPropertyChanged();
            }
        }

        public string Usage
        {
            get => _usage;
            set
            {
                _usage = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddPrescriptionCommand { get; }

        public PrescriptionsViewModel()
        {
            Prescriptions = new ObservableCollection<Medicine>();
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

            Prescriptions.Add(newPrescription);
            MedicineName = string.Empty;
            Dosage = string.Empty;
            Usage = string.Empty;
        }

        
    }
}
