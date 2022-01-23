using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinFirebase.Views.Student
{
    public partial class StudentListPage : ContentPage
    {
        StudentRepository studentRepository = new StudentRepository();
        public StudentListPage()
        {
            InitializeComponent();
            StudentListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });

        }

        protected override async void OnAppearing()
        { 
            
            var students = await studentRepository.GetAll();
            StudentListView.ItemsSource = null;
            StudentListView.ItemsSource = students;
            StudentListView.IsRefreshing = false;
        }

        private void AddToolBarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new StudentEntryPage());
        }

        private void StudentListView_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if(e.Item == null)
            {
                return;
            }
            var student = e.Item as StudentModel;
            Navigation.PushModalAsync(new StudentDetails(student));
            ((ListView)sender).SelectedItem = null;
        }

        private async void EditTap_Tapped(System.Object sender, System.EventArgs e)
        {
            //DisplayAlert("Edit", "Do you want to edit?", "Ok");
            string id = ((TappedEventArgs)e).Parameter.ToString();
            var student = await studentRepository.GetById(id);
            if(student == null)
            {
                await DisplayAlert("Warning", "Data not found.", "Ok");
            }
            student.Id = id;
            await Navigation.PushModalAsync(new StudentEdit(student));
        }

        private async void DeleteTap_Tapped(System.Object sender, System.EventArgs e)
        {
            var response = await DisplayAlert("Delete","Do you want to delete?","Yes", "No");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await studentRepository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Information", "Student has been delete.", "ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Student deleted failed.", "ok");
                }
            }
        }

        private async void TxtSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if (!string.IsNullOrEmpty(searchValue))
            {
                var students = await studentRepository.GetAllByName(searchValue);
                StudentListView.ItemsSource = null;
                StudentListView.ItemsSource = students;
            }
            else
            {
                OnAppearing();
            }
        }

        private async void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if (!string.IsNullOrEmpty(searchValue))
            {
                var students = await studentRepository.GetAllByName(searchValue);
                StudentListView.ItemsSource = null;
                StudentListView.ItemsSource = students;
            }
            else
            {
                OnAppearing();
            }
        }

        private async void EditMenuItem_Clicked(System.Object sender, System.EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var student = await studentRepository.GetById(id);
            if (student == null)
            {
                await DisplayAlert("Warning", "Data not found.", "Ok");
            }
            student.Id = id;
            await Navigation.PushModalAsync(new StudentEdit(student));
        }

        private async void DeleteMenuItem_Clicked(System.Object sender, System.EventArgs e)
        {
            var response = await DisplayAlert("Delete", "Do you want to delete?", "Yes", "No");
            if (response)
            {
                string id = ((MenuItem)sender).CommandParameter.ToString();
                bool isDelete = await studentRepository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Information", "Student has been delete.", "ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Student deleted failed.", "ok");
                }
            }
        }

        private async void EditSwipeItem_Invoked(System.Object sender, System.EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var student = await studentRepository.GetById(id);
            if (student == null)
            {
                await DisplayAlert("Warning", "Data not found.", "Ok");
            }
            student.Id = id;
            await Navigation.PushModalAsync(new StudentEdit(student));
        }
    }
}
