using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Repository.EfCore.StoredProcedures;

namespace TNMarketplace.Service
{
    public class SqlDbService
    {
        private readonly IStoredProcedures _storedProcedures;

        public SqlDbService(IStoredProcedures storedProcedures)
        {
            _storedProcedures = storedProcedures;
        }

        public int UpdateCategoryItemCount(int categoryID)
        {
            return _storedProcedures.UpdateCategoryItemsCount(categoryID);
        }
    }
}
