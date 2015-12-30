using System.Data.SqlClient;

namespace NinjaHive.Core
{
    public interface IConnectionFactory
    {
        SqlConnection CreateAndOpenConnection();
    }
}
