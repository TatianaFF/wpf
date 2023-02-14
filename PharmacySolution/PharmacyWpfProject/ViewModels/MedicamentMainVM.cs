using GalaSoft.MvvmLight;
using PharmacyWpfProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PharmacyWpfProject.ViewModels
{
    public class MedicamentMainVM : INotifyPropertyChanged
    {
        private const string PATH = "http://localhost:3365/Medicament";
        static HttpClient client = new HttpClient();

        private RelayCommand addCommand;
        private RelayCommand editCommand;
        private MedicamentMainModel selectedMedicament;
        public ObservableCollection<MedicamentMainModel> Medicaments { get; set; }

        public MedicamentMainVM()
        {
            client.BaseAddress = new Uri(PATH);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            fetchMedicaments();
        }

        public void CallFetch()
        {
            fetchMedicaments();
        }

        public void fetchMedicaments()
        {
            HttpResponseMessage response = client.GetAsync("").Result;
            if (response.IsSuccessStatusCode)
            {
                var meds = response.Content.ReadAsAsync<ObservableCollection<MedicamentMainModel>>().Result;
                Medicaments = meds;
                OnPropertyChanged("Medicaments");
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      CreateMedicamentWindow createMedWindow = new CreateMedicamentWindow(this);

                      createMedWindow.Show();
                  }));
            }
        }

        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand(obj =>
                  {
                      EditDelMed editDelMedWindow = new EditDelMed(obj as MedicamentMainModel,this);
                      editDelMedWindow.Show();
                  }));
            }
        }


        public MedicamentMainModel SelectedMedicament
        {
            get { return selectedMedicament; }
            set
            {
                selectedMedicament = value;
                OnPropertyChanged("SelectedMedicament");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        
    }
}
