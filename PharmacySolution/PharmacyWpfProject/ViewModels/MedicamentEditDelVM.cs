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
    class MedicamentEditDelVM : INotifyPropertyChanged
    {
        private const string PATH = "http://localhost:3365/Medicament";
        static HttpClient client = new HttpClient();

        private RelayCommand putCommand;
        private RelayCommand deleteCommand;

        private MedicamentEditDelModel currentMedicament;
        private MedicamentMainVM parent;


        public Action CloseAction { get; set; }

        public MedicamentEditDelModel CurrentMedicament
        {
            get { return currentMedicament; }
            set
            {
                currentMedicament = value;
                OnPropertyChanged("CurrentMedicament");
            }
        }
        public MedicamentEditDelVM(MedicamentEditDelModel currentMedicament, MedicamentMainVM parentViewModel)
        {
            CurrentMedicament = currentMedicament;
            parent = parentViewModel;

            //client.BaseAddress = new Uri(PATH);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public RelayCommand PutCommand
        {
            get
            {
                return putCommand ??
                  (putCommand = new RelayCommand(obj =>
                  {
                      MedicamentEditDelModel med = obj as MedicamentEditDelModel;
                      HttpResponseMessage response = client.PutAsJsonAsync(PATH, med).Result;
                      if (response.IsSuccessStatusCode)
                      {
                          MessageBox.Show("User Edited ");
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

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand(obj =>
                  {
                      MedicamentEditDelModel med = obj as MedicamentEditDelModel;
                      var url = "/" + med.id;
                      HttpResponseMessage response = client.DeleteAsync(PATH + url).Result;

                      if (response.IsSuccessStatusCode)
                      {
                          MessageBox.Show("User Deleted ");
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
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
