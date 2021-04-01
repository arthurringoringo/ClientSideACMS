using ACMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ClientSideACMS.Data
{
    public interface IStudentApiServices
    {
        Task<string> CreateAccount(Student model);
        Task<string> UploadRecipt(PaidSessionDTO model);
        public Student GetProfile(Guid id);
        string RegisterClass(RegistredClassDTO model);
        Task<string> RegisterClassAsync(RegistredClassDTO model);
        Task<string> UpdateProfile(Student model);
        Task<List<ClassCategoryDGO>> GetClassCategory();
        Task<List<AvailableClassDTO>> GetAvailableClass();
        Task<List<PaymentMethodDTO>> GetPaymentMethod();
        public Student GetProfileUser(string id);
        Task<List<RegistredClass>> GetStudentRegistredClass(Guid id);
        public List<PaidSession> GetPaidSession(string id);


    }
}
