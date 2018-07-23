using System.Data;

namespace ConsultaSql.Classes
{
    public static class ExtensaoClass
    {
        #region Extensão de DataTable
        public static bool HasRows (this DataTable dataTable)
        {
            return dataTable?.Rows?.Count > 0;
        }
        #endregion
    }
}
