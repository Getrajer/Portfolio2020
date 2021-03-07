using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Services.JsonPlaceholderApiServices
{
    public interface IUserService
    {
        Task<IEnumerable> GetAllUsers();
    }
}
