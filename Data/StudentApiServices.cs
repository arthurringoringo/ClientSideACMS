using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ACMS.DAL.Models;
using System.Text;
using System.IO;

namespace ClientSideACMS.Data
{
    public class StudentApiServices : IStudentApiServices
    {
        private HttpClient client = new HttpClient();

        public StudentApiServices(HttpClient Client)
        {
            client = Client;
            ////client.BaseAddress = new Uri("https://localhost:5001/");
        }

        public async Task<string> CreateAccount(Student model)
        {
            var modelInJson = JsonConvert.SerializeObject(model);

            var result = await client.PostAsync("/Student/account/create", new StringContent(modelInJson, Encoding.UTF8, "application/json"));

            result.EnsureSuccessStatusCode();

            var httpResponseMessage = await result.Content.ReadAsStringAsync();

            return httpResponseMessage;
        }

        public async Task<string> UploadRecipt(PaidSessionDTO model, string imagePath)
        {
            //var modelInJson = JsonConvert.SerializeObject(model);
            //var stringcontent = new StringContent(modelInJson);
            StringContent classid = new StringContent(model.ClassId.ToString());
            StringContent studentid = new StringContent(model.StudentId.ToString());
            StringContent paymentmonth = new StringContent(model.PaymentsMonth.ToString());
            
            var multiPartContent = new MultipartFormDataContent();
            var imageBytes = File.ReadAllBytes(imagePath); ;
            ByteArrayContent baContent = new ByteArrayContent(imageBytes);
            
            multiPartContent.Add(baContent, "File", model.Image.FileName);
            multiPartContent.Add(studentid, "StudentId");
            multiPartContent.Add(classid, "ClassId");
            multiPartContent.Add(paymentmonth, "PaymentsMonth");
            //multiPartContent.Add(stringcontent);


            var result = await client.PostAsync("/payment/upload/reciept", multiPartContent);

            result.EnsureSuccessStatusCode();

            var httpResponseMessage = await result.Content.ReadAsStringAsync();

            return httpResponseMessage;
        }
        //public async Task<string> Upload(PaidSessionDTO model)
        //{
        //    var modelInJson = JsonConvert.SerializeObject(model);

        //    var result = await client.PostAsync("/payment/upload/reciept", new MultipartFormDataContent(modelInJson, Encoding.UTF8, "multipart/form-data"));

        //    result.EnsureSuccessStatusCode();

        //    var httpResponseMessage = await result.Content.ReadAsStringAsync();

        //    return httpResponseMessage;
        //}

        public Student GetProfile(Guid id)
        {
            

            var result = client.GetAsync("/Student/profile/"+id).Result;

            result.EnsureSuccessStatusCode();

            var httpResponseMessage = result.Content.ReadAsStringAsync().Result;

            var profile = JsonConvert.DeserializeObject<Student>(httpResponseMessage);

            return profile;


        }
        public Student GetProfileUser(int id)
        {


            var result = client.GetAsync("/Student/profile/user/" + id).Result;

            result.EnsureSuccessStatusCode();

            var httpResponseMessage = result.Content.ReadAsStringAsync().Result;

            var profile = JsonConvert.DeserializeObject<Student>(httpResponseMessage);

            return profile;


        }

        public async Task<string> RegisterClass(RegistredClassDTO model)
        {
            var modelInJson = JsonConvert.SerializeObject(model);

            var result = await client.PostAsync("/Student/class/register", new StringContent(modelInJson, Encoding.UTF8, "application/json"));

            result.EnsureSuccessStatusCode();

            var httpResponseMessage = await result.Content.ReadAsStringAsync();

            return httpResponseMessage;
        }

        public async Task<string> UpdateProfile(Student model)
        {
            var modelInJson = JsonConvert.SerializeObject(model);

            var result = await client.PostAsync("/Student/profile/update", new StringContent(modelInJson, Encoding.UTF8, "application/json"));

            result.EnsureSuccessStatusCode();

            var httpResponseMessage = await result.Content.ReadAsStringAsync();

            return httpResponseMessage;
        }
        public async Task<List<ClassCategoryDGO>> GetClassCategory()
        {
            var result = await client.GetAsync("/Student/classcategory");

            result.EnsureSuccessStatusCode();

            var httResponseMessage = result.Content.ReadAsStringAsync().Result;

            var classCategory = JsonConvert.DeserializeObject<List<ClassCategoryDGO>>(httResponseMessage);

            return classCategory;
        }
        public async Task<List<AvailableClassDTO>> GetAvailableClass()
        {
            var result = await client.GetAsync("/Student/availableclass");

            result.EnsureSuccessStatusCode();

            var httResponseMessage = result.Content.ReadAsStringAsync().Result;

            var availableClass = JsonConvert.DeserializeObject<List<AvailableClassDTO>>(httResponseMessage);

            return availableClass;
        }
        public async Task<List<PaymentMethodDTO>> GetPaymentMethod()
        {
            var result = await client.GetAsync("/Student/paymentmethod");

            result.EnsureSuccessStatusCode();

            var httResponseMessage = result.Content.ReadAsStringAsync().Result;

            var availableClass = JsonConvert.DeserializeObject<List<PaymentMethodDTO>>(httResponseMessage);

            return availableClass;
        }

    }

}
