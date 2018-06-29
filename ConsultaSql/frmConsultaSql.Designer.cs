namespace ConsultaSql
{
    partial class frmConsultaSql
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnOpcoes = new System.Windows.Forms.Panel();
            this.cbxDatabase = new System.Windows.Forms.ComboBox();
            this.lblQtdRegistros = new System.Windows.Forms.Label();
            this.btnAbrirArquivo = new System.Windows.Forms.Button();
            this.btnExecutar = new System.Windows.Forms.Button();
            this.pnTextoQuery = new System.Windows.Forms.Panel();
            this.txbQuery = new System.Windows.Forms.TextBox();
            this.pnGrid = new System.Windows.Forms.Panel();
            this.dgvDados = new System.Windows.Forms.DataGridView();
            this.ofdArquivo = new System.Windows.Forms.OpenFileDialog();
            this.pnOpcoes.SuspendLayout();
            this.pnTextoQuery.SuspendLayout();
            this.pnGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).BeginInit();
            this.SuspendLayout();
            // 
            // pnOpcoes
            // 
            this.pnOpcoes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnOpcoes.Controls.Add(this.cbxDatabase);
            this.pnOpcoes.Controls.Add(this.lblQtdRegistros);
            this.pnOpcoes.Controls.Add(this.btnAbrirArquivo);
            this.pnOpcoes.Controls.Add(this.btnExecutar);
            this.pnOpcoes.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnOpcoes.Location = new System.Drawing.Point(0, 0);
            this.pnOpcoes.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnOpcoes.Name = "pnOpcoes";
            this.pnOpcoes.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.pnOpcoes.Size = new System.Drawing.Size(123, 491);
            this.pnOpcoes.TabIndex = 0;
            // 
            // cbxDatabase
            // 
            this.cbxDatabase.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbxDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDatabase.DropDownWidth = 116;
            this.cbxDatabase.Location = new System.Drawing.Point(5, 51);
            this.cbxDatabase.MaxDropDownItems = 15;
            this.cbxDatabase.Name = "cbxDatabase";
            this.cbxDatabase.Size = new System.Drawing.Size(116, 21);
            this.cbxDatabase.TabIndex = 3;
            // 
            // lblQtdRegistros
            // 
            this.lblQtdRegistros.AutoSize = true;
            this.lblQtdRegistros.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblQtdRegistros.Location = new System.Drawing.Point(5, 476);
            this.lblQtdRegistros.Name = "lblQtdRegistros";
            this.lblQtdRegistros.Size = new System.Drawing.Size(64, 13);
            this.lblQtdRegistros.TabIndex = 2;
            this.lblQtdRegistros.Text = "0 registro(s).";
            // 
            // btnAbrirArquivo
            // 
            this.btnAbrirArquivo.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAbrirArquivo.Location = new System.Drawing.Point(5, 28);
            this.btnAbrirArquivo.Name = "btnAbrirArquivo";
            this.btnAbrirArquivo.Size = new System.Drawing.Size(116, 23);
            this.btnAbrirArquivo.TabIndex = 1;
            this.btnAbrirArquivo.Text = "Abrir Query";
            this.btnAbrirArquivo.UseVisualStyleBackColor = true;
            this.btnAbrirArquivo.Click += new System.EventHandler(this.btnAbrirArquivo_Click);
            // 
            // btnExecutar
            // 
            this.btnExecutar.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnExecutar.Location = new System.Drawing.Point(5, 5);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(116, 23);
            this.btnExecutar.TabIndex = 0;
            this.btnExecutar.Text = "Executar";
            this.btnExecutar.UseVisualStyleBackColor = true;
            this.btnExecutar.Click += new System.EventHandler(this.btnExecutar_Click);
            // 
            // pnTextoQuery
            // 
            this.pnTextoQuery.Controls.Add(this.txbQuery);
            this.pnTextoQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTextoQuery.Location = new System.Drawing.Point(123, 0);
            this.pnTextoQuery.Name = "pnTextoQuery";
            this.pnTextoQuery.Padding = new System.Windows.Forms.Padding(10);
            this.pnTextoQuery.Size = new System.Drawing.Size(616, 219);
            this.pnTextoQuery.TabIndex = 1;
            // 
            // txbQuery
            // 
            this.txbQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbQuery.Location = new System.Drawing.Point(10, 10);
            this.txbQuery.Multiline = true;
            this.txbQuery.Name = "txbQuery";
            this.txbQuery.Size = new System.Drawing.Size(596, 199);
            this.txbQuery.TabIndex = 0;
            // 
            // pnGrid
            // 
            this.pnGrid.Controls.Add(this.dgvDados);
            this.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnGrid.Location = new System.Drawing.Point(123, 219);
            this.pnGrid.Name = "pnGrid";
            this.pnGrid.Size = new System.Drawing.Size(616, 272);
            this.pnGrid.TabIndex = 2;
            // 
            // dgvDados
            // 
            this.dgvDados.AllowUserToAddRows = false;
            this.dgvDados.AllowUserToDeleteRows = false;
            this.dgvDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDados.Location = new System.Drawing.Point(0, 0);
            this.dgvDados.Name = "dgvDados";
            this.dgvDados.ReadOnly = true;
            this.dgvDados.Size = new System.Drawing.Size(616, 272);
            this.dgvDados.TabIndex = 0;
            // 
            // ofdArquivo
            // 
            this.ofdArquivo.Filter = "Sql Files|*.sql|Text Files|*.txt";
            // 
            // frmConsultaSql
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 491);
            this.Controls.Add(this.pnGrid);
            this.Controls.Add(this.pnTextoQuery);
            this.Controls.Add(this.pnOpcoes);
            this.Name = "frmConsultaSql";
            this.Text = "Consulta - SQL";
            this.Load += new System.EventHandler(this.frmConsultaSql_Load);
            this.pnOpcoes.ResumeLayout(false);
            this.pnOpcoes.PerformLayout();
            this.pnTextoQuery.ResumeLayout(false);
            this.pnTextoQuery.PerformLayout();
            this.pnGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnOpcoes;
        private System.Windows.Forms.Button btnAbrirArquivo;
        private System.Windows.Forms.Button btnExecutar;
        private System.Windows.Forms.Panel pnTextoQuery;
        private System.Windows.Forms.Panel pnGrid;
        private System.Windows.Forms.Label lblQtdRegistros;
        private System.Windows.Forms.TextBox txbQuery;
        private System.Windows.Forms.DataGridView dgvDados;
        private System.Windows.Forms.ComboBox cbxDatabase;
        private System.Windows.Forms.OpenFileDialog ofdArquivo;
    }
}

