using Dapper.Contrib.Extensions;
using DataAccess_Blog.Models;
using DataAccess_Blog.Repositories;
using Microsoft.Data.SqlClient;

internal class Program
{
    private const string CONNECTION_STRING = @"Server=localhost,1433; Database=Blog; User ID=sa; Password=1q2w3e4r@#$; TrustServerCertificate=True";
    private static void Main(string[] args)
    {
        Console.WriteLine("\n");
        var connection = new SqlConnection(CONNECTION_STRING);
        connection.Open();

        ReadUsersWithRoles(connection);
        //ReadRoles(connection);

        //var UserGenericRepository = new GenericRepository<User>(connection);
        //UserGenericRepository.GetAll();        
        //ReadUser(1);

        //CreateUser(new User{
        //     Name = "Careca Carlos",
        //     Bio = "Um ex cabeludo",
        //     Email = "Cabelo123@gmail.com",
        //     Image = "https://..",
        //     PasswordHash = "HASH",
        //     Slug = "careca"
        // });

        //UpdateUser(new User{
        //     Id = 2,
        //     Name = "Careca Carlos",
        //     Bio = "Um ex cabeludo do metal",
        //     Email = "Cabelo123@gmail.com",
        //     Image = "https://..",
        //     PasswordHash = "HASH",
        //     Slug = "careca"
        // });

        //DeleteUser(ReadUser(2));
        Console.WriteLine("\n");
        connection.Close();
    }

    public static void ReadUsersWithRoles(SqlConnection connection){
        //var repository = new UserRepository(connection);
        var repository = new UserRepository(connection);
        var users = repository.GetWithRoles();

        Console.WriteLine("Usuários:\n");

        if(users != null){
            foreach (User user in users)
            {
                Console.WriteLine($"\t{user.Id} - {user.Name}");
                foreach (var role in user.Roles)
                {
                    Console.WriteLine($"\tRole: {role.Name}");
                }
            }
        }
    }

    public static void ReadRoles(SqlConnection connection){
        //var repository = new RoleRepository(connection);
        var repository = new GenericRepository<Role>(connection);
        var roles = repository.GetAll();

        Console.WriteLine("Roles:\n");

        if(roles != null){
            foreach (var role in roles)
            {
                Console.WriteLine($"\t{role.Id} - {role.Name}");
            }
        }
    }

    public static User ReadUser(int id)
    {
        using (var connection = new SqlConnection(CONNECTION_STRING))
        {
            var user = connection.Get<User>(id);

            Console.WriteLine("Usuário:\n");
            Console.WriteLine($"\t{user.Id} - {user.Name}");
            
            return user;
        }
    }
    public static void CreateUser(User user)
    {
        using (var connection = new SqlConnection(CONNECTION_STRING))
        {
            var lines = connection.Insert<User>(user);

            Console.Write($"Usuário {user.Name} ");

            if(lines == 0) Console.Write($"não foi ");
            
            Console.Write("cadastrado.");
            
        }
    }

    public static void UpdateUser(User user)
    {
        using (var connection = new SqlConnection(CONNECTION_STRING))
        {
            var lines = connection.Update<User>(user);

            Console.Write($"Usuário {user.Name} ");

            if(!lines) Console.Write($"não foi ");
            
            Console.Write("atualizado.");
            
        }
    }

    public static void DeleteUser(User user)
    {
        using (var connection = new SqlConnection(CONNECTION_STRING))
        {
            var lines = connection.Delete<User>(user);

            Console.Write($"Usuário {user.Name} ");

            if(!lines) Console.Write($"não foi ");
            
            Console.Write("removido.");
            
        }
    }
}