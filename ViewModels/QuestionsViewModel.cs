using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;


namespace TriviaXamarinApp.ViewModels
{
    class QuestionsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string aText;
        public string AText
        {
            get { return this.aText; }

            set
            {
                if (this.aText != value)
                {
                    this.aText = value;
                    OnPropertyChanged(nameof(AText));
                }
            }
        }
        public AmericanQuestion AQ { get; set; }

        public ObservableCollection<string> AnswersList { get; set; }

        public QuestionsViewModel()
        {
            AnswersList = new ObservableCollection<string>();
            AQ = new AmericanQuestion();
            CreateQuestion();
            
        }

        private async void CreateQuestion()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            this.AQ = await proxy.GetRandomQuestion();
            Random r = new Random();
            string[] arr = new string[4];

           arr[r.Next(0, 4)] = AQ.CorrectAnswer;
            int counter = 0;
            for (int i = 0; i < 4; i++)
            {
                if (arr[i] == null)
                {
                    arr[i] = AQ.OtherAnswers[counter];
                    counter++;
                }
            }
            foreach(string s in arr)
            {
                this.AnswersList.Add(s);
            }
            AText = AQ.QText;
        }

    }
}
