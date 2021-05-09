using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Views;


namespace TriviaXamarinApp.ViewModels
{
    public class AnswerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Answer { get; set; }
        public Color color;
        public Color Color
        {
            get { return this.color; }

            set
            {
                if (this.color != value)
                {
                    this.color = value;
                    OnPropertyChanged(nameof(Color));
                }
            }
        }
    }
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

        public ObservableCollection<AnswerViewModel> AnswersList { get; set; }

        public QuestionsViewModel()
        {
            AnswersList = new ObservableCollection<AnswerViewModel>();
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
            foreach (string s in arr)
            {
                this.AnswersList.Add(new AnswerViewModel
                {
                    Answer = s,
                    Color = Color.Black
                });
            }
            AText = AQ.QText;

        }

        public ICommand CheckCommand => new Command<AnswerViewModel>(Answer);

        public void Answer(AnswerViewModel s)
        {
            if (s.Answer == AQ.CorrectAnswer)
            {
                s.Color = Color.Green;
            }

            else
            {
                s.Color = Color.Red;
            }

            
        }

        public ICommand NextCommand => new Command(Next);
        public async void Next()
        {
            Page p = new QuestionsView();
            await App.Current.MainPage.Navigation.PushAsync(p);

        }

    }
}
