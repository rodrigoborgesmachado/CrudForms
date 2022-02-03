
namespace Visao
{
    partial class FO_QuantidadeLinhasTabelas
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
            this.btn_acao = new System.Windows.Forms.Button();
            this.pan_tot = new System.Windows.Forms.Panel();
            this.grb_total = new System.Windows.Forms.GroupBox();
            this.tbx_quantidade = new System.Windows.Forms.TextBox();
            this.pan_option.SuspendLayout();
            this.pan_tot.SuspendLayout();
            this.grb_total.SuspendLayout();
            this.SuspendLayout();
            // 
            // pan_option
            // 
            this.pan_option.Controls.Add(this.btn_acao);
            this.pan_option.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pan_option.Location = new System.Drawing.Point(0, 89);
            this.pan_option.Name = "pan_option";
            this.pan_option.Size = new System.Drawing.Size(374, 39);
            this.pan_option.TabIndex = 3;
            // 
            // btn_acao
            // 
            this.btn_acao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_acao.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_acao.Location = new System.Drawing.Point(248, 7);
            this.btn_acao.Name = "btn_acao";
            this.btn_acao.Size = new System.Drawing.Size(123, 29);
            this.btn_acao.TabIndex = 9;
            this.btn_acao.Text = "Confirmar";
            this.btn_acao.UseVisualStyleBackColor = true;
            this.btn_acao.Click += new System.EventHandler(this.btn_acao_Click);
            // 
            // pan_tot
            // 
            this.pan_tot.Controls.Add(this.grb_total);
            this.pan_tot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_tot.Location = new System.Drawing.Point(0, 0);
            this.pan_tot.Name = "pan_tot";
            this.pan_tot.Size = new System.Drawing.Size(374, 89);
            this.pan_tot.TabIndex = 4;
            // 
            // grb_total
            // 
            this.grb_total.Controls.Add(this.tbx_quantidade);
            this.grb_total.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grb_total.Location = new System.Drawing.Point(0, 0);
            this.grb_total.Name = "grb_total";
            this.grb_total.Size = new System.Drawing.Size(374, 89);
            this.grb_total.TabIndex = 0;
            this.grb_total.TabStop = false;
            this.grb_total.Text = "Quantidade itens por tabela";
            // 
            // textBox1
            // 
            this.tbx_quantidade.Location = new System.Drawing.Point(13, 27);
            this.tbx_quantidade.Name = "textBox1";
            this.tbx_quantidade.Size = new System.Drawing.Size(355, 27);
            this.tbx_quantidade.TabIndex = 0;
            // 
            // FO_QuantidadeLinhasTabelas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(374, 128);
            this.Controls.Add(this.pan_tot);
            this.Controls.Add(this.pan_option);
            this.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FO_QuantidadeLinhasTabelas";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quantidade";
            this.pan_option.ResumeLayout(false);
            this.pan_tot.ResumeLayout(false);
            this.grb_total.ResumeLayout(false);
            this.grb_total.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pan_option;
        private System.Windows.Forms.Button btn_acao;
        private System.Windows.Forms.Panel pan_tot;
        private System.Windows.Forms.GroupBox grb_total;
        private System.Windows.Forms.TextBox tbx_quantidade;
    }
}