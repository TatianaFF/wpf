using PharmacyWpfProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace PharmacyWpfProject.ViewModels
{
    class MedicamentCreateVM : INotifyPropertyChanged
    {
        private const string PATH = "http://localhost:3365/Medicament";
        static HttpClient client = new HttpClient();
        private RelayCommand postCommand;
        private MedicamentCreateModel newMedicament;
        private MedicamentMainVM parent;

        public Action CloseAction { get; set; }

        public MedicamentCreateVM(MedicamentMainVM parentViewModel)
        {
            NewMedicament = new MedicamentCreateModel();
            parent = parentViewModel;
            //client.BaseAddress = new Uri(PATH);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public MedicamentCreateModel NewMedicament
        {
            get { return newMedicament; }
            set
            {
                newMedicament = value;
                OnPropertyChanged("NewMedicament");
            }
        }

        public RelayCommand PostCommand
        {
            get
            {
                return postCommand ??
                  (postCommand = new RelayCommand(obj =>
                  {
                      MedicamentCreateModel med = obj as MedicamentCreateModel;
                      HttpResponseMessage response = client.PostAsJsonAsync(PATH, med).Result;
                      if (response.IsSuccessStatusCode)
                      {
                          MessageBox.Show("User Created ");
                          parent.fetchMedicaments();
                          CloseAction();
                      }
                      else
                      {
                          MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                      }
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CallFetch()
        {
            
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
