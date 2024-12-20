using System;
using System.Windows;
using PrescriptionManagement.ViewModel;

namespace PrescriptionManagement
{
    /// <summary>
    /// Interaction logic for Prescription.xaml
    /// </summary>
    public partial class Prescription : Window
    {
        public Prescription(String name)
        {
            InitializeComponent();
            var vm = (PrescriptionsViewModel)DataContext;
            if (vm != null)
            {
                vm.PatientName = name;
            }
        }
    }
}
