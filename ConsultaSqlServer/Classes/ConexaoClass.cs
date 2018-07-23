using System;
using System.Data;
using System.Data.SqlClient;

namespace ConsultaSql.Classes
{
    internal class ConexaoClass
    {
        #region Variáveis
        /// <summary>
        /// String de conexão do banco de dados.
        /// </summary>
        private string strConexao = "";
        #endregion

        #region Métodos
        /// <summary>
        /// Método construtor. Cria uma nova instância da classe de conexão.
        /// </summary>
        public ConexaoClass()
        {
            strConexao = "";
        }

        /// <summary>
        /// Executa uma query no banco de dados.
        /// </summary>
        /// <param name="query">A query que será executada.</param>
        /// <returns>DataTable com os resultados da query, se houver.</returns>
        public DataTable ExecutarQuery(string query)
        {
            try
            {
                DataTable retorno = new DataTable();
                using (SqlConnection conexao = new SqlConnection(strConexao))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conexao))
                    {
                        adapter.SelectCommand.Connection.Open();
                        adapter.SelectCommand.CommandTimeout = 600;
                        adapter.SelectCommand.CommandType = CommandType.Text;
                        adapter.Fill(retorno);
                    }
                }
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
