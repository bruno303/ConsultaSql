using ConsultaSql.Classes;
using System;
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
        /// Variável do tipo ConsultaController, responsável por realizar a consulta assíncrona.
        /// </summary>
        private SqlClass executaSql;
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
            executaSql = new SqlClass();
            executaSql.EventoAntesExecucao += EventoAntesExecucao;
            executaSql.EventoAposExecucao += EventoAposExecucao;
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
            SqlClass consulta = new SqlClass()
            {
                QueryText = "SELECT NAME FROM SYS.DATABASES"
            };
            consulta.EventoAposExecucao += (object sender, ExecucaoSqlEventArgs e) =>
            {
                Invoke((MethodInvoker)delegate
                {
                    if (e.HasErrors)
                    {
                        ExibirMensagem("Erro ao consultar databases do servidor. Mensagem: " + e.SqlException.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (e.ReturnedData.HasRows())
                    {
                        foreach (DataRow linha in e.ReturnedData.Rows)
                        {
                            cbxDatabase.Items.Add(linha[0].ToString());
                        }
                        if (cbxDatabase.Items.Count > 0)
                        {
                            cbxDatabase.SelectedIndex = 0;
                        }
                    }
                });
            };
            consulta.ExecutarAssincrona();
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
                ArquivoClass arquivo = new ArquivoClass(ofdArquivo.FileName);
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
            Executar();
        }

        /// <summary>
        /// Realiza a consulta caso não tenha outra consulta sendo executada no momento.
        /// </summary>
        private void Executar()
        {
            executaSql.QueryText = txbQuery.Text;
            executaSql.DatabaseName = cbxDatabase.Text;
            executaSql.ExecutarAssincrona();
        }

        /// <summary>
        /// Preparação da tela para que a consulta seja executada.
        /// </summary>
        /// <param name="sender">Objeto que fez a chamada do evento.</param>
        /// <param name="e">Parâmetros do evento ExecucaoSql.</param>
        private void EventoAntesExecucao(object sender, ExecucaoSqlEventArgs e)
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
                cbxDatabase.Enabled = false;
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
        /// <param name="sender">Objeto que fez a chamada do evento.</param>
        /// <param name="e">Parâmetros do evento ExecucaoSql.</param>
        private void EventoAposExecucao(object sender, ExecucaoSqlEventArgs e)
        {
            dadosGrid = e.ReturnedData;
            Invoke((MethodInvoker)delegate
            {
                btnAbrirArquivo.Enabled = true;
                btnExecutar.Enabled = true;
                cbxDatabase.Enabled = true;
                if (string.IsNullOrEmpty(txbQuery.Text))
                {
                    lblStatus.Text = "Aguardando...";
                    lblQtdRegistros.Text = "0 registro(s).";
                    ZerarTempoExecucao();
                    ColocarFoco(txbQuery);
                    ExibirMensagem("É necessário definir a consulta!");
                }
                else if ((!e.HasErrors))
                {
                    dgvDados.DataSource = dadosGrid;
                    try
                    {
                        dgvDados.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
                        lblQtdRegistros.Text = string.Format("{0} registro(s).", (dadosGrid.HasRows() ? dadosGrid.Rows.Count : 0));
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

                if (e.HasErrors)
                {
                    lblStatus.Text = "Apresentou erros...";
                    lblQtdRegistros.Text = "0 registro(s).";
                    ExibirMensagem("Ocorreu o seguinte erro durante a execução: " + e.SqlException.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                Executar();
            }
            else if (e.KeyCode == Keys.Tab && txbQuery.Focused)
            {
                txbQuery.Text += "    ";
                e.Handled = true;
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

        private void frmConsultaSql_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason != CloseReason.WindowsShutDown) && (executaSql.EmExecucao()))
            {
                e.Cancel = true;
            }
        }
        #endregion
    }
}
