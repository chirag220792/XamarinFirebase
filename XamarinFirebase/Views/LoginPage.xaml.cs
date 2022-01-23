using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinFirebase.Views.Student;

namespace XamarinFirebase.Views
{
    public partial class LoginPage : ContentPage
    {
        UserRepository _userRepository = new UserRepository();

        public LoginPage()
        {
            InitializeComponent();
            bool hasKey = Preferences.ContainsKey("token");
            if (hasKey)
            {
                string token = Preferences.Get("token", "");
                if (string.IsNullOrEmpty(token))
                {
                    Navigation.PushAsync(new HomePage());
                }
            }
        }

        private async void BtnSignIn_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                string email = TxtEmail.Text;
                string password = TxtPassword.Text;
                if (string.IsNullOrEmpty(email))
                {
                    await DisplayAlert("Warning", "Type your email", "ok");
                    return;
                }
                if (string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Warning", "Type your password", "ok");
                    return;
                }
                string token = await _userRepository.SignIn(email, password);
                if (!string.IsNullOrEmpty(token))
                {
                    Preferences.Set("token", token);
                    Preferences.Set("userEmail", email);
                    await Navigation.PushAsync(new HomePage());
                }
                else
                {
                    await DisplayAlert("Sign In", "Sign in failed", "ok");
                }
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("EMAIL_NOT_FOUND"))
                {
                    await DisplayAlert("Unauthorized", "Email not found", "ok");
                }
                else if (exception.Message.Contains("INVALID_PASSWORD"))
                {
                    await DisplayAlert("Unauthorized", "Password incorrect", "ok");
                }
                else
                {
                    await DisplayAlert("Error", exception.Message, "ok");
                }
            }
        }

        private async void RegisterTap_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterUser());
        }

        private async void ForgotTap_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new ForgetPasswordPage());
        }
    }
}
