using System;
using System.Windows.Forms;

namespace Mane.Helpers
{
    public static class MsgBox
    {
        public static bool DebugMode;
        public static void debug(Exception ex) => debug(ex.ToString());
        public static void debug(string msg) { if (DebugMode) MessageBox.Show(msg, "DebugMsg"); }
        public static void normal(string msg, string titulo = "") => MessageBox.Show(msg, titulo);
        public static void warning(string msg, string titulo = "Advertencia") => MessageBox.Show(msg, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        public static void info(string msg, string titulo = "Info") => MessageBox.Show(msg, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        public static void error(string msg, string titulo = "Error") => MessageBox.Show(msg, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        public static bool confirm(string msg, string titulo = "Confirmar") => MessageBox.Show(msg, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        public static DialogResult abortRetryIngnore(string msg, string titulo = "") => MessageBox.Show(msg, titulo, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Question);
        public static DialogResult retryCancel(string msg, string titulo = "") => MessageBox.Show(msg, titulo, MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
    }
}
