using System.Collections.Generic;
using UserApi.Models;

namespace UserApi.Dal.Abstract
{
    public interface IUserRepository
    {
        User Get(int id);
        IEnumerable<User> Get();
        void Create(User user);
        void Update(User user);
        void Delete(int id);
    }
}
