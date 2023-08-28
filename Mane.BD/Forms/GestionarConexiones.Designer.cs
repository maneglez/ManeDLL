namespace Mane.BD.Forms
{
    partial class GestionarConexiones
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTipoBd = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colSubtipoBd = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.probarConexionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarConexionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.colTipoBd,
            this.colSubtipoBd,
            this.Column7,
            this.Column8,
            this.Column9});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(659, 208);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Nombre";
            this.Column1.HeaderText = "Nombre Conexion";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Servidor";
            this.Column2.HeaderText = "Servidor";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "NombreBD";
            this.Column3.HeaderText = "Base De datos";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Puerto";
            this.Column4.HeaderText = "Puerto";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Usuario";
            this.Column5.HeaderText = "Usuario";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "Contrasena";
            this.Column6.HeaderText = "Contrasena";
            this.Column6.Name = "Column6";
            // 
            // colTipoBd
            // 
            this.colTipoBd.DataPropertyName = "TipoDeBaseDeDatos";
            this.colTipoBd.HeaderText = "Tipo de Bd";
            this.colTipoBd.Name = "colTipoBd";
            // 
            // colSubtipoBd
            // 
            this.colSubtipoBd.DataPropertyName = "SubTipoDeBD";
            this.colSubtipoBd.HeaderText = "SubTipo de BD";
            this.colSubtipoBd.Name = "colSubtipoBd";
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "Driver";
            this.Column7.HeaderText = "Driver";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "TimeOut";
            this.Column8.HeaderText = "TimeOut";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "UseSSL";
            this.Column9.HeaderText = "UseSSL";
            this.Column9.Name = "Column9";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.probarConexionToolStripMenuItem,
            this.eliminarConexionToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(172, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // probarConexionToolStripMenuItem
            // 
            this.probarConexionToolStripMenuItem.Name = "probarConexionToolStripMenuItem";
            this.probarConexionToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.probarConexionToolStripMenuItem.Text = "Probar Conexion";
            this.probarConexionToolStripMenuItem.Click += new System.EventHandler(this.probarConexionToolStripMenuItem_Click);
            // 
            // eliminarConexionToolStripMenuItem
            // 
            this.eliminarConexionToolStripMenuItem.Name = "eliminarConexionToolStripMenuItem";
            this.eliminarConexionToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.eliminarConexionToolStripMenuItem.Text = "Eliminar Conexion";
            this.eliminarConexionToolStripMenuItem.Click += new System.EventHandler(this.eliminarConexionToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(25, 241);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 34);
            this.button1.TabIndex = 1;
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GestionarConexiones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 287);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "GestionarConexiones";
            this.Text = "GestionarConexiones";
            this.Load += new System.EventHandler(this.GestionarConexiones_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.GestionarConexiones_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.GestionarConexiones_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewComboBoxColumn colTipoBd;
        private System.Windows.Forms.DataGridViewComboBoxColumn colSubtipoBd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem probarConexionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarConexionToolStripMenuItem;
    }
}