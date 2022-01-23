using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinFirebase.Views.Student
{
    public partial class StudentDetails : ContentPage
    {
        public StudentDetails(StudentModel student)
        {
            InitializeComponent();
            LabelName.Text = student.Name;
            LabelEmail.Text = student.Email;
        }
    }
}
