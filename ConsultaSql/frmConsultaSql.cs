using ConsultaSql.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
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
        /// Variável do tipo ConsultaController, responsável por realizar a consulta assíncrona.
        /// </summary>
        ConsultaController consultaController;
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
            consultaController = new ConsultaController();
            consultaController.EventoAntesConsulta += EventoAntesConsulta;
            consultaController.EventoAposConsulta += EventoAposConsulta;
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
        /// Carrega as databases disponíveis no banco de dados.
        /// </summary>
        private void CarregarDatabases()
        {
            ConsultaController consulta = new ConsultaController()
            {
                QueryText = "SELECT NAME FROM SYS.DATABASES"
            };
            consulta.EventoAposConsulta += OnAposConsultaDatabases;
            consulta.ExecutarConsulta();
        }

        /// <summary>
        /// Evento a ser executado após a consulta de Databases. Aqui será preenchido o cbxDatabases com o retorno da consulta.
        /// </summary>
        /// <param name="houveErro">Booleano indicando se houve erro durante a consulta.</param>
        /// <param name="exception">Nulo caso não haja erro. Se houve erro, essa será a exception capturada.</param>
        /// <param name="retorno">DataTable com os dados retornados pela consulta.</param>
        private void OnAposConsultaDatabases(bool houveErro, Exception exception, DataTable retorno)
        {
            Invoke((MethodInvoker) delegate
            {
                if (houveErro)
                {
                    ExibirMensagem("Erro ao consultar databases do servidor. Mensagem: "  + exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    foreach (DataRow linha in retorno.Rows)
                    {
                        cbxDatabase.Items.Add(linha[0].ToString());
                    }
                    if (cbxDatabase.Items.Count > 0)
                    {
                        cbxDatabase.SelectedIndex = 0;
                    }
                }
            });
        }

        /// <summary>
        /// Abre a janela de seleção de arquivos para o usuário.
        /// Caso seja selecionado um arquivo, carrega seu conteúdo para o editor de texto.
        /// </summary>
        /// <param name="sender">Objeto que disparou o evento.</param>
        /// <param name="e">Argumentos do evento.</param>
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
        /// Faz a chamada para uma nova consulta.
        /// </summary>
        /// <param name="sender">Objeto que invocou o evento.</param>
        /// <param name="e">Argumentos do evento.</param>
        private void btnExecutar_Click(object sender, EventArgs e)
        {
            ExecutarConsulta();
        }

        /// <summary>
        /// Realiza a consulta caso não tenha outra consulta sendo executada no momento.
        /// </summary>
        private void ExecutarConsulta()
        {
            consultaController.QueryText = txbQuery.Text;
            consultaController.DatabaseName = cbxDatabase.Text;
            consultaController.ExecutarConsulta();
        }

        /// <summary>
        /// Preparação da tela para que a consulta seja executada.
        /// </summary>
        private void EventoAntesConsulta()
        {
            Invoke((MethodInvoker)delegate
            {
                if (dadosGrid != null)
                {
                    dadosGrid.Clear();
                    dadosGrid.Columns.Clear();
                    dadosGrid = null;
                }
                dgvDados.DataSource = null;
                ZerarTempoExecucao();
                tmTempoExecucao.Start();
                lblStatus.Text = "Executando...";
                btnExecutar.Enabled = false;
                btnAbrirArquivo.Enabled = false;
            });
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
        /// <param name="houveErro">Booleano indicando se houve erro durante a consulta.</param>
        /// <param name="exception">Caso tenha acontecido algum erro, esse parâmetro conterá a exception gerada. Caso contrário, nulo.</param>
        /// <param name="dados">DataTable com os dados retornados da consulta.</param>
        private void EventoAposConsulta(bool houveErro, Exception exception, DataTable dados)
        {
            dadosGrid = dados;
            Invoke((MethodInvoker)delegate
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
                else if ((!houveErro))
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
                    ExibirMensagem("Ocorreu o seguinte erro durante a execução: " + exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
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

        /// <summary>
        /// Força a finalização do processo e alguma thread que esteja rodando.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmConsultaSql_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion
    }
}
