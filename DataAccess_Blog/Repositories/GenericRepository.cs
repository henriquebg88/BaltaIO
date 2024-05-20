using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace DataAccess_Blog.Repositories
{
    public class GenericRepository<T> where T : class
    {
        private readonly SqlConnection _connection;

        public GenericRepository(SqlConnection connection){
            _connection = connection;
        }

        public List<T> GetAll() => (List<T>)_connection.GetAll<T>();

        public T Get(int id) => _connection.Get<T>(id);

        public void Create(T model) => _connection.Insert(model);

        public void Update(T model) => _connection.Update(model);
        
        public void Delete(T model) => _connection.Delete(model);  
        public void Delete(int id){
            var model = Get(id);
            _connection.Delete(model);  
        } 
    }
}