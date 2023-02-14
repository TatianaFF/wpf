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
    public partial class EditDelMed : Window
    {
        private MedicamentEditDelModel Medicament;
        public EditDelMed(MedicamentMainModel medicamentMain, MedicamentMainVM parentViewModel)
        {
            InitializeComponent();
            Medicament = new MedicamentEditDelModel
            {
                id = medicamentMain.id,
                Title = medicamentMain.Title,
                Price = medicamentMain.Price
            };

            MedicamentEditDelVM vm = new MedicamentEditDelVM(Medicament, parentViewModel);
            DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
        }
    }
}
