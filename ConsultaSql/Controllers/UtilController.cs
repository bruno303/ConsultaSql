using ConsultaSql.Classes;
using System.Data;
using System.Windows.Forms;

namespace ConsultaSql.Controllers
{
    internal class UtilController
    {
        #region Métodos
        /// <summary>
        /// Monta uma string de conexão para SQL Server
        /// </summary>
        /// <param name="servidor">Servidor para conexão</param>
        /// <param name="database">Database para conexão</param>
        /// <param name="usuario">Usuário que será utilizado na conexão</param>
        /// <param name="senha">Senha do usuário informado</param>
        /// <returns></returns>
        public string MontarStringConexao(string servidor, string database, string usuario, string senha)
        {
            return string.Format("Server={0};Database={1};User Id={2};Password={3};", servidor, database, usuario, senha);
        }

        /// <summary>
        /// Preenche o 'combo' com as databases disponíveis no banco de dados.
        /// </summary>
        /// <param name="combo">ComboBox que será preenchido.</param>
        public void CarregarDatabases(ComboBox combo)
        {
            try
            {
                DataTable databases = new ConexaoClass().RetornarDados("SELECT NAME FROM MASTER.SYS.DATABASES WITH(NOLOCK)");
                foreach (DataRow row in databases.Rows)
                {
                    combo.Items.Add(row[0].ToString());
                }
                combo.SelectedIndex = 0;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
