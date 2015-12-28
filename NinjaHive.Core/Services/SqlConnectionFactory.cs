using System.Data.SqlClient;

namespace NinjaHive.Core.Services
{
    public class SqlConnectionFactory : IConnectionFactory
    {
        private readonly string connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public SqlConnection CreateAndOpenConnection()
        {
            var connection = new SqlConnection(this.connectionString);

            try
            {
                connection.Open();
                return connection;
            }
            catch
            {
                connection.Dispose();
                throw;
            }
        }
    }
}
