using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using CrystalDecisions.Windows.Forms;

namespace Mane.Helpers.CrystalReports
{
    public partial class VisorCrystal : Form
    {
        private dynamic CrpViewer;
        private object RptSource;
        public VisorCrystal()
        {
            InitializeComponent();
            CrpViewer = Activator.CreateInstance(Dependencias.ReportViewerType);
            //CrpViewer = new CrystalReportViewer();
            CrpViewer.ActiveViewIndex = -1;
            CrpViewer.BorderStyle = BorderStyle.FixedSingle;
            CrpViewer.Cursor = Cursors.Default;
            CrpViewer.Dock = DockStyle.Fill;
            CrpViewer.Location = new Point(0, 0);
            CrpViewer.Name = "CrpViewer";
            CrpViewer.TabIndex = 0;
            Controls.Add(CrpViewer);
        }
        public void SetReportSource(object rpt)
        {
            if (Visible) CrpViewer.ReportSource = rpt;
            RptSource = rpt;
        }

        private void VisorCrystal_Load(object sender, EventArgs e)
        {
            
        }

        private void VisorCrystal_Shown(object sender, EventArgs e)
        {
            CrpViewer.ReportSource = RptSource;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // VisorCrystal
            // 
            this.ClientSize = new System.Drawing.Size(707, 401);
            this.Name = "VisorCrystal";
            this.Load += new System.EventHandler(this.VisorCrystal_Load);
            this.Shown += new System.EventHandler(this.VisorCrystal_Shown);
            this.ResumeLayout(false);

        }
    }
}
