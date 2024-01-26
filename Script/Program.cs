using MobileStore.Infrastructure.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobileStore.Infrastructure.Abstractions.Contexts;
using MobileStore.Infrastructure.Configurations;

class Program
{
    static Random random = new Random();
    static void Main()
    {
        var serviceCollection = new ServiceCollection();

        var builder = new ConfigurationBuilder();
        builder.AddInMemoryCollection(new Dictionary<string, string>
        {
            {"Database:ConnectionString", "Host=localhost;Port=5432;Database=mobilestoredb;Username=postgres;Password=postgres"},
        }!);

        var configuration = builder.Build();

        serviceCollection.RegisterInfrastructureDependencies(configuration);
        var provider = serviceCollection.BuildServiceProvider();

        using var scope = provider.CreateScope();

        using var context = scope.ServiceProvider.GetService<IDefaultContext>()!;

        // Bogus faker

        for (int i = 0; i < 3; i++)
        {
            var order = GenerateRandomOrder();
            context.Orders.Add(order);
        }
        context.SaveChanges();
        Console.WriteLine("Orders were created");

        return;
    }

    static Order GenerateRandomOrder()
    {
        return new Order
        {
            Id = Guid.NewGuid(),
            Datetime = DateTime.UtcNow.AddDays(-random.Next(1, 365)),
            FirstName = GenerateRandomString(8),
            LastName = GenerateRandomString(10),
            Email = GenerateRandomEmail(),
            Address = GenerateRandomString(20),
            ContactPhone = GenerateRandomPhoneNumber(),
            UserId = Guid.Parse("30ccd9d5-9ee6-446f-a36e-d68e51d7678e"),
        };
    }
    static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    static string GenerateRandomPhoneNumber()
    {
        return $"{random.Next(100, 999)}-{random.Next(100, 999)}-{random.Next(1000, 9999)}";
    }

    static string GenerateRandomEmail()
    {
        return $"{GenerateRandomString(8)}@example.com";
    }

    #region AnotherExample
    //static Random random = new Random();

    //static void Main()
    //{
    //string connectionString = "Host=localhost;Port=5432;Database=mobilestoredb;Username=postgres;Password=postgres";


    //using (var connection = new NpgsqlConnection(connectionString))
    //{
    //    connection.Open();

    //    for (int i = 0; i < 100; i++)
    //    {
    //        var order = GenerateRandomOrder();
    //        InsertOrder(connection, order);
    //    }
    //}
    //}

    //static void InsertOrder(NpgsqlConnection connection, Order order)
    //{
    //    using (var cmd = new NpgsqlCommand())
    //    {
    //        cmd.Connection = connection;
    //        cmd.CommandType = CommandType.Text;
    //        cmd.CommandText = "INSERT INTO public.\"Orders\"(\r\n\t\"Id\", \"Datetime\", \"FirstName\", \"LastName\", \"Address\", \"ContactPhone\", \"UserId\", \"Email\")" +
    //                          "VALUES (@Id, @Datetime, @FirstName, @LastName, @Address, @ContactPhone, @UserId, @Email)";

    //        cmd.Parameters.AddWithValue("@Id", order.Id);
    //        cmd.Parameters.AddWithValue("@Datetime", order.Datetime);
    //        cmd.Parameters.AddWithValue("@FirstName", order.FirstName);
    //        cmd.Parameters.AddWithValue("@LastName", order.LastName);
    //        cmd.Parameters.AddWithValue("@Address", order.Address);
    //        cmd.Parameters.AddWithValue("@ContactPhone", order.ContactPhone);
    //        cmd.Parameters.AddWithValue("@UserId", order.UserId);
    //        cmd.Parameters.AddWithValue("@Email", order.Email);

    //        cmd.ExecuteNonQuery();
    //    }
    //}

    // Another code ...
    #endregion
}