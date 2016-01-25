namespace nxprice_ui
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tbProductId = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buyIndexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageIndexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yearRateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dayLeftDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minMountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dealCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productionIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zCBRecordBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zCBRecordBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(1000, 837);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("https://my.alipay.com/portal/i.htm", System.UriKind.Absolute);
            // 
            // tbProductId
            // 
            this.tbProductId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbProductId.Location = new System.Drawing.Point(99, 22);
            this.tbProductId.Name = "tbProductId";
            this.tbProductId.ReadOnly = true;
            this.tbProductId.Size = new System.Drawing.Size(360, 26);
            this.tbProductId.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.webBrowser1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 837);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.BtnRefresh);
            this.panel2.Controls.Add(this.tbProductId);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1279, 65);
            this.panel2.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = global::nxprice_ui.Properties.Resources.currency_yuan;
            this.pictureBox1.Location = new System.Drawing.Point(1211, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(68, 65);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "ProductId";
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRefresh.Location = new System.Drawing.Point(1066, 17);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(119, 28);
            this.BtnRefresh.TabIndex = 2;
            this.BtnRefresh.Text = "刷新";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.buyIndexDataGridViewTextBoxColumn,
            this.pageIndexDataGridViewTextBoxColumn,
            this.yearRateDataGridViewTextBoxColumn,
            this.dayLeftDataGridViewTextBoxColumn,
            this.minMountDataGridViewTextBoxColumn,
            this.dealCountDataGridViewTextBoxColumn,
            this.productionIDDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.zCBRecordBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(275, 837);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // buyIndexDataGridViewTextBoxColumn
            // 
            this.buyIndexDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.buyIndexDataGridViewTextBoxColumn.DataPropertyName = "BuyIndex";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.buyIndexDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.buyIndexDataGridViewTextBoxColumn.HeaderText = "BuyIndex";
            this.buyIndexDataGridViewTextBoxColumn.Name = "buyIndexDataGridViewTextBoxColumn";
            this.buyIndexDataGridViewTextBoxColumn.ReadOnly = true;
            this.buyIndexDataGridViewTextBoxColumn.Width = 76;
            // 
            // pageIndexDataGridViewTextBoxColumn
            // 
            this.pageIndexDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pageIndexDataGridViewTextBoxColumn.DataPropertyName = "PageIndex";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pageIndexDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.pageIndexDataGridViewTextBoxColumn.HeaderText = "P.";
            this.pageIndexDataGridViewTextBoxColumn.Name = "pageIndexDataGridViewTextBoxColumn";
            this.pageIndexDataGridViewTextBoxColumn.ReadOnly = true;
            this.pageIndexDataGridViewTextBoxColumn.Width = 42;
            // 
            // yearRateDataGridViewTextBoxColumn
            // 
            this.yearRateDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.yearRateDataGridViewTextBoxColumn.DataPropertyName = "YearRate";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.yearRateDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.yearRateDataGridViewTextBoxColumn.HeaderText = "R.";
            this.yearRateDataGridViewTextBoxColumn.Name = "yearRateDataGridViewTextBoxColumn";
            this.yearRateDataGridViewTextBoxColumn.ReadOnly = true;
            this.yearRateDataGridViewTextBoxColumn.Width = 43;
            // 
            // dayLeftDataGridViewTextBoxColumn
            // 
            this.dayLeftDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dayLeftDataGridViewTextBoxColumn.DataPropertyName = "DayLeft";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dayLeftDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.dayLeftDataGridViewTextBoxColumn.HeaderText = "Day";
            this.dayLeftDataGridViewTextBoxColumn.Name = "dayLeftDataGridViewTextBoxColumn";
            this.dayLeftDataGridViewTextBoxColumn.ReadOnly = true;
            this.dayLeftDataGridViewTextBoxColumn.Width = 51;
            // 
            // minMountDataGridViewTextBoxColumn
            // 
            this.minMountDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.minMountDataGridViewTextBoxColumn.DataPropertyName = "MinMount";
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.minMountDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.minMountDataGridViewTextBoxColumn.HeaderText = "MinMount";
            this.minMountDataGridViewTextBoxColumn.Name = "minMountDataGridViewTextBoxColumn";
            this.minMountDataGridViewTextBoxColumn.ReadOnly = true;
            this.minMountDataGridViewTextBoxColumn.Width = 79;
            // 
            // dealCountDataGridViewTextBoxColumn
            // 
            this.dealCountDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dealCountDataGridViewTextBoxColumn.DataPropertyName = "DealCount";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dealCountDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.dealCountDataGridViewTextBoxColumn.HeaderText = "Deal.";
            this.dealCountDataGridViewTextBoxColumn.Name = "dealCountDataGridViewTextBoxColumn";
            this.dealCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.dealCountDataGridViewTextBoxColumn.Width = 57;
            // 
            // productionIDDataGridViewTextBoxColumn
            // 
            this.productionIDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.productionIDDataGridViewTextBoxColumn.DataPropertyName = "ProductionID";
            this.productionIDDataGridViewTextBoxColumn.HeaderText = "ProductionID";
            this.productionIDDataGridViewTextBoxColumn.Name = "productionIDDataGridViewTextBoxColumn";
            this.productionIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // zCBRecordBindingSource
            // 
            this.zCBRecordBindingSource.DataSource = typeof(nxprice_data.ZCBRecord);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 65);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1MinSize = 1000;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(1279, 837);
            this.splitContainer1.SplitterDistance = 1000;
            this.splitContainer1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1279, 902);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "NxZhaoCaiBao";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zCBRecordBindingSource)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TextBox tbProductId;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource zCBRecordBindingSource;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn buyIndexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pageIndexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn yearRateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dayLeftDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn minMountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dealCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productionIDDataGridViewTextBoxColumn;
    }
}

