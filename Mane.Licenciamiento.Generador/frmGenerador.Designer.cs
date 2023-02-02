namespace Mane.Licenciamiento.Generador
{
    partial class frmGenerador
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
            this.dtpExp = new System.Windows.Forms.DateTimePicker();
            this.tbHk = new System.Windows.Forms.TextBox();
            this.bdWebService1 = new Mane.BD.WebServiceBd.BdWebService();
            this.tbAppCve = new System.Windows.Forms.TextBox();
            this.tbAppId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtpExp
            // 
            this.dtpExp.Location = new System.Drawing.Point(109, 104);
            this.dtpExp.Name = "dtpExp";
            this.dtpExp.Size = new System.Drawing.Size(200, 20);
            this.dtpExp.TabIndex = 0;
            // 
            // tbHk
            // 
            this.tbHk.Location = new System.Drawing.Point(109, 78);
            this.tbHk.Name = "tbHk";
            this.tbHk.Size = new System.Drawing.Size(200, 20);
            this.tbHk.TabIndex = 1;
            // 
            // bdWebService1
            // 
            this.bdWebService1.Credentials = null;
            this.bdWebService1.Url = "https://localhost:44380/ManeBdWebService.asmx";
            this.bdWebService1.UseDefaultCredentials = false;
            this.bdWebService1.UsuarioValue = null;
            // 
            // tbAppCve
            // 
            this.tbAppCve.Location = new System.Drawing.Point(109, 52);
            this.tbAppCve.Name = "tbAppCve";
            this.tbAppCve.Size = new System.Drawing.Size(200, 20);
            this.tbAppCve.TabIndex = 1;
            // 
            // tbAppId
            // 
            this.tbAppId.Location = new System.Drawing.Point(109, 26);
            this.tbAppId.Name = "tbAppId";
            this.tbAppId.Size = new System.Drawing.Size(200, 20);
            this.tbAppId.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Applicacion ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Clave Encriptado";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Clave de Hardware";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Expiración";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(124, 146);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 37);
            this.button1.TabIndex = 3;
            this.button1.Text = "Generar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmGenerador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 229);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbAppId);
            this.Controls.Add(this.tbAppCve);
            this.Controls.Add(this.tbHk);
            this.Controls.Add(this.dtpExp);
            this.Name = "frmGenerador";
            this.Text = "frmGenerador";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpExp;
        private System.Windows.Forms.TextBox tbHk;
        private BD.WebServiceBd.BdWebService bdWebService1;
        private System.Windows.Forms.TextBox tbAppCve;
        private System.Windows.Forms.TextBox tbAppId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
    }
}