using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinFirebase.Views.Student
{
    public partial class StudentEntryPage : ContentPage
    {
        StudentRepository repository = new StudentRepository();
        public StudentEntryPage()
        {
            InitializeComponent();
        }

        private async void ButtonSave_Clicked(System.Object sender, System.EventArgs e)
        {
            string name = TxtName.Text;
            string email = TxtEmail.Text;
            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("Warning", "Please enter your name", "Cancel");
            }
            if (string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Warning", "Please enter your email", "Cancel");
            }

            StudentModel student = new StudentModel();
            student.Name = name;
            student.Email = email;

            var isSaved = await repository.Save(student);
            if (isSaved)
            {
                await DisplayAlert("Information", "Studnet has been added", "Cancel");
                Clear();
            }
            else
            {
                await DisplayAlert("Error", "Studnet saved failed", "Cancel");
            }
        }

        private void Clear()
        {
            TxtName.Text = string.Empty;
            TxtEmail.Text = string.Empty;
        }
    }
}
