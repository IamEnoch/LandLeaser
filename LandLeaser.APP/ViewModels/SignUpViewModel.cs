using CommunityToolkit.Mvvm.Input;
using LandLeaser.APP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandLeaser.APP.ViewModels
{
    public partial class SignUpViewModel : BaseViewModel
    {
        public SignUpViewModel()
        {
            Title = nameof(SignUpPage);
        }

        [RelayCommand]
        public async Task DisplayLoginPage()
        {
            await Shell.Current.GoToAsync("/LoginPage");
        }

    }
}
