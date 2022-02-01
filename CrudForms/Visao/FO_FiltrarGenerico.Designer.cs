
namespace Visao
{
    partial class FO_FiltrarGenerico
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
            this.pan_option = new System.Windows.Forms.Panel();
            this.btn_filtrar = new System.Windows.Forms.Button();
            this.pan_tot = new System.Windows.Forms.Panel();
            this.pan_option.SuspendLayout();
            this.SuspendLayout();
            // 
            // pan_option
            // 
            this.pan_option.Controls.Add(this.btn_filtrar);
            this.pan_option.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pan_option.Location = new System.Drawing.Point(0, 606);
            this.pan_option.Name = "pan_option";
            this.pan_option.Size = new System.Drawing.Size(568, 39);
            this.pan_option.TabIndex = 0;
            // 
            // btn_filtrar
            // 
            this.btn_filtrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_filtrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_filtrar.Location = new System.Drawing.Point(442, 7);
            this.btn_filtrar.Name = "btn_filtrar";
            this.btn_filtrar.Size = new System.Drawing.Size(123, 29);
            this.btn_filtrar.TabIndex = 9;
            this.btn_filtrar.Text = "Filtrar";
            this.btn_filtrar.UseVisualStyleBackColor = true;
            this.btn_filtrar.Click += new System.EventHandler(this.btn_filtrar_Click);
            // 
            // pan_tot
            // 
            this.pan_tot.AutoScroll = true;
            this.pan_tot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_tot.Location = new System.Drawing.Point(0, 0);
            this.pan_tot.Name = "pan_tot";
            this.pan_tot.Size = new System.Drawing.Size(568, 606);
            this.pan_tot.TabIndex = 1;
            // 
            // FO_FiltrarGenerico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(568, 645);
            this.Controls.Add(this.pan_tot);
            this.Controls.Add(this.pan_option);
            this.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FO_FiltrarGenerico";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "<filtro tabela>";
            this.pan_option.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pan_option;
        private System.Windows.Forms.Button btn_filtrar;
        private System.Windows.Forms.Panel pan_tot;
    }
}