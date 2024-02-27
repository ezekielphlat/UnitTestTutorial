
using CloudCustomer.API.Models;

namespace CloudCustomer.API.Services
{
    public interface IUsersService
    {
         Task<List<User>> GetAllUsers();

    }
}


