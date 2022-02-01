namespace Visao
{
    partial class UC_WEB
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
            this.pan_completo = new System.Windows.Forms.Panel();
            this.web_completo = new System.Windows.Forms.WebBrowser();
            this.pan_completo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pan_completo
            // 
            this.pan_completo.Controls.Add(this.web_completo);
            this.pan_completo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_completo.Location = new System.Drawing.Point(0, 0);
            this.pan_completo.Name = "pan_completo";
            this.pan_completo.Size = new System.Drawing.Size(740, 562);
            this.pan_completo.TabIndex = 0;
            // 
            // web_completo
            // 
            this.web_completo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.web_completo.Location = new System.Drawing.Point(0, 0);
            this.web_completo.MinimumSize = new System.Drawing.Size(20, 20);
            this.web_completo.Name = "web_completo";
            this.web_completo.Size = new System.Drawing.Size(740, 562);
            this.web_completo.TabIndex = 0;
            // 
            // UC_WEB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pan_completo);
            this.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.Name = "UC_WEB";
            this.Size = new System.Drawing.Size(740, 562);
            this.pan_completo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pan_completo;
        private System.Windows.Forms.WebBrowser web_completo;
    }
}
