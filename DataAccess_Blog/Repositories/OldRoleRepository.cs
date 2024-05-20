using Dapper.Contrib.Extensions;
using DataAccess_Blog.Models;
using Microsoft.Data.SqlClient;

namespace DataAccess_Blog.Repositories
{
    /// <summary>
    /// Criado antes de se criar o repositório genérico
    /// </summary>
    public class OldRoleRepository
    {
        private readonly SqlConnection _connection;

        public OldRoleRepository(SqlConnection connection){
            _connection = connection;
        }
        public List<Role> GetAll() => (List<Role>)_connection.GetAll<Role>();
            
        public Role Get(int id) => _connection.Get<Role>(id);

        public void Create(Role role) => _connection.Insert(role);

        public void Update(Role role){
            if(role.Id > 0)
                _connection.Update(role);
        }

        public void Delete(Role role){
            if(role.Id > 0)
                _connection.Delete(role);
        }  

        public void Delete(int id){
            if(id <= 0) return;

            var role = Get(id);

            if(role != null)
                _connection.Delete(role);
        }
    }
}