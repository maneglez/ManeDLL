namespace Mane.BD.Forms
{
    partial class SeleccionarGenerico
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
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.cbFiltro = new System.Windows.Forms.ComboBox();
            this.tbBusqueda = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContenido)).BeginInit();
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
            this.dgvContenido.Location = new System.Drawing.Point(9, 46);
            this.dgvContenido.Margin = new System.Windows.Forms.Padding(2);
            this.dgvContenido.MultiSelect = false;
            this.dgvContenido.Name = "dgvContenido";
            this.dgvContenido.ReadOnly = true;
            this.dgvContenido.RowHeadersVisible = false;
            this.dgvContenido.RowHeadersWidth = 51;
            this.dgvContenido.RowTemplate.Height = 24;
            this.dgvContenido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvContenido.Size = new System.Drawing.Size(334, 336);
            this.dgvContenido.TabIndex = 1;
            this.dgvContenido.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvContenido_CellDoubleClick);
            this.dgvContenido.SelectionChanged += new System.EventHandler(this.dgvContenido_SelectionChanged);
            this.dgvContenido.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvContenido_KeyDown);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(188, 387);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(84, 28);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSeleccionar.Location = new System.Drawing.Point(51, 387);
            this.btnSeleccionar.Margin = new System.Windows.Forms.Padding(2);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(84, 28);
            this.btnSeleccionar.TabIndex = 2;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            this.btnSeleccionar.Click += new System.EventHandler(this.button3_Click);
            // 
            // cbFiltro
            // 
            this.cbFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFiltro.FormattingEnabled = true;
            this.cbFiltro.Location = new System.Drawing.Point(9, 10);
            this.cbFiltro.Margin = new System.Windows.Forms.Padding(2);
            this.cbFiltro.Name = "cbFiltro";
            this.cbFiltro.Size = new System.Drawing.Size(134, 21);
            this.cbFiltro.TabIndex = 4;
            // 
            // tbBusqueda
            // 
            this.tbBusqueda.Location = new System.Drawing.Point(147, 11);
            this.tbBusqueda.Margin = new System.Windows.Forms.Padding(2);
            this.tbBusqueda.Name = "tbBusqueda";
            this.tbBusqueda.Size = new System.Drawing.Size(202, 20);
            this.tbBusqueda.TabIndex = 3;
            this.tbBusqueda.TextChanged += new System.EventHandler(this.tbBusqueda_TextChanged);
            // 
            // SeleccionarGenerico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 426);
            this.Controls.Add(this.cbFiltro);
            this.Controls.Add(this.tbBusqueda);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.dgvContenido);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(370, 465);
            this.Name = "SeleccionarGenerico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Seleccionar";
            this.Load += new System.EventHandler(this.SeleccionarGenerico_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvContenido)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.ComboBox cbFiltro;
        private System.Windows.Forms.TextBox tbBusqueda;
        public System.Windows.Forms.DataGridView dgvContenido;
    }
}