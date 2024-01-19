namespace Mane.CommonModules.WMS
{
    partial class Conteo
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
            this.dgvConteo = new System.Windows.Forms.DataGridView();
            this.menuTabla = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copiarToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarUnidadesDeMedidaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkContado = new System.Windows.Forms.CheckBox();
            this.chkFijar = new System.Windows.Forms.CheckBox();
            this.tbArticulo = new System.Windows.Forms.TextBox();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.cbAlmacen = new System.Windows.Forms.ComboBox();
            this.tbUbicacion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCrear = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tbComentarios = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAgregarArticulos = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.cbUnidad = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbSerieLote = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbNoConteo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.colArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colAlmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUbicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSerieLote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colCantidadContada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConteo)).BeginInit();
            this.menuTabla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvConteo
            // 
            this.dgvConteo.AllowUserToAddRows = false;
            this.dgvConteo.AllowUserToDeleteRows = false;
            this.dgvConteo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvConteo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConteo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colArticulo,
            this.Column2,
            this.Column3,
            this.colAlmacen,
            this.colUbicacion,
            this.colSerieLote,
            this.Column6,
            this.Column7,
            this.colCantidadContada,
            this.Column9,
            this.Column10,
            this.Column11});
            this.dgvConteo.Location = new System.Drawing.Point(12, 92);
            this.dgvConteo.MultiSelect = false;
            this.dgvConteo.Name = "dgvConteo";
            this.dgvConteo.RowHeadersVisible = false;
            this.dgvConteo.Size = new System.Drawing.Size(782, 276);
            this.dgvConteo.TabIndex = 0;
            this.dgvConteo.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConteo_CellEndEdit);
            this.dgvConteo.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvConteo_CellMouseClick);
            // 
            // menuTabla
            // 
            this.menuTabla.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copiarToolMenuItem,
            this.eliminarToolStripMenuItem,
            this.editarUnidadesDeMedidaToolStripMenuItem});
            this.menuTabla.Name = "menuTabla";
            this.menuTabla.Size = new System.Drawing.Size(216, 70);
            // 
            // copiarToolMenuItem
            // 
            this.copiarToolMenuItem.Name = "copiarToolMenuItem";
            this.copiarToolMenuItem.Size = new System.Drawing.Size(215, 22);
            this.copiarToolMenuItem.Text = "Copiar";
            this.copiarToolMenuItem.Click += new System.EventHandler(this.copiarToolMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.eliminarToolStripMenuItem.Text = "Borrar Línea";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // editarUnidadesDeMedidaToolStripMenuItem
            // 
            this.editarUnidadesDeMedidaToolStripMenuItem.Name = "editarUnidadesDeMedidaToolStripMenuItem";
            this.editarUnidadesDeMedidaToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.editarUnidadesDeMedidaToolStripMenuItem.Text = "Editar Unidades de medida";
            this.editarUnidadesDeMedidaToolStripMenuItem.Click += new System.EventHandler(this.editarUnidadesDeMedidaToolStripMenuItem_Click);
            // 
            // chkContado
            // 
            this.chkContado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkContado.AutoSize = true;
            this.chkContado.Location = new System.Drawing.Point(709, 68);
            this.chkContado.Name = "chkContado";
            this.chkContado.Size = new System.Drawing.Size(86, 17);
            this.chkContado.TabIndex = 128;
            this.chkContado.Text = "Contar todos";
            this.chkContado.UseVisualStyleBackColor = true;
            this.chkContado.CheckedChanged += new System.EventHandler(this.chkContado_CheckedChanged);
            // 
            // chkFijar
            // 
            this.chkFijar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkFijar.AutoSize = true;
            this.chkFijar.Location = new System.Drawing.Point(628, 69);
            this.chkFijar.Name = "chkFijar";
            this.chkFijar.Size = new System.Drawing.Size(74, 17);
            this.chkFijar.TabIndex = 127;
            this.chkFijar.Text = "Fijar todos";
            this.chkFijar.UseVisualStyleBackColor = true;
            this.chkFijar.CheckedChanged += new System.EventHandler(this.chkFijar_CheckedChanged);
            // 
            // tbArticulo
            // 
            this.tbArticulo.Location = new System.Drawing.Point(58, 8);
            this.tbArticulo.Name = "tbArticulo";
            this.tbArticulo.Size = new System.Drawing.Size(150, 20);
            this.tbArticulo.TabIndex = 129;
            this.tbArticulo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbArticulo_KeyDown);
            // 
            // nudCantidad
            // 
            this.nudCantidad.DecimalPlaces = 2;
            this.nudCantidad.Location = new System.Drawing.Point(58, 39);
            this.nudCantidad.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(150, 20);
            this.nudCantidad.TabIndex = 130;
            this.nudCantidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudCantidad_KeyDown);
            // 
            // cbAlmacen
            // 
            this.cbAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAlmacen.FormattingEnabled = true;
            this.cbAlmacen.Location = new System.Drawing.Point(278, 5);
            this.cbAlmacen.Name = "cbAlmacen";
            this.cbAlmacen.Size = new System.Drawing.Size(188, 21);
            this.cbAlmacen.TabIndex = 131;
            this.cbAlmacen.SelectedIndexChanged += new System.EventHandler(this.cbAlmacen_SelectedIndexChanged);
            // 
            // tbUbicacion
            // 
            this.tbUbicacion.Location = new System.Drawing.Point(277, 33);
            this.tbUbicacion.Name = "tbUbicacion";
            this.tbUbicacion.Size = new System.Drawing.Size(189, 20);
            this.tbUbicacion.TabIndex = 129;
            this.tbUbicacion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbUbicacion_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 132;
            this.label1.Text = "Artículo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 132;
            this.label2.Text = "Cantidad";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 132;
            this.label3.Text = "Ubicación";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(223, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 132;
            this.label4.Text = "Almacén";
            // 
            // btnCrear
            // 
            this.btnCrear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCrear.Location = new System.Drawing.Point(708, 406);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(86, 32);
            this.btnCrear.TabIndex = 133;
            this.btnCrear.Text = "Crear";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(593, 406);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 32);
            this.button1.TabIndex = 133;
            this.button1.Text = "Nuevo Conteo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnNuevoConteo_click);
            // 
            // tbComentarios
            // 
            this.tbComentarios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbComentarios.Location = new System.Drawing.Point(79, 374);
            this.tbComentarios.MaxLength = 255;
            this.tbComentarios.Name = "tbComentarios";
            this.tbComentarios.Size = new System.Drawing.Size(359, 20);
            this.tbComentarios.TabIndex = 134;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 377);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 132;
            this.label5.Text = "Comentarios";
            // 
            // btnAgregarArticulos
            // 
            this.btnAgregarArticulos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregarArticulos.Location = new System.Drawing.Point(16, 406);
            this.btnAgregarArticulos.Name = "btnAgregarArticulos";
            this.btnAgregarArticulos.Size = new System.Drawing.Size(115, 32);
            this.btnAgregarArticulos.TabIndex = 133;
            this.btnAgregarArticulos.Text = "Agregar Artículos";
            this.btnAgregarArticulos.UseVisualStyleBackColor = true;
            this.btnAgregarArticulos.Click += new System.EventHandler(this.btnAgregarArticulos_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(-444, 406);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(86, 32);
            this.button4.TabIndex = 133;
            this.button4.Text = "Nuevo Conteo";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // cbUnidad
            // 
            this.cbUnidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUnidad.FormattingEnabled = true;
            this.cbUnidad.Location = new System.Drawing.Point(58, 65);
            this.cbUnidad.Name = "cbUnidad";
            this.cbUnidad.Size = new System.Drawing.Size(121, 21);
            this.cbUnidad.TabIndex = 135;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 132;
            this.label6.Text = "Unidad";
            // 
            // tbSerieLote
            // 
            this.tbSerieLote.Location = new System.Drawing.Point(278, 65);
            this.tbSerieLote.Name = "tbSerieLote";
            this.tbSerieLote.Size = new System.Drawing.Size(189, 20);
            this.tbSerieLote.TabIndex = 129;
            this.tbSerieLote.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSerieLote_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(214, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 132;
            this.label7.Text = "Serie/Lote";
            // 
            // tbNoConteo
            // 
            this.tbNoConteo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNoConteo.Location = new System.Drawing.Point(628, 9);
            this.tbNoConteo.Name = "tbNoConteo";
            this.tbNoConteo.Size = new System.Drawing.Size(166, 20);
            this.tbNoConteo.TabIndex = 129;
            this.tbNoConteo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbNoConteo_KeyDown);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(552, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 132;
            this.label8.Text = "No. Conteo";
            // 
            // colArticulo
            // 
            this.colArticulo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colArticulo.DataPropertyName = "ItemCode";
            this.colArticulo.HeaderText = "Artículo";
            this.colArticulo.Name = "colArticulo";
            this.colArticulo.ReadOnly = true;
            this.colArticulo.Width = 69;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.DataPropertyName = "ItemDescription";
            this.Column2.HeaderText = "Descripción";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 200;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Column3.DataPropertyName = "Fijar";
            this.Column3.HeaderText = "Fijar";
            this.Column3.Name = "Column3";
            this.Column3.Width = 32;
            // 
            // colAlmacen
            // 
            this.colAlmacen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colAlmacen.DataPropertyName = "FromWhs";
            this.colAlmacen.HeaderText = "Almacén";
            this.colAlmacen.Name = "colAlmacen";
            this.colAlmacen.ReadOnly = true;
            this.colAlmacen.Width = 73;
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
            this.colUbicacion.Width = 80;
            // 
            // colSerieLote
            // 
            this.colSerieLote.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colSerieLote.DataPropertyName = "SerieLote";
            dataGridViewCellStyle2.NullValue = "N/A";
            this.colSerieLote.DefaultCellStyle = dataGridViewCellStyle2;
            this.colSerieLote.HeaderText = "Serie/Lote";
            this.colSerieLote.Name = "colSerieLote";
            this.colSerieLote.ReadOnly = true;
            this.colSerieLote.Width = 82;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Column6.DataPropertyName = "Quantity";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle3.Format = "N2";
            this.Column6.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column6.HeaderText = "Existencia";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 80;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Column7.DataPropertyName = "Contado";
            this.Column7.HeaderText = "Contado";
            this.Column7.Name = "Column7";
            this.Column7.Width = 53;
            // 
            // colCantidadContada
            // 
            this.colCantidadContada.DataPropertyName = "CantidadContada";
            dataGridViewCellStyle4.Format = "N2";
            this.colCantidadContada.DefaultCellStyle = dataGridViewCellStyle4;
            this.colCantidadContada.HeaderText = "Cantidad Contada";
            this.colCantidadContada.Name = "colCantidadContada";
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "Diferencia";
            this.Column9.HeaderText = "Desviación";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "UnidadDeMedida";
            this.Column10.HeaderText = "Unidad de Medida";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "ArticulosPorUnidad";
            this.Column11.HeaderText = "Artículos por UdM";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // ConteoV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 441);
            this.Controls.Add(this.cbUnidad);
            this.Controls.Add(this.tbComentarios);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnAgregarArticulos);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbAlmacen);
            this.Controls.Add(this.nudCantidad);
            this.Controls.Add(this.tbSerieLote);
            this.Controls.Add(this.tbNoConteo);
            this.Controls.Add(this.tbUbicacion);
            this.Controls.Add(this.tbArticulo);
            this.Controls.Add(this.chkContado);
            this.Controls.Add(this.chkFijar);
            this.Controls.Add(this.dgvConteo);
            this.Name = "ConteoV2";
            this.Text = "ConteoV2";
            ((System.ComponentModel.ISupportInitialize)(this.dgvConteo)).EndInit();
            this.menuTabla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvConteo;
        private System.Windows.Forms.CheckBox chkContado;
        private System.Windows.Forms.CheckBox chkFijar;
        private System.Windows.Forms.TextBox tbArticulo;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.ComboBox cbAlmacen;
        private System.Windows.Forms.TextBox tbUbicacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbComentarios;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAgregarArticulos;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox cbUnidad;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbSerieLote;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ContextMenuStrip menuTabla;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.TextBox tbNoConteo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripMenuItem copiarToolMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarUnidadesDeMedidaToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArticulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlmacen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUbicacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSerieLote;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCantidadContada;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
    }
}