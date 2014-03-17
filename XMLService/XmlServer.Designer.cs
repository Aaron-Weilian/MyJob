
namespace XMLService
{
    partial class XmlServer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.scheduled = new System.Timers.Timer();
            this.eventLog = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.scheduled)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog)).BeginInit();
            // 
            // scheduled
            // 
            this.scheduled.Enabled = true;
            // 
            // eventLog
            // 
            this.eventLog.Log = "XmlServer";
            this.eventLog.Source = "XMLServer";
            // 
            // XmlServer
            // 
            this.ServiceName = "XmlService";
            ((System.ComponentModel.ISupportInitialize)(this.scheduled)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog)).EndInit();

        }

        #endregion

        private System.Timers.Timer scheduled;
        private System.Diagnostics.EventLog eventLog;

        
    }
}
