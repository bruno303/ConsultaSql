using ConsultaSql.Classes;
using ConsultaSql.Controllers;
using System;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace ConsultaSql
{
    public partial class frmConsultaSql : Form
    {
        private DataTable dadosGrid;
        private TimeSpan tempoExecucao;

        public frmConsultaSql()
        {
            InitializeComponent();
        }

        private void frmConsultaSql_Load(object sender, EventArgs e)
        {
            CarregarDatabases();
            tempoExecucao = new TimeSpan(0, 0, 0, 0, 0);
        }

        private void AtualizarTempoExecucao(object sender, EventArgs e)
        {
            tempoExecucao = tempoExecucao.Add(new TimeSpan(0, 0, 1));
            lblTempoExecucao.Text = tempoExecucao.ToString(@"hh\:mm\:ss");
        }

        private void CarregarDatabases()
        {
            UtilController util = new UtilController();
            util.CarregarDatabases(cbxDatabase);
        }

        private void btnAbrirArquivo_Click(object sender, EventArgs e)
        {
            ofdArquivo.ShowDialog();
            if (!string.IsNullOrEmpty(ofdArquivo.FileName))
            {
                ArquivoController arquivo = new ArquivoController(ofdArquivo.FileName);
                txbQuery.Text = arquivo.LerArquivoInteiro();
            }
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            ExecutarConsulta();
        }

        private void ExecutarConsulta()
        {
            if (!worker.IsBusy)
            {
                if (dadosGrid != null)
                {
                    dadosGrid.Clear();
                }
                tempoExecucao = new TimeSpan(0, 0, 0);
                tmTempoExecucao.Start();
                object[] parametros = new object[2];
                parametros[0] = txbQuery.Text;
                parametros[1] = cbxDatabase.Text;
                worker.RunWorkerAsync(parametros);
            }
        }

        private void ColocarFoco(Control control)
        {
            if (control.CanFocus)
            {
                control.Focus();
            }
        }

        private void ExibirMensagem(string message, MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.Warning)
        {
            MessageBox.Show(message, Text, buttons, icon);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbQuery.Text))
            {
                ColocarFoco(txbQuery);
                ExibirMensagem("É necessário definir a consulta!");
            }
            else
            {
                dgvDados.DataSource = dadosGrid;
                try
                {
                    dgvDados.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);
                }
                catch (Exception) { }

                lblQtdRegistros.Text = string.Format("{0} registro(s).", (dadosGrid.Rows != null ? dadosGrid.Rows.Count : 0));
                tmTempoExecucao.Stop();
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] parametros = (object[])e.Argument;
            if (!string.IsNullOrEmpty(parametros[0].ToString()))
            {
                string queryPreparada = string.Format("USE {0}; {1}", parametros[1].ToString(), parametros[0].ToString());
                dadosGrid = new ConexaoClass().RetornarDados(queryPreparada);
            }
        }
    }
}
