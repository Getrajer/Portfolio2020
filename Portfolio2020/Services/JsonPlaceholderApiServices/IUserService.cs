using Portfolio2020.Models.JsonPlaceholderApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Services.JsonPlaceholderApiServices
{
    public interface IUserService
    {
        Task<IEnumerable<JPUser>> GetAllUsers();
        Task<JPUser> GetUserById(int userId);
        Task<JPUser> AddNewUser(JPUser newUser);
        Task<JPUser> UpdateUser(JPUser userToUpdate);
        Task DeleteUser(int userId);
    }
}
