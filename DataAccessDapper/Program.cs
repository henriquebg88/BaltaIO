using Microsoft.Data.SqlClient; //$ dotnet add package Microsoft.Data.SqlClient
using Dapper;
using DataAccessDapper.Models;
using System.Data; //$ dotnet add package Dapper


// See https://aka.ms/new-console-template for more information
const string stringConnection = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$; TrustServerCertificate=True";

using (var connection = new SqlConnection(stringConnection))
{
    //UpdateCategoryTitleById(connection);
    //CreateCategories(connection);
    //ListCategories(connection);
    //CreateCategory(connection);
    //ExecuteProcedure(connection);
    //ExecuteReadProcedure(connection);
    //ExecuteScalar(connection);
    //OneToOne(connection);
    //OneToMany(connection);
    //ManyToMany(connection);
    //SelectIn(connection);
    //Like(connection);
    Transaction(connection);
}

static void UpdateCategoryTitleById(SqlConnection connection){
    var updateSql = @"UPDATE [CATEGORY]
                        SET [Title] = @title
                        WHERE [Id]  = @id";

    var linhasAfetadas = connection.Execute(updateSql, new {
        id = new Guid("b4c5af73-7e02-4ff7-951c-f69ee1729cac"),
        title = "Titulo Atualizado"
    });

    Console.WriteLine($"{linhasAfetadas} registros atualizadas");
}
static void ListCategories(SqlConnection connection){
    var categories = connection.Query<Category>("SELECT [Id], [Title] FROM Category");

    foreach (var item in categories)
    {
        Console.WriteLine($"{item.id} - {item.Title}");
    }
}
static void CreateCategory(SqlConnection connection){
    
    var category = new Category{
        id = Guid.NewGuid(),
        Title = "Titulo Teste",
        description = "Isto é apenas um teste",
        order = 1,
        featured = true,
        summary = "Teste",
        url = "www.teste.com"
    };

    /// Importante nunca concatenar os parametros com string para evitar SQL Injection.
    var insertSql = @"INSERT INTO [CATEGORY] 
                        VALUES(
                            @id, 
                            @title, 
                            @url, 
                            @summary, 
                            @order, 
                            @description, 
                            @featured
                        )";


        /// Dessa forma pode-se impedir SQL Injection
        var linhasAfetadas = connection.Execute(insertSql, new {
            id = category.id, 
            title = category.Title, 
            url = category.url, 
            summary = category.summary, 
            order = category.order, 
            description = category.description, 
            featured = category.featured
        });

        Console.WriteLine($"{linhasAfetadas} linhas inseridas");
}
static void CreateCategories(SqlConnection connection){
    var category = new Category{
        id = Guid.NewGuid(),
        Title = "Titulo Teste",
        description = "Isto é apenas um teste",
        order = 1,
        featured = true,
        summary = "Teste",
        url = "www.teste.com"
    };

    var category2 = new Category{
        id = Guid.NewGuid(),
        Title = "Titulo novo",
        description = "Isto é apenas um novo",
        order = 2,
        featured = false,
        summary = "novo",
        url = "www.novo.com"
    };


    /// Importante nunca concatenar os parametros com string para evitar SQL Injection.
    var insertSql = @"INSERT INTO [CATEGORY] 
                        VALUES(
                            @id, 
                            @title, 
                            @url, 
                            @summary, 
                            @order, 
                            @description, 
                            @featured
                        )";


        /// Dessa forma pode-se impedir SQL Injection
        var linhasAfetadas = connection.Execute(insertSql, new[] {
            new{
                id = category.id, 
                title = category.Title, 
                url = category.url, 
                summary = category.summary, 
                order = category.order, 
                description = category.description, 
                featured = category.featured
            },
            new{
                id = category2.id, 
                title = category2.Title, 
                url = category2.url, 
                summary = category2.summary, 
                order = category2.order, 
                description = category2.description, 
                featured = category2.featured
            }
        });

        Console.WriteLine($"{linhasAfetadas} linhas inseridas");
}
static void ExecuteProcedure(SqlConnection connection){
    var sql = "[spDeleteStudent]";
    var parametros = new {StudentId = "a379a9b0-4f8a-4b25-80c4-b3beb14f45aa"};

    var linhasAfetadas = connection.Execute(sql, parametros, commandType: CommandType.StoredProcedure);
    Console.WriteLine($"{linhasAfetadas} linhas removidas");
}
static void ExecuteReadProcedure(SqlConnection connection){
    var sql = "[spGetCoursesByCategory]";
    var parametros = new {CategoryId = "af3407aa-11ae-4621-a2ef-2028b85507c4"};

    var courses = connection.Query(sql, parametros, commandType: CommandType.StoredProcedure);
    
    foreach (var course in courses)
    {
        Console.WriteLine($"{course.Title}");
    }     
}
static void ExecuteScalar(SqlConnection connection){
    var category = new Category{
        Title = "Titulo Teste",
        description = "Isto é apenas um teste",
        order = 1,
        featured = true,
        summary = "Teste",
        url = "www.teste.com"
    };

    /// Importante nunca concatenar os parametros com string para evitar SQL Injection.
    var insertSql = @"INSERT INTO [CATEGORY] 
                        OUTPUT INSERTED.[Id]
                        VALUES(
                            NEWID(), 
                            @title, 
                            @url, 
                            @summary, 
                            @order, 
                            @description, 
                            @featured
                        )";


        /// Dessa forma pode-se impedir SQL Injection
        var ID = connection.ExecuteScalar<Guid>(insertSql, new {
            title = category.Title, 
            url = category.url, 
            summary = category.summary, 
            order = category.order, 
            description = category.description, 
            featured = category.featured
        });

        Console.WriteLine($"ID: {ID}");
}
static void OneToOne(SqlConnection connection){
    var sql = @"
        SELECT * FROM [CareerItem]
        INNER JOIN [Course] ON [CareerItem].[CourseId] = [Course].Id
    ";

    //var items = connection.Query<CareerItem>(sql); -> Dessa forma não vai popular os dados da view que não pertencem a esta tabela/classe
    var items = connection.Query<CareerItem, Course, CareerItem>( // Item 1, Item 2, Item retornado
        sql, 
        (carrerItem, course) => {

            carrerItem.Course = course;
            return carrerItem;

        }, splitOn: "Id" // Define qual coluna do resultado do select na view (que tras 2 tabelas) vai dividir, send o primeiro item da nova coluna
                           // Id neste caso, desde que não esteja repetido na query. Se não, deve-se usar alias
    );
    
    foreach (CareerItem item in items)
    {
        Console.WriteLine($"Item: {item.Title} -  Título: {item.Course.Title}");
    }
}
static void OneToMany(SqlConnection connection){
    var sql = @"
        SELECT 
            [Career].[Id],
            [Career].[Title],
            [CareerItem].[CareerId] AS Id,
            [CareerItem].[Title]
        FROM 
            [Career] 
        INNER JOIN 
            [CareerItem] ON [CareerItem].[CareerId] = [Career].[Id]
        ORDER BY
            [Career].[Title]
    ";

    var careers = new List<Career>();
    var items = connection.Query<Career, CareerItem, Career>( // Item 1, Item 2, Item retornado
        sql, 
        (career, item) => {
            
            var careerSearch = careers.Where(c => c.Id == career.Id).FirstOrDefault();

            if(careerSearch == null){
                career.Items.Add(item);
                careers.Add(career);
                careerSearch = career;
            }else{
                careerSearch.Items.Add(item);
            }

            return career;

        }, splitOn: "Id" // Define qual coluna do resultado do select na view (que tras 2 tabelas) vai dividir, send o primeiro item da nova coluna
                           // Id neste caso, desde que não esteja repetido na query. Se não, deve-se usar alias
    );
    
    foreach (var career in careers)
    {
        Console.WriteLine($"Carreira: {career.Title}");
        foreach (var item in career.Items)
        {
            Console.WriteLine($"    - {item.Title}");
        }
    }
}
static void ManyToMany(SqlConnection connection){
    var query = @"
        SELECT * FROM [Category];
        SELECT * FROM [Course]
    ";
    //Forma mais otimizada de fazer multiplas leituras
    using (var multi = connection.QueryMultiple(query))
    {
        var categories = multi.Read<Category>();
        var courses = multi.Read<Course>();   

        foreach (var item in categories)
        {
            Console.WriteLine($"{item.Title}");
        }
        foreach (var item in courses)
        {
            Console.WriteLine($"{item.Title}");
        }
    }
}
static void SelectIn(SqlConnection connection){
    var query = @"
        SELECT * FROM [Category] WHERE Id IN @Id
    ";

    var categories = connection.Query<Career>(query,new {
        Id = new[] {
        "af3407aa-11ae-4621-a2ef-2028b85507c4",
        "09ce0b7b-cfca-497b-92c0-3290ad9d5142"
    }
    } );

    foreach (var item in categories)
    {
        Console.WriteLine($"{item.Title}");
    }
}
static void Like(SqlConnection connection){
    var query = @"
        SELECT * FROM [Course] WHERE Title LIKE @exp
    ";

    var courses = connection.Query<Course>(query,new {
        exp = "%backend%"
    } );

    foreach (var item in courses)
    {
        Console.WriteLine($"{item.Title}");
    }
}
static void Transaction(SqlConnection connection){
    
    var category = new Category{
        id = Guid.NewGuid(),
        Title = "Titulo Teste",
        description = "Isto é apenas um teste",
        order = 1,
        featured = true,
        summary = "Teste",
        url = "www.teste.com"
    };

    /// Importante nunca concatenar os parametros com string para evitar SQL Injection.
    var insertSql = @"INSERT INTO [CATEGORY] 
                        VALUES(
                            @id, 
                            @title, 
                            @url, 
                            @summary, 
                            @order, 
                            @description, 
                            @featured
                        )";

    connection.Open();
    using (var transaction = connection.BeginTransaction())
    {
        /// Dessa forma pode-se impedir SQL Injection
        var linhasAfetadas = connection.Execute(insertSql, new {
            id = category.id, 
            title = category.Title, 
            url = category.url, 
            summary = category.summary, 
            order = category.order, 
            description = category.description, 
            featured = category.featured
        }, transaction);

        //transaction.Commit();
        transaction.Rollback();
        Console.WriteLine($"{linhasAfetadas} linhas inseridas");
    }

}
