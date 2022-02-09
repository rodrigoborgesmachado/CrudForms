
namespace Visao
{
    partial class FO_OrderBy
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
            this.cmb_campo = new System.Windows.Forms.ComboBox();
            this.lbl_campo = new System.Windows.Forms.Label();
            this.cmb_formaOrdenacao = new System.Windows.Forms.ComboBox();
            this.pan_botton = new System.Windows.Forms.Panel();
            this.btn_filtrar = new System.Windows.Forms.Button();
            this.pan_botton.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmb_campo
            // 
            this.cmb_campo.FormattingEnabled = true;
            this.cmb_campo.Location = new System.Drawing.Point(17, 30);
            this.cmb_campo.Name = "cmb_campo";
            this.cmb_campo.Size = new System.Drawing.Size(291, 27);
            this.cmb_campo.TabIndex = 0;
            // 
            // lbl_campo
            // 
            this.lbl_campo.AutoSize = true;
            this.lbl_campo.Location = new System.Drawing.Point(13, 8);
            this.lbl_campo.Name = "lbl_campo";
            this.lbl_campo.Size = new System.Drawing.Size(57, 19);
            this.lbl_campo.TabIndex = 1;
            this.lbl_campo.Text = "Campo";
            // 
            // cmb_formaOrdenacao
            // 
            this.cmb_formaOrdenacao.FormattingEnabled = true;
            this.cmb_formaOrdenacao.Items.AddRange(new object[] {
            "ASC",
            "DESC"});
            this.cmb_formaOrdenacao.Location = new System.Drawing.Point(314, 30);
            this.cmb_formaOrdenacao.Name = "cmb_formaOrdenacao";
            this.cmb_formaOrdenacao.Size = new System.Drawing.Size(95, 27);
            this.cmb_formaOrdenacao.TabIndex = 2;
            // 
            // pan_botton
            // 
            this.pan_botton.Controls.Add(this.btn_filtrar);
            this.pan_botton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pan_botton.Location = new System.Drawing.Point(0, 60);
            this.pan_botton.Name = "pan_botton";
            this.pan_botton.Size = new System.Drawing.Size(419, 36);
            this.pan_botton.TabIndex = 21;
            // 
            // btn_filtrar
            // 
            this.btn_filtrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_filtrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_filtrar.Location = new System.Drawing.Point(290, 4);
            this.btn_filtrar.Name = "btn_filtrar";
            this.btn_filtrar.Size = new System.Drawing.Size(123, 29);
            this.btn_filtrar.TabIndex = 8;
            this.btn_filtrar.Text = "Filtrar";
            this.btn_filtrar.UseVisualStyleBackColor = true;
            this.btn_filtrar.Click += new System.EventHandler(this.btn_filtrar_Click);
            // 
            // FO_OrderBy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(419, 96);
            this.Controls.Add(this.pan_botton);
            this.Controls.Add(this.cmb_formaOrdenacao);
            this.Controls.Add(this.lbl_campo);
            this.Controls.Add(this.cmb_campo);
            this.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FO_OrderBy";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Order By";
            this.pan_botton.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_campo;
        private System.Windows.Forms.Label lbl_campo;
        private System.Windows.Forms.ComboBox cmb_formaOrdenacao;
        private System.Windows.Forms.Panel pan_botton;
        private System.Windows.Forms.Button btn_filtrar;
    }
}