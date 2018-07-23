using System;
using System.Data;

namespace ConsultaSql.Classes
{
    public class ExecucaoSqlEventArgs : EventArgs
    {
        #region Propriedades
        public bool HasErrors { get; }
        public Exception SqlException  { get; }
        public DataTable ReturnedData { get; set; }
        #endregion

        #region Construtor
        public ExecucaoSqlEventArgs(bool hasErrors = false, Exception exception = null, DataTable data = null)
        {
            HasErrors = hasErrors;
            SqlException = exception;
            ReturnedData = data;
        }
        #endregion
    }
}
