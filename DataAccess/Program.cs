using Microsoft.Data.SqlClient; //dotnet add package Microsoft.Data.SqlClient

// See https://aka.ms/new-console-template for more information
const string stringConnection = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$; TrustServerCertificate=True";

/// Metodo 1
var connection = new SqlConnection(stringConnection);

connection.Open();

connection.Close();
connection.Dispose(); // Destruir a conexão (e fechar). Elimita o objeto connection. Se for abrir a conexão, precisa criar outro objeto com new.

///Método 2
using (var connection2 = new SqlConnection(stringConnection))
{
    connection2.Open();

    using (var command = new SqlCommand())
    {
        command.Connection = connection2;
        command.CommandType = System.Data.CommandType.Text;
        command.CommandText = "SELECT [Id], [Title] FROM [Category]";

        SqlDataReader reader = command.ExecuteReader(); 
                    //command.ExecuteNonQuery() 
                    //new SqlDataReader(); // Forma mais rapida de consulta direta ao banco (Usado pelo Entity e outros frameworks)

        while (reader.Read())
        {
            Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)}");
        }
    }
}