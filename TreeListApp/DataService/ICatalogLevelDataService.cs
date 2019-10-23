using System.Collections.Generic;

namespace TreeListApp
{
    public interface ICatalogLevelDataService
    {
        /// <summary>
        /// Loads all the data from app.catalog_level table
        /// </summary>
        /// <returns><see cref="IEnumerable{TreeListDto}"/></returns>
        /// <exception cref="T:TreeListApp.Exceptions.DbException"></exception>
        IEnumerable<TreeListDto> GetAllTreeListDataObjects();
    }
}
