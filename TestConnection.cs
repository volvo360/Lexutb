using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace EfBloggy
{
    public static class ExtensionMethods
    {
        //https://stackoverflow.com/a/49286320 2022-01-22

        public static bool TestConnection(this DbContext context)
        {
            DbConnection conn = context.Database.GetDbConnection();

            try
            {
                conn.Open();   // Check the database connection

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}