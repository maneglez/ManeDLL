namespace Mane.Helpers
{
    partial class MostrarDatosGenerico
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
            this.dgvContenido = new System.Windows.Forms.DataGridView();
            this.cbFiltro = new System.Windows.Forms.ComboBox();
            this.tbBusqueda = new System.Windows.Forms.TextBox();
            this.grpFecha = new System.Windows.Forms.GroupBox();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContenido)).BeginInit();
            this.grpFecha.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvContenido
            // 
            this.dgvContenido.AllowUserToAddRows = false;
            this.dgvContenido.AllowUserToDeleteRows = false;
            this.dgvContenido.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvContenido.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvContenido.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvContenido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContenido.Location = new System.Drawing.Point(9, 83);
            this.dgvContenido.Margin = new System.Windows.Forms.Padding(2);
            this.dgvContenido.MultiSelect = false;
            this.dgvContenido.Name = "dgvContenido";
            this.dgvContenido.ReadOnly = true;
            this.dgvContenido.RowHeadersVisible = false;
            this.dgvContenido.RowHeadersWidth = 51;
            this.dgvContenido.RowTemplate.Height = 24;
            this.dgvContenido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvContenido.Size = new System.Drawing.Size(587, 332);
            this.dgvContenido.TabIndex = 1;
            this.dgvContenido.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvContenido_CellDoubleClick);
            this.dgvContenido.SelectionChanged += new System.EventHandler(this.dgvContenido_SelectionChanged);
            // 
            // cbFiltro
            // 
            this.cbFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFiltro.FormattingEnabled = true;
            this.cbFiltro.Location = new System.Drawing.Point(9, 10);
            this.cbFiltro.Margin = new System.Windows.Forms.Padding(2);
            this.cbFiltro.Name = "cbFiltro";
            this.cbFiltro.Size = new System.Drawing.Size(202, 21);
            this.cbFiltro.TabIndex = 4;
            // 
            // tbBusqueda
            // 
            this.tbBusqueda.Location = new System.Drawing.Point(9, 35);
            this.tbBusqueda.Margin = new System.Windows.Forms.Padding(2);
            this.tbBusqueda.Name = "tbBusqueda";
            this.tbBusqueda.Size = new System.Drawing.Size(202, 20);
            this.tbBusqueda.TabIndex = 3;
            this.tbBusqueda.TextChanged += new System.EventHandler(this.tbBusqueda_TextChanged);
            // 
            // grpFecha
            // 
            this.grpFecha.Controls.Add(this.label2);
            this.grpFecha.Controls.Add(this.label1);
            this.grpFecha.Controls.Add(this.dtpHasta);
            this.grpFecha.Controls.Add(this.dtpDesde);
            this.grpFecha.Location = new System.Drawing.Point(244, 10);
            this.grpFecha.Name = "grpFecha";
            this.grpFecha.Size = new System.Drawing.Size(262, 68);
            this.grpFecha.TabIndex = 5;
            this.grpFecha.TabStop = false;
            this.grpFecha.Text = "Filtrar por Fecha";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Location = new System.Drawing.Point(56, 38);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(200, 20);
            this.dtpHasta.TabIndex = 6;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Location = new System.Drawing.Point(56, 12);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(200, 20);
            this.dtpDesde.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Desde";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Hasta";
            // 
            // MostrarDatosGenerico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 426);
            this.Controls.Add(this.grpFecha);
            this.Controls.Add(this.cbFiltro);
            this.Controls.Add(this.tbBusqueda);
            this.Controls.Add(this.dgvContenido);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MostrarDatosGenerico";
            this.Text = "Seleccionar";
            this.Load += new System.EventHandler(this.SeleccionarGenerico_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvContenido)).EndInit();
            this.grpFecha.ResumeLayout(false);
            this.grpFecha.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbFiltro;
        private System.Windows.Forms.TextBox tbBusqueda;
        private System.Windows.Forms.GroupBox grpFecha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        public System.Windows.Forms.DataGridView dgvContenido;
    }
}