using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaXamarinApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriviaXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersPageView : ContentPage
    {
        public UsersPageView()
        {
            UsersPageViewModel context = new UsersPageViewModel();
            context.NavigateToPageEvent += NavigateToAsync;
            this.BindingContext = context; 
            
            this.Title = "Users Page";
            InitializeComponent();
        }

        public async void NavigateToAsync(Page p)
        {
            await Navigation.PushAsync(p);
        }

       
    }
}