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
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PrescriptionManagement.ViewModel
{
    internal class PrescriptionsViewModel:ViewModelBase
    {
        
        
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
            set
            {
                _usage = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> UsageIntervals { get; set; }

        private string _selectedUsageInternal;

        public string SelectedUsageInternal
        {
            get { return _selectedUsageInternal; }
            set { _selectedUsageInternal = value;
                OnPropertyChanged();
            }
        }


        private readonly List<Medicine> _medicines; 
        public ObservableCollection<Medicine> Prescriptions { get; set; }

        public RelayCommand AddPrescriptionCommand { get; set; }
        public RelayCommand DownloadPrescription { get; set; }
        public RelayCommand DeletePrescriptionCommand { get; set; }

        public PrescriptionsViewModel()
        {
            _medicines = new List<Medicine>();
            Prescriptions = new ObservableCollection<Medicine>(_medicines);
            AddPrescriptionCommand = new RelayCommand(AddPrescription);
            DownloadPrescription = new RelayCommand(Download);
            DeletePrescriptionCommand = new RelayCommand(DeletePrescription, CanDeletePrescription);

        }

        

        private void DeletePrescription()
        {
            if (SelectedPrescription != null)
            {
                _medicines.Remove(SelectedPrescription);
                Prescriptions.Remove(SelectedPrescription); 
                SelectedPrescription = null; 
            }
        }

        private bool CanDeletePrescription()
        {
            return SelectedPrescription != null;
        }

        private Medicine _selectedPrescription;

        public Medicine SelectedPrescription
        {
            get { return _selectedPrescription; }
            set { _selectedPrescription = value; OnPropertyChanged();
                DeletePrescriptionCommand.OnCanExecute();
            }
        }

        private void AddPrescription()
        {
            var newPrescription = new Medicine
            {
                MedicineName = MedicineName,
                Dosage = Dosage,
                Usage = $"{SelectedUsageInternal}"
            };

            _medicines.Add(newPrescription);
            Prescriptions.Add(newPrescription);

            MedicineName = string.Empty;
            Dosage = string.Empty;
            //Usage = string.Empty;
            SelectedUsageInternal = string.Empty;
        }

        private void Download()
        {
            try
            {
                string fileName = $"Prescription_{DateTime.Now:yyyyMMddHHmmss}.txt";
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var prescription in Prescriptions)
                    {
                        writer.WriteLine($"Medicine: {prescription.MedicineName}");
                        writer.WriteLine($"Dosage:{prescription.Dosage}");
                        writer.WriteLine($"Usage: {prescription.Usage}");
                    }
                }

                MessageBox.Show($"Predcription saved successfully at {filePath}",
                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex) 
            {
                MessageBox.Show($"An error occured {ex.Message}");
                    
            }
                    
            }
        }
        
    }

