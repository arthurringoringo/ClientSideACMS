using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
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

        public async Task<string> UploadRecipt(PaidSessionDTO model)
        {
            //var modelInJson = JsonConvert.SerializeObject(model);
            //var stringcontent = new StringContent(modelInJson);
            //StringContent classid = new StringContent(model.ClassId.ToString());
            //StringContent studentid = new StringContent(model.StudentId.ToString());
            StringContent RegistrationClassID = new StringContent(model.RegistredClassId);
            StringContent paymentmonth = new StringContent(model.PaymentsMonth.ToString());
            StringContent picturelink = new StringContent(model.PictureLink);

            var multiPartContent = new MultipartFormDataContent();
            //var imageBytes = File.ReadAllBytes(imagePath); ;
            ByteArrayContent baContent = new ByteArrayContent(model.Image.ToArray());
            
            multiPartContent.Add(baContent, "\"image\"", model.PictureLink);
            //multiPartContent.Add(studentid, "StudentId");
            //multiPartContent.Add(classid, "ClassId");
            multiPartContent.Add(picturelink, "PictureLink");
            multiPartContent.Add(RegistrationClassID, "RegistredClassId");
            multiPartContent.Add(paymentmonth, "PaymentsMonth");
            //multiPartContent.Add(stringcontent);


            var result = await client.PostAsync("/Student/payment/upload/reciept", multiPartContent);

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
        public List<PaidSession> GetPaidSession(string id)
        {


            var result = client.GetAsync("/Student/paidsession/" + id).Result;

            result.EnsureSuccessStatusCode();

            var httpResponseMessage = result.Content.ReadAsStringAsync().Result;

            var paidSession = JsonConvert.DeserializeObject<List<PaidSession>>(httpResponseMessage);

            return paidSession;


        }
        public Student GetProfileUser(string id)
        {


            var result = client.GetAsync("/Student/profile/user/" + id).Result;

            result.EnsureSuccessStatusCode();

            var httpResponseMessage = result.Content.ReadAsStringAsync().Result;

            var profile = JsonConvert.DeserializeObject<Student>(httpResponseMessage);

            return profile;


        }

        public string RegisterClass(RegistredClassDTO model)
        {
            var modelInJson = JsonConvert.SerializeObject(model);

            var result = client.PostAsync("/Student/class/register", new StringContent(modelInJson, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

            if (result.IsSuccessStatusCode)
            {
                var response = result.Content;

                return response.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            else 
            {
                return "False";
            }
        }

        public async Task<string> UpdateProfile(Student model)
        {
            var modelInJson = JsonConvert.SerializeObject(model);

            var result = await client.PostAsync("/Student/profile/update", new StringContent(modelInJson, Encoding.UTF8, "application/json"));

            result.EnsureSuccessStatusCode();

            var httpResponseMessage = await result.Content.ReadAsStringAsync();

            return httpResponseMessage;
        }
        public async Task<List<ClassCategory>> GetClassCategory()
        {
            var result = await client.GetAsync("/Student/classcategory");

            result.EnsureSuccessStatusCode();

            var httResponseMessage = result.Content.ReadAsStringAsync().Result;

            var classCategory = JsonConvert.DeserializeObject<List<ClassCategory>>(httResponseMessage);

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
        public async Task<List<RegistredClass>> GetStudentRegistredClass(Guid id)
        {
            var result = await client.GetAsync("/Student/registred/class/"+id);

            result.EnsureSuccessStatusCode();

            var httResponseMessage = result.Content.ReadAsStringAsync().Result;

            var registredClass = JsonConvert.DeserializeObject<List<RegistredClass>>(httResponseMessage);

            return registredClass;
        }

        public async Task<string> RegisterClassAsync(RegistredClassDTO model)
        {
            var modelInJson = JsonConvert.SerializeObject(model);

            var result = await client.PostAsync("/Student/class/register", new StringContent(modelInJson, Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
            {
                var response = result.Content;

                return await response.ReadAsStringAsync();
            }
            else
            {
                return "False";
            }
        }
    }

}
