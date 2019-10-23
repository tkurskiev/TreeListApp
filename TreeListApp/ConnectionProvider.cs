using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Npgsql;

namespace TreeListApp
{
    // TODO: connection CLOSING???
    public static class ConnectionProvider
    {
        private static IDbConnection _dbConnection;

        /// <summary>
        /// Provides default database connection
        /// </summary>
        /// <returns><see cref="IDbConnection"/> instance</returns>
        public static IDbConnection GetDefaultDbConnection()
        {
            return _dbConnection ?? (_dbConnection =
                       new NpgsqlConnection(
                           ConfigurationManager.ConnectionStrings["PostgresTestExercise"].ConnectionString));
        }
    }
}
