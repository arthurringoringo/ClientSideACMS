using System.Threading.Tasks;
using ACMS.DAL.Models;

namespace ClientSideACMS.Data
{
    public interface IAPIServiceExtension
    {
        Task<string> SendEmail(EmailDto emailDto);
    }
}