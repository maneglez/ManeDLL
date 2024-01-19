namespace Mane.CommonModules.WMS
{
    partial class BuscarArticulosConteo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvStock = new System.Windows.Forms.DataGridView();
            this.colCodAlmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUbicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSerieLote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTipoManejo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkStockPorUbicacion = new System.Windows.Forms.CheckBox();
            this.chkStockBasadoEnSerieLote = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbSerieLote = new System.Windows.Forms.Label();
            this.chkSinStock = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tbProveedor = new System.Windows.Forms.TextBox();
            this.tbSerieLote = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbArticulo = new System.Windows.Forms.TextBox();
            this.tbUbicacion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbAlmacen = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkGestionNinguna = new System.Windows.Forms.CheckBox();
            this.chkGestionSerie = new System.Windows.Forms.CheckBox();
            this.chkGestionLotes = new System.Windows.Forms.CheckBox();
            this.lbRecuento = new System.Windows.Forms.Label();
            this.lblStock = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvStock
            // 
            this.dgvStock.AllowUserToAddRows = false;
            this.dgvStock.AllowUserToDeleteRows = false;
            this.dgvStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStock.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCodAlmacen,
            this.colUbicacion,
            this.colItemCode,
            this.colItemName,
            this.colStock,
            this.colSerieLote,
            this.colTipoManejo});
            this.dgvStock.Location = new System.Drawing.Point(14, 204);
            this.dgvStock.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvStock.Name = "dgvStock";
            this.dgvStock.ReadOnly = true;
            this.dgvStock.Size = new System.Drawing.Size(880, 265);
            this.dgvStock.TabIndex = 0;
            // 
            // colCodAlmacen
            // 
            this.colCodAlmacen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colCodAlmacen.DataPropertyName = "WhsCode";
            this.colCodAlmacen.HeaderText = "Almacén";
            this.colCodAlmacen.Name = "colCodAlmacen";
            this.colCodAlmacen.ReadOnly = true;
            this.colCodAlmacen.Width = 78;
            // 
            // colUbicacion
            // 
            this.colUbicacion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colUbicacion.DataPropertyName = "BinCode";
            dataGridViewCellStyle1.NullValue = "N/A";
            this.colUbicacion.DefaultCellStyle = dataGridViewCellStyle1;
            this.colUbicacion.HeaderText = "Ubicación";
            this.colUbicacion.Name = "colUbicacion";
            this.colUbicacion.ReadOnly = true;
            this.colUbicacion.Width = 83;
            // 
            // colItemCode
            // 
            this.colItemCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colItemCode.DataPropertyName = "ItemCode";
            this.colItemCode.HeaderText = "Articulo";
            this.colItemCode.Name = "colItemCode";
            this.colItemCode.ReadOnly = true;
            this.colItemCode.Width = 73;
            // 
            // colItemName
            // 
            this.colItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colItemName.DataPropertyName = "ItemName";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colItemName.DefaultCellStyle = dataGridViewCellStyle2;
            this.colItemName.HeaderText = "Descripción";
            this.colItemName.Name = "colItemName";
            this.colItemName.ReadOnly = true;
            this.colItemName.Width = 250;
            // 
            // colStock
            // 
            this.colStock.DataPropertyName = "Quantity";
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = "0";
            this.colStock.DefaultCellStyle = dataGridViewCellStyle3;
            this.colStock.HeaderText = "Stock ";
            this.colStock.Name = "colStock";
            this.colStock.ReadOnly = true;
            // 
            // colSerieLote
            // 
            this.colSerieLote.DataPropertyName = "DistNumber";
            dataGridViewCellStyle4.NullValue = "N/A";
            this.colSerieLote.DefaultCellStyle = dataGridViewCellStyle4;
            this.colSerieLote.HeaderText = "Serie/Lote";
            this.colSerieLote.Name = "colSerieLote";
            this.colSerieLote.ReadOnly = true;
            // 
            // colTipoManejo
            // 
            this.colTipoManejo.DataPropertyName = "TipoManejo";
            dataGridViewCellStyle5.NullValue = "N/A";
            this.colTipoManejo.DefaultCellStyle = dataGridViewCellStyle5;
            this.colTipoManejo.HeaderText = "Tipo de Gestión";
            this.colTipoManejo.Name = "colTipoManejo";
            this.colTipoManejo.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkStockPorUbicacion);
            this.groupBox1.Controls.Add(this.chkStockBasadoEnSerieLote);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lbSerieLote);
            this.groupBox1.Controls.Add(this.chkSinStock);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.tbProveedor);
            this.groupBox1.Controls.Add(this.tbSerieLote);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbArticulo);
            this.groupBox1.Controls.Add(this.tbUbicacion);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbAlmacen);
            this.groupBox1.Location = new System.Drawing.Point(14, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(693, 173);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtrar";
            // 
            // chkStockPorUbicacion
            // 
            this.chkStockPorUbicacion.AutoSize = true;
            this.chkStockPorUbicacion.Checked = true;
            this.chkStockPorUbicacion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStockPorUbicacion.Location = new System.Drawing.Point(7, 106);
            this.chkStockPorUbicacion.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkStockPorUbicacion.Name = "chkStockPorUbicacion";
            this.chkStockPorUbicacion.Size = new System.Drawing.Size(133, 18);
            this.chkStockPorUbicacion.TabIndex = 5;
            this.chkStockPorUbicacion.Text = "Stock por ubicación";
            this.chkStockPorUbicacion.UseVisualStyleBackColor = true;
            this.chkStockPorUbicacion.CheckedChanged += new System.EventHandler(this.checkStateChanged);
            // 
            // chkStockBasadoEnSerieLote
            // 
            this.chkStockBasadoEnSerieLote.AutoSize = true;
            this.chkStockBasadoEnSerieLote.Location = new System.Drawing.Point(7, 127);
            this.chkStockBasadoEnSerieLote.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkStockBasadoEnSerieLote.Name = "chkStockBasadoEnSerieLote";
            this.chkStockBasadoEnSerieLote.Size = new System.Drawing.Size(188, 18);
            this.chkStockBasadoEnSerieLote.TabIndex = 5;
            this.chkStockBasadoEnSerieLote.Text = "Stock Basado en Series/Lotes";
            this.chkStockBasadoEnSerieLote.UseVisualStyleBackColor = true;
            this.chkStockBasadoEnSerieLote.CheckedChanged += new System.EventHandler(this.checkStateChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(356, 107);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "Proveedor(es)";
            // 
            // lbSerieLote
            // 
            this.lbSerieLote.AutoSize = true;
            this.lbSerieLote.Location = new System.Drawing.Point(356, 62);
            this.lbSerieLote.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSerieLote.Name = "lbSerieLote";
            this.lbSerieLote.Size = new System.Drawing.Size(94, 14);
            this.lbSerieLote.TabIndex = 7;
            this.lbSerieLote.Text = "Serie(s)/Lote(s)";
            // 
            // chkSinStock
            // 
            this.chkSinStock.AutoSize = true;
            this.chkSinStock.Location = new System.Drawing.Point(7, 149);
            this.chkSinStock.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkSinStock.Name = "chkSinStock";
            this.chkSinStock.Size = new System.Drawing.Size(167, 18);
            this.chkSinStock.TabIndex = 5;
            this.chkSinStock.Text = "Mostrar Articulos sin stock";
            this.chkSinStock.UseVisualStyleBackColor = true;
            this.chkSinStock.CheckedChanged += new System.EventHandler(this.checkStateChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(245, 108);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 39);
            this.button1.TabIndex = 3;
            this.button1.Text = "Consultar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Consultar);
            // 
            // tbProveedor
            // 
            this.tbProveedor.Location = new System.Drawing.Point(359, 125);
            this.tbProveedor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbProveedor.Name = "tbProveedor";
            this.tbProveedor.Size = new System.Drawing.Size(326, 22);
            this.tbProveedor.TabIndex = 6;
            this.tbProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbProveedor_KeyDown);
            // 
            // tbSerieLote
            // 
            this.tbSerieLote.Location = new System.Drawing.Point(359, 80);
            this.tbSerieLote.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbSerieLote.Name = "tbSerieLote";
            this.tbSerieLote.Size = new System.Drawing.Size(326, 22);
            this.tbSerieLote.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ubicacion(es)";
            // 
            // tbArticulo
            // 
            this.tbArticulo.Location = new System.Drawing.Point(359, 38);
            this.tbArticulo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbArticulo.Name = "tbArticulo";
            this.tbArticulo.Size = new System.Drawing.Size(326, 22);
            this.tbArticulo.TabIndex = 0;
            this.tbArticulo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BuscarArticulo);
            // 
            // tbUbicacion
            // 
            this.tbUbicacion.Location = new System.Drawing.Point(10, 80);
            this.tbUbicacion.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbUbicacion.Name = "tbUbicacion";
            this.tbUbicacion.Size = new System.Drawing.Size(326, 22);
            this.tbUbicacion.TabIndex = 0;
            this.tbUbicacion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BuscarUbicacion);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(356, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "Artículo(s)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Almacen";
            // 
            // tbAlmacen
            // 
            this.tbAlmacen.Location = new System.Drawing.Point(10, 38);
            this.tbAlmacen.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbAlmacen.Name = "tbAlmacen";
            this.tbAlmacen.Size = new System.Drawing.Size(326, 22);
            this.tbAlmacen.TabIndex = 0;
            this.tbAlmacen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BuscarAlmacen);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkGestionNinguna);
            this.groupBox2.Controls.Add(this.chkGestionSerie);
            this.groupBox2.Controls.Add(this.chkGestionLotes);
            this.groupBox2.Location = new System.Drawing.Point(783, 12);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(309, 43);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mostrar artículos con gestión basada en";
            this.groupBox2.Visible = false;
            // 
            // chkGestionNinguna
            // 
            this.chkGestionNinguna.AutoSize = true;
            this.chkGestionNinguna.Checked = true;
            this.chkGestionNinguna.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGestionNinguna.Location = new System.Drawing.Point(146, 18);
            this.chkGestionNinguna.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkGestionNinguna.Name = "chkGestionNinguna";
            this.chkGestionNinguna.Size = new System.Drawing.Size(87, 18);
            this.chkGestionNinguna.TabIndex = 5;
            this.chkGestionNinguna.Text = "Sin Gestión";
            this.chkGestionNinguna.UseVisualStyleBackColor = true;
            // 
            // chkGestionSerie
            // 
            this.chkGestionSerie.AutoSize = true;
            this.chkGestionSerie.Checked = true;
            this.chkGestionSerie.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGestionSerie.Location = new System.Drawing.Point(7, 18);
            this.chkGestionSerie.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkGestionSerie.Name = "chkGestionSerie";
            this.chkGestionSerie.Size = new System.Drawing.Size(58, 18);
            this.chkGestionSerie.TabIndex = 5;
            this.chkGestionSerie.Text = "Series";
            this.chkGestionSerie.UseVisualStyleBackColor = true;
            this.chkGestionSerie.CheckedChanged += new System.EventHandler(this.checkStateChanged);
            // 
            // chkGestionLotes
            // 
            this.chkGestionLotes.AutoSize = true;
            this.chkGestionLotes.Checked = true;
            this.chkGestionLotes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGestionLotes.Location = new System.Drawing.Point(78, 18);
            this.chkGestionLotes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkGestionLotes.Name = "chkGestionLotes";
            this.chkGestionLotes.Size = new System.Drawing.Size(56, 18);
            this.chkGestionLotes.TabIndex = 5;
            this.chkGestionLotes.Text = "Lotes";
            this.chkGestionLotes.UseVisualStyleBackColor = true;
            this.chkGestionLotes.CheckedChanged += new System.EventHandler(this.checkStateChanged);
            // 
            // lbRecuento
            // 
            this.lbRecuento.AutoSize = true;
            this.lbRecuento.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRecuento.Location = new System.Drawing.Point(14, 186);
            this.lbRecuento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbRecuento.Name = "lbRecuento";
            this.lbRecuento.Size = new System.Drawing.Size(116, 13);
            this.lbRecuento.TabIndex = 4;
            this.lbRecuento.Text = "Mostrando: 0 registros";
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStock.Location = new System.Drawing.Point(181, 184);
            this.lblStock.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(54, 16);
            this.lblStock.TabIndex = 4;
            this.lblStock.Text = "Stock: 0";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(744, 166);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 31);
            this.button2.TabIndex = 3;
            this.button2.Text = "Seleccionar Artículos";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // BuscarArticulosConteo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 481);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.lbRecuento);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dgvStock);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "BuscarArticulosConteo";
            this.Text = "Buscar Articulos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUbicacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAlmacen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbArticulo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkSinStock;
        private System.Windows.Forms.TextBox tbSerieLote;
        private System.Windows.Forms.Label lbSerieLote;
        private System.Windows.Forms.CheckBox chkStockBasadoEnSerieLote;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkGestionNinguna;
        private System.Windows.Forms.CheckBox chkGestionSerie;
        private System.Windows.Forms.CheckBox chkGestionLotes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodAlmacen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUbicacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSerieLote;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipoManejo;
        private System.Windows.Forms.CheckBox chkStockPorUbicacion;
        private System.Windows.Forms.Label lbRecuento;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbProveedor;
        public System.Windows.Forms.DataGridView dgvStock;
    }
}