using System;
using System.Data;
using System.Data.SqlClient;
using ConsultaSql.Controllers;

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
        /// Realiza uma consulta ao banco de dados.
        /// </summary>
        /// <param name="query">A query select que será executada.</param>
        /// <returns>DataTable com os resultados da query.</returns>
        public DataTable RetornarDados(string query)
        {
            try
            {
                DataTable retorno = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, new SqlConnection(strConexao)))
                {
                    adapter.SelectCommand.Connection.Open();
                    adapter.SelectCommand.CommandTimeout = 600;
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.Fill(retorno);
                }
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Executa uma query no banco de dados que não tenha retorno.
        /// </summary>
        /// <param name="query">Query a ser executada.</param>
        public void ExecutarQuery(string query)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, new SqlConnection(strConexao)))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
