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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConsultaSql));
            this.pnOpcoes = new System.Windows.Forms.Panel();
            this.lblTempoExecucao = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cbxDatabase = new System.Windows.Forms.ComboBox();
            this.lblQtdRegistros = new System.Windows.Forms.Label();
            this.ofdArquivo = new System.Windows.Forms.OpenFileDialog();
            this.tmTempoExecucao = new System.Windows.Forms.Timer(this.components);
            this.spcQueryDados = new System.Windows.Forms.SplitContainer();
            this.txbQuery = new System.Windows.Forms.TextBox();
            this.dgvDados = new System.Windows.Forms.DataGridView();
            this.btnAbrirArquivo = new System.Windows.Forms.Button();
            this.btnExecutar = new System.Windows.Forms.Button();
            this.pnOpcoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcQueryDados)).BeginInit();
            this.spcQueryDados.Panel1.SuspendLayout();
            this.spcQueryDados.Panel2.SuspendLayout();
            this.spcQueryDados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).BeginInit();
            this.SuspendLayout();
            // 
            // pnOpcoes
            // 
            this.pnOpcoes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnOpcoes.Controls.Add(this.lblTempoExecucao);
            this.pnOpcoes.Controls.Add(this.lblStatus);
            this.pnOpcoes.Controls.Add(this.cbxDatabase);
            this.pnOpcoes.Controls.Add(this.lblQtdRegistros);
            this.pnOpcoes.Controls.Add(this.btnAbrirArquivo);
            this.pnOpcoes.Controls.Add(this.btnExecutar);
            this.pnOpcoes.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnOpcoes.Location = new System.Drawing.Point(0, 0);
            this.pnOpcoes.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnOpcoes.Name = "pnOpcoes";
            this.pnOpcoes.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.pnOpcoes.Size = new System.Drawing.Size(161, 491);
            this.pnOpcoes.TabIndex = 0;
            // 
            // lblTempoExecucao
            // 
            this.lblTempoExecucao.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTempoExecucao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTempoExecucao.Location = new System.Drawing.Point(5, 431);
            this.lblTempoExecucao.Name = "lblTempoExecucao";
            this.lblTempoExecucao.Size = new System.Drawing.Size(154, 19);
            this.lblTempoExecucao.TabIndex = 6;
            this.lblTempoExecucao.Text = "00:00:00";
            this.lblTempoExecucao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Location = new System.Drawing.Point(5, 450);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(154, 20);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Aguardando...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbxDatabase
            // 
            this.cbxDatabase.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbxDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDatabase.DropDownWidth = 116;
            this.cbxDatabase.Location = new System.Drawing.Point(5, 119);
            this.cbxDatabase.MaxDropDownItems = 15;
            this.cbxDatabase.Name = "cbxDatabase";
            this.cbxDatabase.Size = new System.Drawing.Size(154, 21);
            this.cbxDatabase.TabIndex = 3;
            // 
            // lblQtdRegistros
            // 
            this.lblQtdRegistros.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblQtdRegistros.Location = new System.Drawing.Point(5, 470);
            this.lblQtdRegistros.Name = "lblQtdRegistros";
            this.lblQtdRegistros.Size = new System.Drawing.Size(154, 19);
            this.lblQtdRegistros.TabIndex = 2;
            this.lblQtdRegistros.Text = "0 registro(s).";
            this.lblQtdRegistros.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ofdArquivo
            // 
            this.ofdArquivo.Filter = "Sql Files|*.sql|Text Files|*.txt";
            // 
            // tmTempoExecucao
            // 
            this.tmTempoExecucao.Interval = 1000;
            this.tmTempoExecucao.Tick += new System.EventHandler(this.AtualizarTempoExecucao);
            // 
            // spcQueryDados
            // 
            this.spcQueryDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcQueryDados.Location = new System.Drawing.Point(161, 0);
            this.spcQueryDados.Name = "spcQueryDados";
            this.spcQueryDados.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcQueryDados.Panel1
            // 
            this.spcQueryDados.Panel1.Controls.Add(this.txbQuery);
            this.spcQueryDados.Panel1.Padding = new System.Windows.Forms.Padding(10);
            this.spcQueryDados.Panel1MinSize = 50;
            // 
            // spcQueryDados.Panel2
            // 
            this.spcQueryDados.Panel2.Controls.Add(this.dgvDados);
            this.spcQueryDados.Panel2MinSize = 50;
            this.spcQueryDados.Size = new System.Drawing.Size(578, 491);
            this.spcQueryDados.SplitterDistance = 180;
            this.spcQueryDados.TabIndex = 6;
            // 
            // txbQuery
            // 
            this.txbQuery.BackColor = System.Drawing.SystemColors.Menu;
            this.txbQuery.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txbQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbQuery.Location = new System.Drawing.Point(10, 10);
            this.txbQuery.Multiline = true;
            this.txbQuery.Name = "txbQuery";
            this.txbQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbQuery.Size = new System.Drawing.Size(558, 160);
            this.txbQuery.TabIndex = 1;
            // 
            // dgvDados
            // 
            this.dgvDados.AllowUserToAddRows = false;
            this.dgvDados.AllowUserToDeleteRows = false;
            this.dgvDados.AllowUserToResizeRows = false;
            this.dgvDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDados.Location = new System.Drawing.Point(0, 0);
            this.dgvDados.Name = "dgvDados";
            this.dgvDados.ReadOnly = true;
            this.dgvDados.Size = new System.Drawing.Size(578, 307);
            this.dgvDados.TabIndex = 1;
            // 
            // btnAbrirArquivo
            // 
            this.btnAbrirArquivo.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAbrirArquivo.Image = global::ConsultaSql.Properties.Resources.Open_file_icon1;
            this.btnAbrirArquivo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbrirArquivo.Location = new System.Drawing.Point(5, 62);
            this.btnAbrirArquivo.Name = "btnAbrirArquivo";
            this.btnAbrirArquivo.Padding = new System.Windows.Forms.Padding(5);
            this.btnAbrirArquivo.Size = new System.Drawing.Size(154, 57);
            this.btnAbrirArquivo.TabIndex = 1;
            this.btnAbrirArquivo.Text = "Abrir Query";
            this.btnAbrirArquivo.UseVisualStyleBackColor = true;
            this.btnAbrirArquivo.Click += new System.EventHandler(this.btnAbrirArquivo_Click);
            // 
            // btnExecutar
            // 
            this.btnExecutar.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnExecutar.Image = global::ConsultaSql.Properties.Resources.database_search_icon1;
            this.btnExecutar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExecutar.Location = new System.Drawing.Point(5, 5);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Padding = new System.Windows.Forms.Padding(5);
            this.btnExecutar.Size = new System.Drawing.Size(154, 57);
            this.btnExecutar.TabIndex = 0;
            this.btnExecutar.Text = "Executar";
            this.btnExecutar.UseVisualStyleBackColor = true;
            this.btnExecutar.Click += new System.EventHandler(this.btnExecutar_Click);
            // 
            // frmConsultaSql
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 491);
            this.Controls.Add(this.spcQueryDados);
            this.Controls.Add(this.pnOpcoes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmConsultaSql";
            this.Text = "Consulta - SQL";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConsultaSql_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmConsultaSql_FormClosed);
            this.Load += new System.EventHandler(this.frmConsultaSql_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EventoKeyDown);
            this.pnOpcoes.ResumeLayout(false);
            this.spcQueryDados.Panel1.ResumeLayout(false);
            this.spcQueryDados.Panel1.PerformLayout();
            this.spcQueryDados.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcQueryDados)).EndInit();
            this.spcQueryDados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnOpcoes;
        private System.Windows.Forms.Button btnAbrirArquivo;
        private System.Windows.Forms.Button btnExecutar;
        private System.Windows.Forms.Label lblQtdRegistros;
        private System.Windows.Forms.ComboBox cbxDatabase;
        private System.Windows.Forms.OpenFileDialog ofdArquivo;
        private System.Windows.Forms.Timer tmTempoExecucao;
        private System.Windows.Forms.SplitContainer spcQueryDados;
        private System.Windows.Forms.TextBox txbQuery;
        private System.Windows.Forms.DataGridView dgvDados;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblTempoExecucao;
    }
}

