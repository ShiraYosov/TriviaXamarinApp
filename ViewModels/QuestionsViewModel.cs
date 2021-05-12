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
        private bool pressed = false;
        public ObservableCollection<AnswerViewModel> AnswersList { get; set; }

        public bool click;
        public bool Click
        {
            get { return this.click; }

            set
            {
                if (this.click != value)
                {
                    this.click = value;
                    OnPropertyChanged(nameof(Click));
                }
            }
        }
        public static int count = 0;

        public QuestionsViewModel()
        {
            Click = false;
            Message = "";
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
            if (!pressed)
            {
                if (s.Answer == AQ.CorrectAnswer)
                {
                    s.Color = Color.Green;
                    count++;
                }

                else
                {
                    s.Color = Color.Red;
                }
                pressed = true;

                if (count != 0 && count % 3 == 0)
                {
                    Click = true;
                }

            }

        }

        public ICommand NextCommand => new Command(Next);
        public void Next()
        {
            Page p = new QuestionsView();
            App.Current.MainPage.Navigation.PushAsync(p);
        }

        public ICommand AddQues => new Command(Add);
        public void Add()
        {
            App a = (App)App.Current;
            if (a.User != null)
            {
                Page p = new AddQuestionView();
                App.Current.MainPage.Navigation.PushAsync(p);
            }
            else
                Message = "Sorry... Login is required to enter this field";
        }

        public ICommand UsersCommand => new Command(Users);
        public void Users()
        {
            App a = (App)App.Current;
            if (a.User != null)
            {
                Page p = new UsersPageView();
                App.Current.MainPage.Navigation.PushAsync(p);
            }
            else
                Message = "Sorry... Login is required to enter this field";
        }

    }
}
