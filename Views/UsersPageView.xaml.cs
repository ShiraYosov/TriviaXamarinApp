using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaXamarinApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriviaXamarinApp.Models;

namespace TriviaXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersPageView : ContentPage
    {
        public UsersPageView()
        {
            this.BindingContext = new UsersPageViewModel();

            this.Title = "Users Page";
            InitializeComponent();
        }

       
        private void Question_Clicked(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button b = (Button)sender;
                AmericanQuestion chosenQuestion = (AmericanQuestion)b.BindingContext;

                Page QuestionPage = new TheQuestionView();
                TheQuestionViewModel QuestionContext = new TheQuestionViewModel
                {
                    QuestionText = chosenQuestion.QText,
                    CorrectAnswer = chosenQuestion.CorrectAnswer,
                    OtherAnswers = chosenQuestion.OtherAnswers
                };
                QuestionPage.BindingContext = QuestionContext;

                App.Current.MainPage.Navigation.PushAsync(QuestionPage);
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Page p = new QuestionsView();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
    }
}