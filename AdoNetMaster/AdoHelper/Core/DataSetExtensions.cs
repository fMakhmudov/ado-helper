using System.Data;

namespace AdoNetMaster.Infrastructure.AdoHelper.Core
{
    public static class DataSetExtensions
    {
        public static bool HasTables(this DataSet ds)
        {
            if (ds != null && ds.Tables != null)
            {
                return ds.Tables.Count > 0;
            }

            return false;
        }
    }
}
