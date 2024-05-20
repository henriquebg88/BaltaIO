using Dapper.Contrib.Extensions;
using DataAccess_Blog.Models;
using Microsoft.Data.SqlClient;

namespace DataAccess_Blog.Models
{
    [Table("[Role]")]
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}