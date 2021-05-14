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
    class EditQuestionViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        ICommand EditCommand => new Command(Edit);

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

        private string[] answersList ;
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
        public AmericanQuestion AQ { get; set; }

        

        public ICommand EditQuestionCommand => new Command(Edit);
        public async void Edit()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            App a = (App)App.Current;

           await proxy.DeleteQuestion(AQ);

            AmericanQuestion newQ = new AmericanQuestion
            {
                QText=QText,
                OtherAnswers=AnswersList,
                CorrectAnswer=CorrectAns,
                CreatorNickName=a.User.NickName
            };

            await proxy.PostNewQuestion(newQ);
            await App.Current.MainPage.Navigation.PushAsync(new UsersPageView());


        }

        }
    }

