using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Npgsql;
using TreeListApp.Exceptions;

namespace TreeListApp.DataService
{
    public class CatalogLevelDataService : ICatalogLevelDataService
    {
        #region Private Fields

        private static readonly IDbConnection Connection = ConnectionProvider.GetDefaultDbConnection();

        private static string _getAllTreeListDataObjectsQuery;

        #endregion

        #region Private Properties

        private static string GetAllTreeListDataObjectsQuery =>
            _getAllTreeListDataObjectsQuery ?? (_getAllTreeListDataObjectsQuery = $@"
SELECT
Id as {nameof(TreeListDto.Id)}
,Parent_Id as {nameof(TreeListDto.ParentId)}
,Name as {nameof(TreeListDto.Name)}
,Description as {nameof(TreeListDto.Description)}
FROM app.catalog_level
");

        #endregion

        #region Public Constructor

        static CatalogLevelDataService()
        {
            if (Connection.State != ConnectionState.Closed)
                return;

            try
            {
                Connection.Open();
            }
            catch (Exception e)
            {
                throw new DbException(e);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads all the data from app.catalog_level table
        /// </summary>
        /// <returns><see cref="IEnumerable{TreeListDto}"/></returns>
        /// <exception cref="T:TreeListApp.Exceptions.DbException"></exception>
        public IEnumerable<TreeListDto> GetAllTreeListDataObjects()
        {
            try
            {
                var result = Connection.Query<TreeListDto>(GetAllTreeListDataObjectsQuery);

                return result;
            }
            catch (NpgsqlException nException)
            {
                throw new DbException(nException);
            }
        }

        #endregion
    }
}
