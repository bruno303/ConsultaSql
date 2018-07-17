using ConsultaSql.Classes;
using System;
using System.Data;
using System.Threading;

namespace ConsultaSql.Controllers
{
    internal class ConsultaController
    {
        #region Delegates para eventos
        public delegate void AposConsultaDelegate(bool houveErro, Exception exception, DataTable dados);
        public delegate void AntesConsultaDelegate();
        #endregion

        #region Propriedades
        // Privados
        private DataTable dados;
        private bool houveErro;
        private Exception exception;
        private Thread threadExecucao;

        // Públicos
        public event AposConsultaDelegate EventoAposConsulta;
        public event AntesConsultaDelegate EventoAntesConsulta;
        public string QueryText { get; set; }
        public string DatabaseName { get; set; } = "MASTER";
        #endregion

        #region Métodos
        /// <summary>
        /// Retorna uma nova instância de ConsultaController.
        /// </summary>
        public ConsultaController()
        {
            dados = new DataTable();
        }

        /// <summary>
        /// Inicia a execução da consulta.
        /// </summary>
        public void ExecutarConsulta()
        {
            Consultar();
        }

        /// <summary>
        /// Verifica se há uma consulta em execução.
        /// </summary>
        /// <returns>True caso haja uma consulta em execução. False caso contrário.</returns>
        private bool EmExecucao()
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
        /// Realiza a consulta.
        /// </summary>
        private void Consultar()
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
                            string queryPreparada = string.Format("USE {0}; {1}", DatabaseName, QueryText);
                            OnEventoAntesConsulta();
                            dados = conexao.RetornarDados(queryPreparada);
                            OnEventoAposConsulta();
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
            OnEventoAposConsulta();
        }

        /// <summary>
        /// Chamada do evento AposConsulta.
        /// </summary>
        private void OnEventoAposConsulta()
        {
            EventoAposConsulta?.Invoke(houveErro, exception, dados);
        }

        /// <summary>
        /// Chamada do evento AntesConsulta.
        /// </summary>
        private void OnEventoAntesConsulta()
        {
            EventoAntesConsulta?.Invoke();
        }
        #endregion
    }
}
