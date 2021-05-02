using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriviaXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageView : ContentPage
    {
        public HomePageView()
        {
            InitializeComponent();
            this.Title = "Home Page";
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            Page p = new LoginView();
            App.Current.MainPage.Navigation.PushAsync(p);
        }

        private void Register_Clicked(object sender, EventArgs e)
        {
            Page p = new RegisterView();
            App.Current.MainPage.Navigation.PushAsync(p);
        }

        private void ViewQuestions_Clicked(object sender, EventArgs e)
        {
            Page p = new QuestionsView();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
    }
}