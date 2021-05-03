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
    class UsersPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<AmericanQuestion> QuestionsList { get; }

        public UsersPageViewModel()
        {
            QuestionsList = new ObservableCollection<AmericanQuestion>();
            CreateQuestionCollection();
        }

        private void CreateQuestionCollection()
        {
            App a = (App)App.Current;
            List<AmericanQuestion> theQuestions = a.User.Questions;
            foreach (AmericanQuestion q in theQuestions)
            {
                this.QuestionsList.Add(q);
            }
        }

        public ICommand DeleteCommand => new Command<AmericanQuestion>(RemoveQuestion);
        void RemoveQuestion(AmericanQuestion aq)
        {
            if (QuestionsList.Contains(aq))
            {
                QuestionsList.Remove(aq);
            }

        }
    }
}
