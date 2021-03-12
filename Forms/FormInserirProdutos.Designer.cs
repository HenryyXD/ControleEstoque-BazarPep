namespace SistemaBazarPep.Forms {
    partial class FormInserirProdutos {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.gbDadosProd = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNumDesconto = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNumPreco = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNumQtd = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cbProdutos = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnProd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnQtd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPreco = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDesconto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblPreco = new System.Windows.Forms.Label();
            this.gbDadosProd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumDesconto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumPreco)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumQtd)).BeginInit();
            this.SuspendLayout();
            // 
            // gbDadosProd
            // 
            this.gbDadosProd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDadosProd.Controls.Add(this.label5);
            this.gbDadosProd.Controls.Add(this.txtNumDesconto);
            this.gbDadosProd.Controls.Add(this.label4);
            this.gbDadosProd.Controls.Add(this.txtNumPreco);
            this.gbDadosProd.Controls.Add(this.label3);
            this.gbDadosProd.Controls.Add(this.txtNumQtd);
            this.gbDadosProd.Controls.Add(this.label2);
            this.gbDadosProd.Controls.Add(this.cbProdutos);
            this.gbDadosProd.Controls.Add(this.label1);
            this.gbDadosProd.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDadosProd.Location = new System.Drawing.Point(11, 12);
            this.gbDadosProd.Name = "gbDadosProd";
            this.gbDadosProd.Size = new System.Drawing.Size(684, 130);
            this.gbDadosProd.TabIndex = 0;
            this.gbDadosProd.TabStop = false;
            this.gbDadosProd.Text = "Inserir Produtos";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Schoolbook", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(639, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 23);
            this.label5.TabIndex = 29;
            this.label5.Text = "%";
            // 
            // txtNumDesconto
            // 
            this.txtNumDesconto.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNumDesconto.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNumDesconto.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumDesconto.Location = new System.Drawing.Point(588, 83);
            this.txtNumDesconto.Name = "txtNumDesconto";
            this.txtNumDesconto.Size = new System.Drawing.Size(51, 26);
            this.txtNumDesconto.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(584, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 20);
            this.label4.TabIndex = 27;
            this.label4.Text = "Desconto";
            // 
            // txtNumPreco
            // 
            this.txtNumPreco.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNumPreco.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNumPreco.DecimalPlaces = 2;
            this.txtNumPreco.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumPreco.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.txtNumPreco.Location = new System.Drawing.Point(447, 83);
            this.txtNumPreco.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.txtNumPreco.Name = "txtNumPreco";
            this.txtNumPreco.Size = new System.Drawing.Size(109, 26);
            this.txtNumPreco.TabIndex = 26;
            this.txtNumPreco.ThousandsSeparator = true;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(443, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 20);
            this.label3.TabIndex = 25;
            this.label3.Text = "Preço Total";
            // 
            // txtNumQtd
            // 
            this.txtNumQtd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNumQtd.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNumQtd.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumQtd.Location = new System.Drawing.Point(300, 83);
            this.txtNumQtd.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.txtNumQtd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNumQtd.Name = "txtNumQtd";
            this.txtNumQtd.Size = new System.Drawing.Size(109, 26);
            this.txtNumQtd.TabIndex = 24;
            this.txtNumQtd.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNumQtd.ValueChanged += new System.EventHandler(this.txtNumQtd_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(296, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "Quantidade";
            // 
            // cbProdutos
            // 
            this.cbProdutos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbProdutos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbProdutos.DropDownHeight = 140;
            this.cbProdutos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProdutos.DropDownWidth = 270;
            this.cbProdutos.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProdutos.FormattingEnabled = true;
            this.cbProdutos.IntegralHeight = false;
            this.cbProdutos.ItemHeight = 20;
            this.cbProdutos.Location = new System.Drawing.Point(19, 83);
            this.cbProdutos.Name = "cbProdutos";
            this.cbProdutos.Size = new System.Drawing.Size(241, 28);
            this.cbProdutos.TabIndex = 20;
            this.cbProdutos.SelectedIndexChanged += new System.EventHandler(this.cbProdutos_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "Produto \r\n(ID - Nome - Preço - Estoque)";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnId,
            this.columnProd,
            this.columnQtd,
            this.columnPreco,
            this.columnDesconto});
            this.listView1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(11, 206);
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(683, 312);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnId
            // 
            this.columnId.Text = "ID";
            this.columnId.Width = 63;
            // 
            // columnProd
            // 
            this.columnProd.Text = "Produto";
            this.columnProd.Width = 288;
            // 
            // columnQtd
            // 
            this.columnQtd.Text = "Quantidade";
            this.columnQtd.Width = 111;
            // 
            // columnPreco
            // 
            this.columnPreco.Text = "Preço Total";
            this.columnPreco.Width = 132;
            // 
            // columnDesconto
            // 
            this.columnDesconto.Text = "Desconto";
            this.columnDesconto.Width = 85;
            // 
            // btnRemover
            // 
            this.btnRemover.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRemover.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnRemover.FlatAppearance.BorderSize = 0;
            this.btnRemover.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemover.Font = new System.Drawing.Font("Linux Biolinum G", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemover.Location = new System.Drawing.Point(229, 148);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(94, 36);
            this.btnRemover.TabIndex = 3;
            this.btnRemover.Text = "-";
            this.btnRemover.UseVisualStyleBackColor = false;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdicionar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnAdicionar.FlatAppearance.BorderSize = 0;
            this.btnAdicionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionar.Font = new System.Drawing.Font("Linux Biolinum G", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdicionar.Location = new System.Drawing.Point(352, 148);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(94, 36);
            this.btnAdicionar.TabIndex = 4;
            this.btnAdicionar.Text = "+";
            this.btnAdicionar.UseVisualStyleBackColor = false;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(602, 524);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(94, 36);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSalvar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.Location = new System.Drawing.Point(500, 524);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(94, 36);
            this.btnSalvar.TabIndex = 6;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 532);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 20);
            this.label6.TabIndex = 22;
            this.label6.Text = "Preço Total: R$";
            // 
            // lblPreco
            // 
            this.lblPreco.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPreco.AutoSize = true;
            this.lblPreco.BackColor = System.Drawing.SystemColors.Control;
            this.lblPreco.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreco.Location = new System.Drawing.Point(122, 532);
            this.lblPreco.Name = "lblPreco";
            this.lblPreco.Size = new System.Drawing.Size(0, 20);
            this.lblPreco.TabIndex = 23;
            // 
            // FormInserirProdutos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(708, 572);
            this.Controls.Add(this.lblPreco);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.btnRemover);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.gbDadosProd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(708, 572);
            this.Name = "FormInserirProdutos";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inserir Produtos";
            this.Load += new System.EventHandler(this.FormInserirProdutos_Load);
            this.gbDadosProd.ResumeLayout(false);
            this.gbDadosProd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumDesconto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumPreco)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumQtd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDadosProd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbProdutos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtNumQtd;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.NumericUpDown txtNumPreco;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown txtNumDesconto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.ColumnHeader columnProd;
        private System.Windows.Forms.ColumnHeader columnQtd;
        private System.Windows.Forms.ColumnHeader columnPreco;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.ColumnHeader columnId;
        public System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnDesconto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPreco;
    }
}