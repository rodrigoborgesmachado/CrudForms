namespace Visao
{
    partial class FO_Aguarde
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
            this.lbl_carregando = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_carregando
            // 
            this.lbl_carregando.AutoSize = true;
            this.lbl_carregando.Location = new System.Drawing.Point(22, 22);
            this.lbl_carregando.Name = "lbl_carregando";
            this.lbl_carregando.Size = new System.Drawing.Size(84, 16);
            this.lbl_carregando.TabIndex = 0;
            this.lbl_carregando.Text = "<mensagem>";
            // 
            // FO_Agurade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(349, 58);
            this.Controls.Add(this.lbl_carregando);
            this.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FO_Agurade";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Carregando";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_carregando;
    }
}