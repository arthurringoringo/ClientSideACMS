using ACMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientSideACMS.Data
{
    public interface IStudentApiServices
    {
        Task<string> CreateAccount(Student model);
        Task<string> UploadRecipt(PaidSessionDTO model, string imagePath);
        public Student GetProfile(Guid id);
        Task<string> RegisterClass(RegistredClassDTO model);
        Task<string> UpdateProfile(Student model);
        Task<List<ClassCategoryDGO>> GetClassCategory();
        Task<List<AvailableClassDTO>> GetAvailableClass();
        Task<List<PaymentMethodDTO>> GetPaymentMethod();
        public Student GetProfileUser(int id);


    }
}
