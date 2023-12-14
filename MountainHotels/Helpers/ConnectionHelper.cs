using Npgsql;

namespace MountainHotels.Helpers
{
    public static class ConnectionHelper
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            return string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(configuration, databaseUrl);
        }

        // Build the connection string from the environment, e.g., Heroku
        private static string BuildConnectionString(IConfiguration configuration, string databaseUrl)
        {
            if (string.IsNullOrEmpty(databaseUrl))
            {
                return configuration.GetConnectionString("DefaultConnection");
            }

            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.AbsolutePath.TrimStart('/'),
                TrustServerCertificate = true // Assuming you want to trust the server certificate
            };
            return builder.ToString();
        }
    }
}
