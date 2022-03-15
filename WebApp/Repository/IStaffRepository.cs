using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Repository
{
    public interface IStaffRepository
    {
        Task<IEnumerable<Staff>> GetAll(int? page);
        Task<Staff> GetByID(int id);
        Task<Staff> Create(Staff staff);
        Task<Staff> Update(Staff staff);
        Task Delete(int staffID);
    }
}