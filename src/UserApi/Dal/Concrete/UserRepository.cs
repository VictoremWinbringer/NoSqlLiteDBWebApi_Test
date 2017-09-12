using LiteDB;
using System.Collections.Generic;
using System.Linq;
using UserApi.Dal.Abstract;
using UserApi.Models;

namespace UserApi.Dal.Concrete
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly string connection;

        public UserRepository(string connection)
        {
            this.connection = connection;
        }

        public void Create(User user)
        {
            using (var db = new LiteDatabase(connection))
            {
                var users = db.GetCollection<User>();

                users.Insert(user);
            }
        }

        public void Delete(int id)
        {
            using (var db = new LiteDatabase(connection))
            {
                var users = db.GetCollection<User>();

                users.Delete(u => u.Id == id);
            }
        }

        public User Get(int id)
        {
            using (var db = new LiteDatabase(connection))
            {
                var users = db.GetCollection<User>();

                return users.FindOne(u => u.Id == id);
            }
        }

        public IEnumerable<User> Get()
        {
            using (var db = new LiteDatabase(connection))
            {
                var users = db.GetCollection<User>();

                return users.FindAll().ToList();
            }
        }

        public void Update(User user)
        {
            using (var db = new LiteDatabase(connection))
            {
                var users = db.GetCollection<User>();

                users.Update(user);
            }
        }
    }
}
