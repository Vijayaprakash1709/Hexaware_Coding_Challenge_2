using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace LoanManagementSystem.Util
{
    internal class DBConnUtil
    {
        private static IConfiguration _configuration;

        static DBConnUtil()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
                _configuration = builder.Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading configuration: " + ex.Message);
            }
        }

        public static string GetConnectionString()
        {
            return _configuration.GetConnectionString("LocalConnectionString");
        }

        public static SqlConnection GetConnection()
        {
            try
            {
                string connectionString = GetConnectionString();
                return new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting connection: " + ex.Message);
                throw;
            }
        }
    }
}
