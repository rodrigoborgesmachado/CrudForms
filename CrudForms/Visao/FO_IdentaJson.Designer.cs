
namespace Visao
{
    partial class FO_IdentaJson
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
            this.grb_total = new System.Windows.Forms.GroupBox();
            this.pan_completo = new System.Windows.Forms.Panel();
            this.tbx_json = new System.Windows.Forms.TextBox();
            this.pan_button = new System.Windows.Forms.Panel();
            this.btn_copiar = new System.Windows.Forms.Button();
            this.btn_confirmar = new System.Windows.Forms.Button();
            this.grb_total.SuspendLayout();
            this.pan_completo.SuspendLayout();
            this.pan_button.SuspendLayout();
            this.SuspendLayout();
            // 
            // grb_total
            // 
            this.grb_total.Controls.Add(this.pan_completo);
            this.grb_total.Controls.Add(this.pan_button);
            this.grb_total.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grb_total.Location = new System.Drawing.Point(0, 0);
            this.grb_total.Name = "grb_total";
            this.grb_total.Size = new System.Drawing.Size(541, 440);
            this.grb_total.TabIndex = 0;
            this.grb_total.TabStop = false;
            this.grb_total.Text = "Json";
            // 
            // pan_completo
            // 
            this.pan_completo.Controls.Add(this.tbx_json);
            this.pan_completo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_completo.Location = new System.Drawing.Point(3, 23);
            this.pan_completo.Name = "pan_completo";
            this.pan_completo.Size = new System.Drawing.Size(535, 371);
            this.pan_completo.TabIndex = 1;
            // 
            // tbx_json
            // 
            this.tbx_json.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_json.Location = new System.Drawing.Point(0, 0);
            this.tbx_json.MaxLength = 999999999;
            this.tbx_json.Multiline = true;
            this.tbx_json.Name = "tbx_json";
            this.tbx_json.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbx_json.Size = new System.Drawing.Size(535, 371);
            this.tbx_json.TabIndex = 0;
            // 
            // pan_button
            // 
            this.pan_button.Controls.Add(this.btn_copiar);
            this.pan_button.Controls.Add(this.btn_confirmar);
            this.pan_button.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pan_button.Location = new System.Drawing.Point(3, 394);
            this.pan_button.Name = "pan_button";
            this.pan_button.Size = new System.Drawing.Size(535, 43);
            this.pan_button.TabIndex = 0;
            // 
            // btn_copiar
            // 
            this.btn_copiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_copiar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_copiar.Location = new System.Drawing.Point(3, 6);
            this.btn_copiar.Name = "btn_copiar";
            this.btn_copiar.Size = new System.Drawing.Size(83, 29);
            this.btn_copiar.TabIndex = 6;
            this.btn_copiar.Text = "Copiar";
            this.btn_copiar.UseVisualStyleBackColor = true;
            this.btn_copiar.Click += new System.EventHandler(this.btn_copiar_Click);
            // 
            // btn_confirmar
            // 
            this.btn_confirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_confirmar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_confirmar.Location = new System.Drawing.Point(448, 6);
            this.btn_confirmar.Name = "btn_confirmar";
            this.btn_confirmar.Size = new System.Drawing.Size(83, 29);
            this.btn_confirmar.TabIndex = 5;
            this.btn_confirmar.Text = "Confirmar";
            this.btn_confirmar.UseVisualStyleBackColor = true;
            this.btn_confirmar.Click += new System.EventHandler(this.btn_confirmar_Click);
            // 
            // FO_IdentaJson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(541, 440);
            this.Controls.Add(this.grb_total);
            this.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FO_IdentaJson";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Identa Json";
            this.grb_total.ResumeLayout(false);
            this.pan_completo.ResumeLayout(false);
            this.pan_completo.PerformLayout();
            this.pan_button.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grb_total;
        private System.Windows.Forms.Panel pan_button;
        private System.Windows.Forms.Panel pan_completo;
        private System.Windows.Forms.TextBox tbx_json;
        private System.Windows.Forms.Button btn_copiar;
        private System.Windows.Forms.Button btn_confirmar;
    }
}