using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinFirebase.Views
{
    public partial class ForgetPasswordPage : ContentPage
    {
        UserRepository _userRepository = new UserRepository();
        public ForgetPasswordPage()
        {
            InitializeComponent();
        }

        private async void ButtonSpendLink_Clicked(System.Object sender, System.EventArgs e)
        {
            string email = TxtEmail.Text;
            if (string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Warning", "Please enter your email.", "Ok");
                return;
            }

            bool isSend = await _userRepository.ResetPassword(email);
            if (isSend)
            {
                await DisplayAlert("Reset Password", "Send link in your email.", "Ok");
            }
            else
            {
                await DisplayAlert("Reset Password", "Link sending failed.", "Ok");
            }
        }
    }
}
