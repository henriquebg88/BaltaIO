using Dapper.Contrib.Extensions;
using DataAccess_Blog.Models;
using Microsoft.Data.SqlClient;

namespace DataAccess_Blog.Repositories
{
    /// <summary>
    /// Criado antes de se criar o repositório genérico
    /// </summary>
    public class OldUserRepository
    {
        private readonly SqlConnection _connection;

        public OldUserRepository(SqlConnection connection){
            _connection = connection;
        }
        public List<User> GetAll() => (List<User>)_connection.GetAll<User>();
            
        public User Get(int id) => _connection.Get<User>(id);

        public void Create(User user) => _connection.Insert(user);

        public void Update(User user){
            if(user.Id > 0)
                _connection.Update(user);
        }

        public void Delete(User user){
            if(user.Id > 0)
                _connection.Delete(user);
        }  

        public void Delete(int id){
            if(id <= 0) return;

            var user = Get(id);

            if(user != null)
                _connection.Delete(user);
        }
    }
}