using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Storage;
using Newtonsoft.Json;

namespace XamarinFirebase
{
    public class StudentRepository
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://xamarinfirebase-eee1c-default-rtdb.asia-southeast1.firebasedatabase.app/");
        FirebaseStorage firebaseStorage = new FirebaseStorage("xamarinfirebase-eee1c.appspot.com");

        public async Task<bool> Save(StudentModel student)
        {
            var data = await firebaseClient.Child(nameof(StudentModel)).PostAsync(JsonConvert.SerializeObject(student));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        public async Task<List<StudentModel>> GetAll()
        {
            return (await firebaseClient.Child(nameof(StudentModel)).OnceAsync<StudentModel>()).Select(item => new StudentModel
            {
                Id = item.Key,
                Name = item.Object.Name,
                Email = item.Object.Email,
                Image = item.Object.Image,
            }).ToList();
        }

        public async Task<List<StudentModel>> GetAllByName(string name)
        {
            return (await firebaseClient.Child(nameof(StudentModel)).OnceAsync<StudentModel>()).Select(item => new StudentModel
            {
                Id = item.Key,
                Name = item.Object.Name,
                Email = item.Object.Email,
                Image = item.Object.Image,
            }).Where(c => c.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        public async Task<StudentModel>GetById(string id)
        {
            return (await firebaseClient.Child(nameof(StudentModel) + "/" + id).OnceSingleAsync<StudentModel>());
        }

        public async Task<bool> Update(StudentModel student)
        {
            await firebaseClient.Child(nameof(StudentModel) + "/" + student.Id).PutAsync(JsonConvert.SerializeObject(student));
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            await firebaseClient.Child(nameof(StudentModel) + "/" + id).DeleteAsync();
            return true;
        }

        public async Task<string>Upload(Stream img, string fileName)
        {
            var image = await firebaseStorage.Child("Images").Child(fileName).PutAsync(img);
            return image;
        }
    }
}
