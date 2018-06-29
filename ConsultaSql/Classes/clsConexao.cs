using System.Data;
using System.Data.SqlClient;

namespace ConsultaSql.Classes
{
    internal class clsConexao
    {
        private const string strConexao = "";

        public DataTable RetornarDados(string query)
        {
            DataTable retorno = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, new SqlConnection(strConexao)))
            {
                adapter.SelectCommand.Connection.Open();
                adapter.SelectCommand.CommandType = CommandType.Text;
                adapter.Fill(retorno);
            }
            return retorno;
        }

        public void ExecutarQuery(string query)
        {
            using (SqlCommand cmd = new SqlCommand(query, new SqlConnection(strConexao)))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
