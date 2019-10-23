﻿using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Npgsql;

namespace TreeListApp
{
    public static class ConnectionProvider
    {
        private static IDbConnection _dbConnection;

        /// <summary>
        ///     Provides default database connection
        /// </summary>
        /// <returns><see cref="IDbConnection" /> instance</returns>
        public static IDbConnection GetDefaultDbConnection()
        {
            return _dbConnection ?? (_dbConnection =
                       new NpgsqlConnection(
                           ConfigurationManager.ConnectionStrings["PostgresTestExercise"].ConnectionString));
        }

        public static void ReleaseConnection()
        {
            _dbConnection.Close();
            _dbConnection = null;
        }
    }
}
