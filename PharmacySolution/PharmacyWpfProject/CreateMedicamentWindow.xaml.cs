using PharmacyWpfProject.Models;
using PharmacyWpfProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PharmacyWpfProject
{
    public partial class CreateMedicamentWindow : Window
    {
        public CreateMedicamentWindow(MedicamentMainVM parentViewModel)
        {
            InitializeComponent();

            MedicamentCreateVM vm = new MedicamentCreateVM(parentViewModel);
            DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
        }
    }
}
