using System.Data;
using MySql.Data.MySqlClient;

namespace nhlstendencafe
{
    public class DbUtils
    {
        public DbUtils()
        {
        }
        
        public IDbConnection GetDbConnection()
        {
            string connectionString = Program.Configuration.GetConnectionString("nhlstendencafe.MySQL")!;

            return new MySqlConnection(connectionString);
        }
    }
}
