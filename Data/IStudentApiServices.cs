using ACMS.DAL.Models;
using System;
using System.Threading.Tasks;

namespace ClientSideACMS.Data
{
    public interface IStudentApiServices
    {
        Task<string> CreateAccount(Student model);
        Task<string> UploadRecipt(PaidSessionDTO model, string imagePath);
        public Student GetProfile(Guid id);
        Task<string> RegisterClass(RegistredClass model);
        Task<string> UpdateProfile(Student model);

    }
}
