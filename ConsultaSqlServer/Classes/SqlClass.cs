using System;
using System.Data;
using System.Threading;

namespace ConsultaSql.Classes
{
    internal class SqlClass
    {
        #region Delegates para eventos
        public delegate void AposExecutaDelegate(object sender, ExecucaoSqlEventArgs e);
        public delegate void AntesExecucaoDelegate(object sender, ExecucaoSqlEventArgs e);
        #endregion

        #region Propriedades
        // Privados
        private DataTable dados;
        private bool houveErro;
        private Exception exception;
        private Thread threadExecucao;

        // Públicos
        public event AposExecutaDelegate EventoAposExecucao;
        public event AntesExecucaoDelegate EventoAntesExecucao;
        public string QueryText { get; set; }
        public string DatabaseName { get; set; } = "MASTER";
        #endregion

        #region Métodos
        /// <summary>
        /// Retorna uma nova instância de ConsultaController.
        /// </summary>
        public SqlClass()
        {
            dados = new DataTable();
        }

        /// <summary>
        /// Inicia a execução da query.
        /// </summary>
        public void ExecutarAssincrona()
        {
            ExecutarAsync();
        }

        /// <summary>
        /// Executa a query síncrona.
        /// </summary>
        private void ExecutarSync()
        {
            ConexaoClass conexao = new ConexaoClass();
            try
            {
                if (!string.IsNullOrEmpty(QueryText))
                {
                    string queryPreparada = string.Format("USE {0}; {1}", DatabaseName, QueryText);
                    OnEventoAntesExecucao();
                    dados = conexao.ExecutarQuery(queryPreparada);
                    OnEventoAposExecucao();
                }
            }
            catch (Exception ex)
            {
                TratarErro(ex);
            }
        }

        /// <summary>
        /// Executar query síncrona.
        /// </summary>
        public void ExecutarSincrona()
        {
            ExecutarSync();
        }

        /// <summary>
        /// Verifica se há uma query em execução.
        /// </summary>
        /// <returns>True caso haja uma query em execução. False caso contrário.</returns>
        public bool EmExecucao()
        {
            return threadExecucao?.ThreadState == ThreadState.Running;
        }

        /// <summary>
        /// Prepara componentes internos para uma nova execução.
        /// </summary>
        private void LimparRetornos()
        {
            houveErro = false;
            exception = null;
            dados = null;
        }

        /// <summary>
        /// Realiza a consulta assíncrona.
        /// </summary>
        private void ExecutarAsync()
        {
            if (!EmExecucao())
            {
                LimparRetornos();
                threadExecucao = new Thread(() =>
                {
                    ConexaoClass conexao = new ConexaoClass();
                    try
                    {
                        if (!string.IsNullOrEmpty(QueryText))
                        {
                            string queryPreparada = (string.IsNullOrEmpty(DatabaseName) ? "" : string.Format("USE {0}; ", DatabaseName));
                            queryPreparada += QueryText;
                            OnEventoAntesExecucao();
                            dados = conexao.ExecutarQuery(queryPreparada);
                            OnEventoAposExecucao();
                        }
                    }
                    catch (Exception ex)
                    {
                        TratarErro(ex);
                    }
                });
                threadExecucao.Start();
            }
        }

        /// <summary>
        /// Trata erros que surgiram durante a execuação da consulta.
        /// </summary>
        /// <param name="ex">Exception para ser tratada.</param>
        private void TratarErro(Exception ex)
        {
            houveErro = true;
            exception = ex;
            OnEventoAposExecucao();
        }

        /// <summary>
        /// Chamada do evento AposConsulta.
        /// </summary>
        private void OnEventoAposExecucao()
        {
            EventoAposExecucao?.Invoke(this, new ExecucaoSqlEventArgs(houveErro, exception, dados));
        }

        /// <summary>
        /// Chamada do evento AntesConsulta.
        /// </summary>
        private void OnEventoAntesExecucao()
        {
            EventoAntesExecucao?.Invoke(this, new ExecucaoSqlEventArgs(houveErro, exception, dados));
        }
        #endregion
    }
}
