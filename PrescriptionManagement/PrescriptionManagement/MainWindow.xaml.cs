using System.Windows;
using PrescriptionManagement.Services;

namespace PrescriptionManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            new DatabasesManager();
        }

    }
}
