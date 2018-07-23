namespace ConsultaSql.Classes
{
    internal class UtilClass
    {
        #region Métodos
        /// <summary>
        /// Monta uma string de conexão para SQL Server
        /// </summary>
        /// <param name="servidor">Servidor para conexão</param>
        /// <param name="database">Database para conexão</param>
        /// <param name="usuario">Usuário que será utilizado na conexão</param>
        /// <param name="senha">Senha do usuário informado</param>
        /// <returns>Retorna a ConnectionString montada para SQL Server</returns>
        public string MontarStringConexao(string servidor, string database, string usuario, string senha)
        {
            return string.Format("Server={0};Database={1};User Id={2};Password={3};", servidor, database, usuario, senha);
        }
        #endregion
    }
}
