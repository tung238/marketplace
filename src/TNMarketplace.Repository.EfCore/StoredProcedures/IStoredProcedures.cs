using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Repository.EfCore.StoredProcedures
{
    public interface IStoredProcedures
    {
        int UpdateCategoryItemsCount(int categoryID);
    }
}
