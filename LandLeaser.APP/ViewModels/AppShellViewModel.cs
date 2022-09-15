using CommunityToolkit.Mvvm.Input;
using LandLeaser.APP.ViewModels;
using LandLeaser.APP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandLeaserApp.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {
        [RelayCommand]
        public async Task DisplayLoginPage()
        {
            await Shell.Current.GoToAsync(nameof(SignUpPage));
        }
    }
}
