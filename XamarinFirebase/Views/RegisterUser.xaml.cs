using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinFirebase.Views
{
    public partial class RegisterUser : ContentPage
    {
        UserRepository _userRepository = new UserRepository();
        public RegisterUser()
        {
            InitializeComponent();
        }

        private async void ButtonRegister_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                string name = TxtName.Text;
                string email = TxtEmail.Text;
                string password = TxtPassword.Text;
                string confirmPassword = TxtConfirmPass.Text;
                if (string.IsNullOrEmpty(name))
                {
                    await DisplayAlert("Warning", "Type name", "ok");
                    return;
                }
                if (string.IsNullOrEmpty(email))
                {
                    await DisplayAlert("Warning", "Type email", "ok");
                    return;
                }
                if (password.Length < 6)
                {
                    await DisplayAlert("Warning", "Password should be 6 digit.", "ok");
                    return;
                }
                if (string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Warning", "Type password", "ok");
                    return;
                }
                if (string.IsNullOrEmpty(confirmPassword))
                {
                    await DisplayAlert("Warning", "Type confirm Password", "ok");
                    return;
                }
                if (password != confirmPassword)
                {
                    await DisplayAlert("Warning", "Password not match", "ok");
                    return;
                }
                bool IsSave = await _userRepository.Register(email, name, password);
                if (IsSave)
                {
                    await DisplayAlert("Register user", "Registeration completed", "ok");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Registation failed", "ok");
                }
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("EMAIL_EXISTS"))
                {
                    await DisplayAlert("Warning", "Email exist", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", exception.Message, "Ok");
                }
            }

        }
    }
}
