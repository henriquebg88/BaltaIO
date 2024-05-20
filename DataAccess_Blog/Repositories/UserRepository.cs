using Dapper;
using DataAccess_Blog.Models;
using Microsoft.Data.SqlClient;

namespace DataAccess_Blog.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        private readonly SqlConnection _connection;

        public UserRepository(SqlConnection connection) : base(connection) => _connection = connection;
        
        public List<User> GetWithRoles(){
            string query = @"
                SELECT 
                    U.* , 
                    R.* 
                from 
                    [User] U
                    left join [UserRole] UR on UR.UserId = U.Id
                    left join [Role] R on R.Id = UR.RoleId
            ";

            var users = new List<User>();

            // < Item 1, Item 2, Item retornado >
            var items = _connection.Query<User, Role, User>( 
                query, 
                (user, role) => {
                    
                    var u = users.FirstOrDefault(c => c.Id == user.Id);

                    if(u == null){
                        if(role != null)
                            user.Roles.Add(role);

                        users.Add(user);
                        u = user;
                    }else{
                        if(role != null)
                            u.Roles.Add(role);
                    }

                    return user;

                }, splitOn: "Id" // Define qual coluna do resultado do select na view (que tras 2 tabelas) vai dividir, send o primeiro item da nova coluna
                                // Id neste caso, desde que não esteja repetido na query. Se não, deve-se usar alias
            );

            return users;
        }
    }
}