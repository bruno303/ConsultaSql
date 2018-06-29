using ConsultaSql.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace ConsultaSql
{
    public partial class frmConsultaSql : Form
    {
        public frmConsultaSql()
        {
            InitializeComponent();
        }

        private void frmConsultaSql_Load(object sender, EventArgs e)
        {
            CarregarDatabases();
        }

        private void CarregarDatabases()
        {
            DataTable databases = new clsConexao().RetornarDados("SELECT NAME FROM MASTER.SYS.DATABASES WITH(NOLOCK)");
            foreach (DataRow row in databases.Rows)
            {
                cbxDatabase.Items.Add(row[0].ToString());
            }
            cbxDatabase.SelectedIndex = 0;
        }

        private void btnAbrirArquivo_Click(object sender, EventArgs e)
        {
            ofdArquivo.ShowDialog();
            if (!string.IsNullOrEmpty(ofdArquivo.FileName))
            {
                txbQuery.Text = System.IO.File.ReadAllText(ofdArquivo.FileName);
            }
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txbQuery.Text))
            {
                string queryPreparada = string.Format("USE {0}; {1}", cbxDatabase.Text, txbQuery.Text);
                DataTable dados = new clsConexao().RetornarDados(queryPreparada);

                dgvDados.DataSource = dados;
                try
                {
                    dgvDados.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);
                }
                catch (Exception) { }

                lblQtdRegistros.Text = string.Format("{0} registro(s).", dados.Rows.Count);
            }
            else
            {
                ColocarFoco(txbQuery);
                ShowMessage("É necessário definir a consulta!");
            }
        }

        private void ColocarFoco(Control control)
        {
            if (control.CanFocus)
            {
                control.Focus();
            }
        }

        private void ShowMessage(string message, MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.Warning)
        {
            MessageBox.Show(message, Text, buttons, icon);
        }
    }
}
