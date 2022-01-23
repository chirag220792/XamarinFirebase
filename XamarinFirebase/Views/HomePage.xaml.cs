using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinFirebase.Views.Student;

namespace XamarinFirebase.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            LblUser.Text = Preferences.Get("userEmail","Default");
        }

        private void BtnStudentList_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new StudentListPage());
        }

        private void BtnChangePassword_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ChangePasswordPage());
        }
    }
}
