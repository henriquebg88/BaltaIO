
using Dapper.Contrib.Extensions;

namespace DataAccess_Blog.Models
{
    [Table("[User]")] //O Dapper tenta pluralizar o nome da classe e busca USERS no banco de dados.
    public class User
    {
        public User() => Roles = new List<Role>();
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        
        [Write(false)] //DESABILITA no CREATE
        public List<Role> Roles { get; set; }

    }
}