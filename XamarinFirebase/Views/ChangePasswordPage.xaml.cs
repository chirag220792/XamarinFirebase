using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinFirebase.Views
{
    public partial class ChangePasswordPage : ContentPage
    {
        UserRepository _userRepository = new UserRepository();
        public ChangePasswordPage()
        {
            InitializeComponent();
        }

        private async void BtnChangePassword_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                string password = TxtPassword.Text;
                string confirmPassword = TxtConfirmPassword.Text;
                if (string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Change Password", "Please type password", "ok");
                    return;
                }
                if (string.IsNullOrEmpty(confirmPassword))
                {
                    await DisplayAlert("Change Password", "Please type Confirm password", "ok");
                    return;
                }
                if(password != confirmPassword)
                {
                    await DisplayAlert("Change Password", "Confirm password not match", "ok");
                    return;
                }
                string token =Preferences.Get("token", "");
                string newToken =await _userRepository.ChangePassword(token, password);
                if (!string.IsNullOrEmpty(newToken))
                {
                    await DisplayAlert("Change Password", "Password has been changed", "ok");
                    Preferences.Set("token", newToken);
                    //Preferences.Clear();
                    //await Navigation.PushAsync(new LoginPage());
                }
                else
                {
                    await DisplayAlert("Change Password", "Password changing failed", "ok");
                }
            }
            catch (Exception exception)
            {

            }
        }
    }
}
