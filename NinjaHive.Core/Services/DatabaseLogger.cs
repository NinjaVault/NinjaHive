using System;
using System.Data;
using System.Data.SqlClient;

namespace NinjaHive.Core.Services
{
    public class DatabaseLogger : ILogger
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly ITimeProvider timeProvider;

        public DatabaseLogger(IConnectionFactory connectionFactory, ITimeProvider timeProvider)
        {
            this.connectionFactory = connectionFactory;
            this.timeProvider = timeProvider;
        }

        public void Log(Exception exception)
        {
            using (var connection = this.connectionFactory.CreateAndOpenConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"
                        INSERT INTO [dbo].[Errors] (Id, CreatedOn, CreatedBy, [Message])
                        VALUES (newid(), @CREATEDON, @CREATEDBY, @MESSAGE)";

                    command.Parameters.Add(new SqlParameter("CREATEDON", this.timeProvider.Now));
                    command.Parameters.Add(new SqlParameter("CREATEDBY", "system"));
                    command.Parameters.Add(new SqlParameter("MESSAGE", exception.ToString()));

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
