using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Repository.EfCore.StoredProcedures
{
    public partial class ApplicationDbContext: IStoredProcedures
    {
        public int UpdateCategoryItemsCount(int categoryID)
        {
            return 0;
            //return Database.ExecuteSqlCommand("UPDATE CategoryStats SET COUNT = COUNT + 1 WHERE CategoryID = @p0", categoryID);
        }
    }
}
