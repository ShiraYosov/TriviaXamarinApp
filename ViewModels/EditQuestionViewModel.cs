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
    class EditQuestionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        

        private string correctAns;
        public string CorrectAns
        {
            get { return this.correctAns; }

            set
            {
                if (this.correctAns != value)
                {
                    this.correctAns = value;
                    OnPropertyChanged(nameof(CorrectAns));
                }
            }
        }
        private string qText;
        public string QText
        {
            get { return this.qText; }

            set
            {
                if (this.qText != value)
                {
                    this.qText = value;
                    OnPropertyChanged(nameof(QText));
                }
            }
        }

        private string[] answersList;
        public string[] AnswersList
        {
            get { return this.answersList; }

            set
            {
                if (this.answersList != value)
                {
                    this.answersList = value;
                    OnPropertyChanged(nameof(AnswersList));
                }
            }
        }

        private string message;
        public string Message
        {
            get { return this.message; }

            set
            {
                if (this.message != value)
                {
                    this.message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }
        public AmericanQuestion AQ { get; set; }

        public EditQuestionViewModel()
        {
            Message = "";
        }

        
        public ICommand EditQuestionCommand => new Command(Edit);
        public async void Edit()
        {
            if (QText != "" && CorrectAns != "" && AnswersList[0] != "" && AnswersList[1] != "" && AnswersList[2] != "")
            {
                TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
                App a = (App)App.Current;

                await proxy.DeleteQuestion(AQ);

                AmericanQuestion newQ = new AmericanQuestion
                {
                    QText = QText,
                    OtherAnswers = AnswersList,
                    CorrectAnswer = CorrectAns,
                    CreatorNickName = a.User.NickName
                };

                await proxy.PostNewQuestion(newQ);
                await App.Current.MainPage.Navigation.PushAsync(new UsersPageView());
            }

            else
            { this.Message = "Could not edit your question! One field or more is missing"; }
        }

    }
}

