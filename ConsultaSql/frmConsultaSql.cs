using ConsultaSql.Classes;
using ConsultaSql.Controllers;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace ConsultaSql
{
    public partial class frmConsultaSql : Form
    {
        #region Componentes Auxiliares e Variáveis
        /// <summary>
        /// DataSource utilizado pelo dgvDados para exibir as informações consultadas.
        /// </summary>
        private DataTable dadosGrid;

        /// <summary>
        /// TimeSpan usado para controle do tempo de execução das consultas.
        /// </summary>
        private TimeSpan tempoExecucao;

        /// <summary>
        /// Variável para controle de erros em outras threads.
        /// </summary>
        private bool houveErro = false;

        /// <summary>
        /// Armazena última exception na thread de consulta.
        /// </summary>
        private Exception ultException;
        #endregion

        #region Métodos
        /// <summary>
        /// Método construtor. Inicia uma nova instância de frmConsultaSql
        /// </summary>
        public frmConsultaSql()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        /// <summary>
        /// Evento Load do frmConsultaSql. Prepara o form para ser exibido.
        /// </summary>
        /// <param name="sender">Objeto que invocou o evento.</param>
        /// <param name="e">Argumentos do evento disparado.</param>
        private void frmConsultaSql_Load(object sender, EventArgs e)
        {
            CarregarDatabases();
            tempoExecucao = new TimeSpan(0, 0, 0, 0, 0);
        }

        /// <summary>
        /// Incrementa 1 segundo ao TimeSpan tempoExecucao e atualiza a tela com o novo tempo de execução.
        /// </summary>
        /// <param name="sender">Objeto que invocou o evento.</param>
        /// <param name="e">Argumentos do evento.</param>
        private void AtualizarTempoExecucao(object sender, EventArgs e)
        {
            tempoExecucao = tempoExecucao.Add(new TimeSpan(0, 0, 1));
            lblTempoExecucao.Text = tempoExecucao.ToString(@"hh\:mm\:ss");
        }

        /// <summary>
        /// Para o contador do tempo de execução da consulta e limpa o TimeSpan usado para controle.
        /// </summary>
        private void ZerarTempoExecucao()
        {
            if (tmTempoExecucao.Enabled)
            {
                tmTempoExecucao.Stop();
            }
            tempoExecucao = new TimeSpan(0, 0, 0);
            lblTempoExecucao.Text = tempoExecucao.ToString("hh\\:mm\\:ss");
        }

        /// <summary>
        /// Carrega as databases disponíveis no banco de dados e adiciona ao ComboBox cbxDatabase.
        /// </summary>
        private void CarregarDatabases()
        {
            try
            {
                UtilController util = new UtilController();
                util.CarregarDatabases(cbxDatabase);
            }
            catch (Exception ex)
            {
                ExibirMensagem("Erro ao consultar as Databases do servidor. Erro: " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Abre a janela de seleção de arquivos para o usuário.
        /// Caso seja selecionado um arquivo, carrega seu conteúdo para o editor de texto.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbrirArquivo_Click(object sender, EventArgs e)
        {
            ofdArquivo.ShowDialog();
            if (!string.IsNullOrEmpty(ofdArquivo.FileName))
            {
                ArquivoController arquivo = new ArquivoController(ofdArquivo.FileName);
                txbQuery.Text = arquivo.LerArquivoInteiro();
            }
        }

        /// <summary>
        /// Faz a chamada para uma nova consulta ao BackgroundWorker.
        /// </summary>
        /// <param name="sender">Objeto que invocou o evento.</param>
        /// <param name="e">Argumentos do evento.</param>
        private void btnExecutar_Click(object sender, EventArgs e)
        {
            if (!worker.IsBusy)
            {
                ExecutarConsulta();
            }
        }

        /// <summary>
        /// Realiza ajustes de tela e inicia a consulta em segundo plano.
        /// </summary>
        private void ExecutarConsulta()
        {
            if (!worker.IsBusy)
            {
                if (dadosGrid != null)
                {
                    dadosGrid.Clear();
                    dadosGrid.Columns.Clear();
                }
                dgvDados.DataSource = null;
                ZerarTempoExecucao();
                tmTempoExecucao.Start();
                lblStatus.Text = "Executando...";
                btnExecutar.Enabled = false;
                btnAbrirArquivo.Enabled = false;

                object[] parametros = new object[2];
                parametros[0] = txbQuery.Text;
                parametros[1] = cbxDatabase.Text;
                worker.RunWorkerAsync(parametros);
            }
        }

        /// <summary>
        /// Coloca o foco no Control informado, se possível.
        /// </summary>
        /// <param name="control">Control que receberá o foco da aplicação.</param>
        private void ColocarFoco(Control control)
        {
            if (control.CanFocus)
            {
                control.Focus();
            }
        }

        /// <summary>
        /// Exibe uma mensagem ao usuário da aplicação.
        /// </summary>
        /// <param name="message">Texto da mensagem.</param>
        /// <param name="buttons">Botões que aparecerão na nova janela de mensagem.</param>
        /// <param name="icon">Icone que aparecerá na nova janela de mensagem.</param>
        private void ExibirMensagem(string message, MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.Warning)
        {
            MessageBox.Show(message, Text, buttons, icon);
        }

        /// <summary>
        /// Executa os acertos finais após a execução de uma consulta.
        /// </summary>
        /// <param name="sender">Objeto que invocou o evento.</param>
        /// <param name="e">Argumentos do evento.</param>
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnAbrirArquivo.Enabled = true;
            btnExecutar.Enabled = true;
            if (string.IsNullOrEmpty(txbQuery.Text))
            {
                lblStatus.Text = "Aguardando...";
                lblQtdRegistros.Text = "0 registro(s).";
                ZerarTempoExecucao();
                ColocarFoco(txbQuery);
                ExibirMensagem("É necessário definir a consulta!");
            }
            else if ((!worker.CancellationPending) && (!houveErro))
            {
                dgvDados.DataSource = dadosGrid;
                try
                {
                    dgvDados.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
                    lblQtdRegistros.Text = string.Format("{0} registro(s).", (dadosGrid.Rows != null ? dadosGrid.Rows.Count : 0));
                    lblStatus.Text = "Concluído...";
                    if (spcQueryDados.Panel2Collapsed)
                    {
                        spcQueryDados.Panel2Collapsed = false;
                    }
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Apresentou erros...";
                    lblQtdRegistros.Text = "0 registro(s).";
                    ExibirMensagem("Ocorreu o seguinte erro durante a execução: " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            tmTempoExecucao.Stop();

            if (houveErro)
            {
                houveErro = false;
                lblStatus.Text = "Apresentou erros...";
                lblQtdRegistros.Text = "0 registro(s).";
                ExibirMensagem("Ocorreu o seguinte erro durante a execução: " + ultException.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Grava as informações de erro para serem usadas na thread principal.
        /// </summary>
        /// <param name="ex"></param>
        private void TratarErro(Exception ex)
        {
            houveErro = true;
            ultException = ex;
        }

        /// <summary>
        /// Executa a consulta de forma assíncrona.
        /// </summary>
        /// <param name="sender">Objeto que invocou o evento.</param>
        /// <param name="e">Argumentos do evento.</param>
        private void worker_DoWorkAsync(object sender, DoWorkEventArgs e)
        {
            try
            {
                object[] parametros = (object[])e.Argument;
                ConexaoClass conexao = new ConexaoClass();
                if (!string.IsNullOrEmpty(parametros[0].ToString()))
                {
                    string queryPreparada = string.Format("USE {0}; {1}", parametros[1].ToString(), parametros[0].ToString());
                    dadosGrid = conexao.RetornarDados(queryPreparada);
                }
            }
            catch (Exception ex)
            {
                TratarErro(ex);
            }
        }

        /// <summary>
        /// Evento para tratamento de teclas pressionadas na aplicação.
        /// </summary>
        /// <param name="sender">Objeto que invocou o evento.</param>
        /// <param name="e">Argumentos do evento.</param>
        private void EventoKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.R))
            {
                spcQueryDados.Panel2Collapsed = !spcQueryDados.Panel2Collapsed;
            }
            else if (e.KeyCode == Keys.F5)
            {
                ExecutarConsulta();
            }
        }
        #endregion
    }
}
